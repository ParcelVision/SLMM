using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using SLMM.Common.Navigation;

namespace SLMM.Core.Navigation
{
    public class Map
    {
        private readonly ConcurrentDictionary<string, Point> _pins = new ConcurrentDictionary<string, Point>();

        public Map(Size size)
        {
            if (size.IsEmpty)
                throw new ArgumentException("The map cannot be empty", nameof(size));
            Area = new Rectangle(Point.Empty, size);
        }

        public IReadOnlyDictionary<string, Point> Pins => _pins;

        public Rectangle Area { get; }

        public bool AddPin(string pin, Point location)
        {
            if (!Area.Contains(location))
                throw new ArgumentOutOfRangeException(nameof(location), "Pin location is outside the map");

            return _pins.TryAdd(pin, location);
        }

        public Point GetPin(string pin)
        {
            return !_pins.TryGetValue(pin, out var location)
                ? throw new ArgumentException("Pin does not exist", nameof(pin))
                : location;
        }

        private Point PlotPosition(Point currentLocation, int distance, Orientation orientation)
        {
            var newLocation = new Point(currentLocation.X, currentLocation.Y);
            switch (orientation)
            {
                case Orientation.East:
                    newLocation.X = newLocation.X + distance;
                    break;
                case Orientation.North:
                    newLocation.Y = newLocation.Y + distance;
                    break;
                case Orientation.West:
                    newLocation.X = newLocation.X - distance;
                    break;
                case Orientation.South:
                    newLocation.Y = newLocation.Y - distance;
                    break;
            }

            return newLocation;
        }

        public (bool Valid, Point NewLocation) CanMovePin(string pin, int distance, Orientation orientation)
        {
            var location = GetPin(pin);
            var newLocation = PlotPosition(location, distance, orientation);
            return (Area.Contains(newLocation), newLocation);
        }

        public bool TryMovePin(string pin, int distance, Orientation orientation)
        {
            var newLocationCheck = CanMovePin(pin, distance, orientation);
            if (!newLocationCheck.Valid) return false;
            _pins[pin] = newLocationCheck.NewLocation;
            return true;
        }
    }
}