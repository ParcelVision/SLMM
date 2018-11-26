using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using SLMM.Common.Navigation;
using SLMM.Core.Navigation;

namespace SLMM.Tests
{
    public class MapTests
    {
        private const int MapWidth = 100;
        private const int MapHeight = 100;
        private Map _map;

        [SetUp]
        public void Setup()
        {
            _map = new Map(new Size(MapWidth, MapHeight));
        }

        [Test]
        public void Map_Area_IsSetCorrectly()
        {
            _map.Area.Width.Should().Be(MapWidth);
            _map.Area.Height.Should().Be(MapHeight);
        }


        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, -1)]
        public void Map_Pin_NegativeLocation(int x, int y)
        {
            var pin = Guid.NewGuid().ToString();
            Action tryPlace = () => _map.AddPin(pin, new Point(x, y));
            tryPlace.Should().Throw<ArgumentOutOfRangeException>();
        }


        [TestCase(0, MapHeight + 1)]
        [TestCase(MapWidth + 1, 0)]
        public void Map_Pin_Cannot_Place_OutsideMap(int x, int y)
        {
            var pin = Guid.NewGuid().ToString();
            Action tryPlace = () => _map.AddPin(pin, new Point(x, y));
            tryPlace.Should().Throw<ArgumentOutOfRangeException>();
        }


        [TestCase(0, 0, MapWidth + 1, Orientation.East)]
        [TestCase(0, 0, 1, Orientation.West)]
        [TestCase(0, 0, MapHeight + 1, Orientation.North)]
        [TestCase(0, 0, 1, Orientation.South)]
        public void Map_Pin_Cannot_Move_OutsideMap(int x, int y, int distance, Orientation orientation)
        {
            var pin = Guid.NewGuid().ToString();
            _map.AddPin(pin, new Point(x, y));

            var canMove = _map.CanMovePin(pin, distance, orientation);
            canMove.Valid.Should().BeFalse();

            var tryMove = _map.TryMovePin(pin, distance, orientation);
            tryMove.Should().BeFalse();
        }


        [TestCase(0, 0, MapWidth - 1, Orientation.East)]
        [TestCase(MapWidth - 1, 0, MapWidth - 1, Orientation.West)]
        [TestCase(0, 0, MapHeight - 1, Orientation.North)]
        [TestCase(0, MapHeight - 1, MapHeight - 1, Orientation.South)]
        public void Map_Pin_Can_Move(int x, int y, int distance, Orientation orientation)
        {
            var pin = Guid.NewGuid().ToString();
            _map.AddPin(pin, new Point(x, y));

            var canMove = _map.CanMovePin(pin, distance, orientation);
            canMove.Valid.Should().BeTrue();

            var tryMove = _map.TryMovePin(pin, distance, orientation);
            tryMove.Should().BeTrue();
        }
    }
}