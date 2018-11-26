using System.Drawing;
using SLMM.Common.Navigation;

namespace SLMM.Configuration
{
    public class LawnMowingConfiguration
    {
        public Orientation StartOrientation { get; set; }
        public Point StartPosition { get; set; }
        public Size GardenSize { get; set; }
    }
}