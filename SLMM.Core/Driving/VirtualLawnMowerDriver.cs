using System;
using System.Drawing;
using System.Threading.Tasks;
using SLMM.Common.Navigation;
using SLMM.Core.Devices;
using SLMM.Core.Navigation;

namespace SLMM.Core.Driving
{
    public class VirtualLawnMowerDriver : ILawnMowerDriver
    {
        public VirtualLawnMowerDriver(ILawnMower vehicle, Size mapSize,
            Point location = default(Point), Orientation orientation = Orientation.North)
        {
            Mower = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
            Map = new Map(mapSize);
            Pin = nameof(Mower);
            Map.AddPin(Pin, location);
            Orientation = orientation;
        }

        public Map Map { get; }
        public ILawnMower Mower { get; }
        public Orientation Orientation { get; private set; }

        private string Pin { get; }

        public async Task<bool> TurnAsync(TurnDirection direction)
        {
            if (!await Mower.TurnAsync(direction)) return false;

            var orientationAngle = ((int) Orientation + (int) direction) % 360;
            orientationAngle = orientationAngle >= 0 ? orientationAngle : orientationAngle + 360;
            Orientation = (Orientation) orientationAngle;

            return true;
        }

        public async Task<bool> AdvanceAsync()
        {
            if (!Map.CanMovePin(Pin, 1, Orientation).Valid) return false;
            if (!await Mower.AdvanceAsync()) return false;
            Map.TryMovePin(Pin, 1, Orientation);
            return true;
        }

        public Task<Point> GetPositionAsync()
        {
            return Task.FromResult(Map.GetPin(Pin));
        }

        public Task<Orientation> GetOrientation()
        {
            return Task.FromResult(Orientation);
        }
    }
}