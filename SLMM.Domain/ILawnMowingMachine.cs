using System;
using System.Threading;
using System.Threading.Tasks;
using SLMM.Domain.LawnMap;

namespace SLMM.Domain
{
    public interface ILawnMowingMachine : IDisposable
    {
        ILawnMap Map { get; }

        Position Position { get; }

        Task AdvanceAsync();

        Task AdvanceAsync(int steps);

        Task AdvanceAsync(int steps, CancellationToken token);

        Task TurnLeftAsync();

        Task TurnLeftAsync(CancellationToken token);

        Task TurnRightAsync();

        Task TurnRightAsync(CancellationToken token);

        Task TurnAsync(short degrees);

        Task TurnAsync(short degrees, CancellationToken token);
    }
}