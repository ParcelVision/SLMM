using System.Drawing;
using System.Threading.Tasks;
using SLMM.Common.Navigation;

namespace SLMM.Core.Driving
{
    public interface ILawnMowerDriver
    {
        Task<bool> TurnAsync(TurnDirection direction);
        Task<bool> AdvanceAsync();
        Task<Point> GetPositionAsync();
        Task<Orientation> GetOrientation();
    }
}