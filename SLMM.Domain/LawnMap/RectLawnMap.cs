using System;
using System.Drawing;

namespace SLMM.Domain.LawnMap
{
    public class RectLawnMap : IRectLawnMap
    {
        public Rectangle Geometry { get; set; }

        public RectLawnMap(Rectangle geometry)
        {
            Geometry = geometry;

            if (Geometry.IsEmpty)
                throw new ArgumentOutOfRangeException(nameof(geometry));
        }

        public bool CanMove(Point coordinates)
        {
            return Geometry.Contains(coordinates);
        }

        public bool CanMove(in Position position, int steps)
        {
            EnsurePositionWithinLawn(position);

            return CanMove(AddSteps(position, steps));
        }

        public bool TryMove(in Position position, int steps, out Position newPosition)
        {
            EnsurePositionWithinLawn(position);

            var newCoordinates = AddSteps(position, steps);

            if (!CanMove(newCoordinates))
            {
                newPosition = position;
                return false;
            }

            newPosition = new Position(newCoordinates, position.Orientation);
            return true;
        }

        private void EnsurePositionWithinLawn(in Position position)
        {
            if (!Geometry.Contains(position.Coordinates))
                throw new ArgumentOutOfRangeException(nameof(position));
        }

        private static Point AddSteps(Position position, int steps)
        {
            if (steps < 0) throw new ArgumentOutOfRangeException(nameof(steps));

            var x = position.Coordinates.X;
            var y = position.Coordinates.Y;

            switch (position.Orientation)
            {
                case Orientation.North:
                    y = position.Coordinates.Y + steps;
                    break;
                case Orientation.South:
                    y = position.Coordinates.Y - steps;
                    break;
                case Orientation.East:
                    x = position.Coordinates.X + steps;
                    break;
                case Orientation.West:
                    x = position.Coordinates.X - steps;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var newCoordinates = new Point(x, y);
            return newCoordinates;
        }
    }
}