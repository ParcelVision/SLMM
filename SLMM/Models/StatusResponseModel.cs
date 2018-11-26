using SLMM.Common.Navigation;

namespace SLMM.Models
{
    public class StatusResponseModel
    {
        public PositionModel Position { get; set; }
        public Orientation Orientation { get; set; }
    }
}