using System;
using System.Drawing;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SLMM.Common.Navigation;
using SLMM.Core.Devices;
using SLMM.Core.Driving;

namespace SLMM.Tests
{
    public class LawnMowerDriverTests
    {
        private const int MapWidth = 100;
        private const int MapHeight = 100;

        private const int StartPositionX = 0;
        private const int StartPositionY = 0;

        private const Orientation StartOrientation = Orientation.North;
        private ILawnMowerDriver _driver;
        private ILawnMower _mower;


        [SetUp]
        public void Setup()
        {
            _mower = new VirtualLawnMower
            {
                TurnDuration = TimeSpan.Zero,
                AvanceDuration = TimeSpan.Zero
            };
            _driver = new VirtualLawnMowerDriver(_mower, new Size(MapWidth, MapHeight),
                new Point(StartPositionX, StartPositionY), StartOrientation);
        }

        [Test]
        public async Task Driver_Mower_CanGetPosition()
        {
            var position = await _driver.GetPositionAsync();
            position.Should().Be(Point.Empty);

            var orientation = await _driver.GetOrientation();
            orientation.Should().Be(Orientation.North);
        }


        [Test]
        public async Task Driver_Mower_CanMove()
        {
            await _driver.AdvanceAsync();

            var position = await _driver.GetPositionAsync();
            position.Y.Should().Be(1);
        }


        [Test]
        public async Task Driver_Mower_CanTurnClockwise()
        {
            await _driver.TurnAsync(TurnDirection.Clockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.East);

            await _driver.TurnAsync(TurnDirection.Clockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.South);

            await _driver.TurnAsync(TurnDirection.Clockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.West);

            await _driver.TurnAsync(TurnDirection.Clockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.North);

            await _driver.TurnAsync(TurnDirection.Clockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.East);
        }


        [Test]
        public async Task Driver_Mower_CanTurnAnticlockwise()
        {
            await _driver.TurnAsync(TurnDirection.Anticlockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.West);

            await _driver.TurnAsync(TurnDirection.Anticlockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.South);

            await _driver.TurnAsync(TurnDirection.Anticlockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.East);

            await _driver.TurnAsync(TurnDirection.Anticlockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.North);

            await _driver.TurnAsync(TurnDirection.Anticlockwise);
            (await _driver.GetOrientation()).Should().Be(Orientation.West);
        }


        [Test]
        public async Task Driver_Mower_CanDriveEntireMap()
        {
            for (var i = 0; i < MapHeight - 1; i++) (await _driver.AdvanceAsync()).Should().BeTrue();

            await _driver.TurnAsync(TurnDirection.Clockwise);

            for (var i = 0; i < MapWidth - 1; i++) (await _driver.AdvanceAsync()).Should().BeTrue();

            await _driver.TurnAsync(TurnDirection.Clockwise);
            for (var i = 0; i < MapHeight - 1; i++) (await _driver.AdvanceAsync()).Should().BeTrue();

            await _driver.TurnAsync(TurnDirection.Clockwise);
            for (var i = 0; i < MapWidth - 1; i++) (await _driver.AdvanceAsync()).Should().BeTrue();

            var position = await _driver.GetPositionAsync();
            position.X.Should().Be(0);
            position.Y.Should().Be(0);

            var orientation = await _driver.GetOrientation();
            orientation.Should().Be(Orientation.West);
        }
    }
}