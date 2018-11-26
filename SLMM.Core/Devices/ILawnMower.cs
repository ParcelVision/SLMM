using System.Threading.Tasks;
using SLMM.Common.Navigation;

namespace SLMM.Core.Devices
{
    public interface ILawnMower
    {
        Task<bool> TurnAsync(TurnDirection direction);
        Task<bool> AdvanceAsync();
        Task<OperationStatus> GetStatusAsync();
    }
}