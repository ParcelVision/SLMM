using System.Drawing;

namespace SLMM.Models
{
    public class PositionModel
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static implicit operator PositionModel(Point point)
        {
            return new PositionModel {X = point.X, Y = point.Y};
        }
    }
}