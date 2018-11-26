using System;
using System.Threading;
using System.Threading.Tasks;
using SLMM.Common.Navigation;

namespace SLMM.Core.Devices
{
    public class VirtualLawnMower : ILawnMower
    {
        private readonly SemaphoreSlim _operationSemaphore = new SemaphoreSlim(1, 1);
        public TimeSpan TurnDuration { get; set; } = TimeSpan.FromSeconds(2);
        public TimeSpan AvanceDuration { get; set; } = TimeSpan.FromSeconds(5);


        private OperationStatus Status { get; set; } = OperationStatus.Idle;

        public async Task<bool> TurnAsync(TurnDirection direction)
        {
            return await PerformVirtualOperation(OperationStatus.Turning, TurnDuration);
        }

        public async Task<bool> AdvanceAsync()
        {
            return await PerformVirtualOperation(OperationStatus.Advancing, AvanceDuration);
        }


        public async Task<OperationStatus> GetStatusAsync()
        {
            await _operationSemaphore.WaitAsync();
            try
            {
                return Status;
            }
            finally
            {
                _operationSemaphore.Release();
            }
        }

        protected async Task<bool> SetStatusAsync(OperationStatus newStatus, bool rejectNotIdle = true)
        {
            await _operationSemaphore.WaitAsync();
            try
            {
                if (rejectNotIdle && Status != OperationStatus.Idle && newStatus != OperationStatus.Idle)
                    return false;
                Status = newStatus;
                return true;
            }
            finally
            {
                _operationSemaphore.Release();
            }
        }

        protected async Task<bool> PerformVirtualOperation(OperationStatus newStatus, TimeSpan duration)
        {
            if (!await SetStatusAsync(newStatus)) return false;
            await Task.Delay(duration);
            await SetStatusAsync(OperationStatus.Idle);

            return true;
        }
    }
}