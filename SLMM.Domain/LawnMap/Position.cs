using System.Drawing;

namespace SLMM.Domain.LawnMap
{
    public readonly struct Position
    {
        public readonly Point Coordinates;
        public readonly Orientation Orientation;
        
        public Position(Orientation orientation) : this(new Point(), orientation)
        {
        }

        public Position(Point coordinates, Orientation orientation)
        {
            Coordinates = coordinates;
            Orientation = orientation;
        }
    }
}