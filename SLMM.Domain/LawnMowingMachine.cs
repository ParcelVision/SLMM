using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using SLMM.Domain.LawnMap;

namespace SLMM.Domain
{
    public class LawnMowingMachine : ILawnMowingMachine
    {
        public const short TurnDegrees = 90;
        public const short OneStep = 1;

        protected TimeSpan AdvanceDelay { get; set; } = TimeSpan.FromSeconds(5);
        protected TimeSpan TurnDelay { get; set; } = TimeSpan.FromSeconds(2);

        private readonly SemaphoreSlim _busy = new SemaphoreSlim(1, 1);

        private readonly ConcurrentQueue<(TaskCompletionSource<object> Tcs, Func<Task> Action)> _queue = new ConcurrentQueue<(TaskCompletionSource<object>, Func<Task>)>();

        public ILawnMap Map { get; }

        public Position Position { get; private set; }

        public LawnMowingMachine(ILawnMap map, Position startPosition)
        {
            Map = map;
            Position = startPosition;

            if (!map.CanMove(Position.Coordinates))
                throw new ArgumentOutOfRangeException(nameof(startPosition));
        }

        public Task AdvanceAsync()
        {
            return AdvanceAsync(OneStep);
        }

        public Task AdvanceAsync(int steps)
        {
            return AdvanceAsync(steps, CancellationToken.None);
        }

        public async Task AdvanceAsync(int steps, CancellationToken token)
        {
            if (steps < OneStep)
                throw new ArgumentOutOfRangeException(nameof(steps));

            await OnIdleAsync(async () =>
            {
                if (!Map.TryMove(Position, steps, out var newPosition))
                    throw new InvalidOperationException();

                await Task.Delay(AdvanceDelay, token);

                Position = newPosition;
            }, token).ConfigureAwait(false);
        }

        public Task TurnLeftAsync()
        {
            return TurnLeftAsync(CancellationToken.None);
        }

        public Task TurnLeftAsync(CancellationToken token)
        {
            return TurnAsync(-TurnDegrees, token);
        }

        public Task TurnRightAsync()
        {
            return TurnRightAsync(CancellationToken.None);
        }

        public Task TurnRightAsync(CancellationToken token)
        {
            return TurnAsync(TurnDegrees, token);
        }

        public Task TurnAsync(short degrees)
        {
            return TurnAsync(degrees, CancellationToken.None);
        }

        public async Task TurnAsync(short degrees, CancellationToken token)
        {
            if (degrees != -TurnDegrees && degrees != TurnDegrees)
                throw new ArgumentOutOfRangeException(nameof(degrees), degrees, $"Currently supported values: {-TurnDegrees}, {TurnDegrees}");

            await OnIdleAsync(async () =>
            {
                await Task.Delay(TurnDelay, token);

                // Normalize degrees between 0 and 360, assumes the degrees are between -360/360

                var newDegrees = ((short) Position.Orientation + (degrees % 360) + 360) % 360;

                Position = new Position(Position.Coordinates, (Orientation) newDegrees);
            }, token).ConfigureAwait(false);
        }

        private Task OnIdleAsync(Func<Task> action, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            var tcs = new TaskCompletionSource<object>(action);

            _queue.Enqueue((tcs, action));

            _busy.WaitAsync(token).ContinueWith(async t =>
            {
                // Note: The tcs in the queued action is not the same as the one created above.
                // Note: The code below simply handles the draining of the queue.
                (TaskCompletionSource<object> Tcs, Func<Task> Action) queuedAction = default;
                try
                {
                    if (_queue.TryDequeue(out queuedAction))
                    {
                        if (token.IsCancellationRequested)
                            throw new TaskCanceledException();

                        await queuedAction.Action();

                        queuedAction.Tcs.SetResult(null);
                    }
                }
                catch (Exception e)
                {
                    queuedAction.Tcs?.TrySetException(e);
                }
                finally
                {
                    _busy.Release();
                }
            }, token);

            return tcs.Task;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) _busy?.Dispose();

            while (!_queue.IsEmpty)
            {
                try
                {
                    _queue.TryDequeue(out var workItem);

                    workItem.Tcs.TrySetCanceled();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}