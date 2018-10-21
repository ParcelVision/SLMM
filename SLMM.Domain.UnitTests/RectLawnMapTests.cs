using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SLMM.Domain.LawnMap;

namespace SLMM.Domain.UnitTests
{
    [TestFixture]
    public class RectLawnMapTests
    {
        private readonly RectLawnMap _lawnMap;
        private const int LawnWidth = 10;
        private const int LawnHeight = 20;

        public RectLawnMapTests()
        {
            _lawnMap = new RectLawnMap(new Rectangle(Point.Empty, new Size(LawnWidth, LawnHeight)));
        }

        [Test]
        public void Map_Geometry_IsInitialized_Correctly()
        {
            _lawnMap.Geometry.Width.Should().Be(LawnWidth);
            _lawnMap.Geometry.Height.Should().Be(LawnHeight);
        }

        [TestCase(0, 0, 2, Orientation.North, Orientation.East)]
        [TestCase(0, LawnHeight - 1, 2, Orientation.South, Orientation.East)]
        [TestCase(LawnWidth - 1, LawnHeight - 1, 2, Orientation.South, Orientation.West)]
        [TestCase(LawnWidth - 1, 0, 2, Orientation.West, Orientation.North)]
        public void Cannot_Move_OutsideOf_Lawn(int x, int y, int steps, params Orientation[] possibleOrientation)
        {
            Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Where(o =>
            {
                var position = new Position(new Point(x, y), o);

                var tryMove = _lawnMap.TryMove(position, steps, out _);
                var canMove = _lawnMap.CanMove(position, steps);

                tryMove.Should().Be(canMove);

                return tryMove;
            }).ToArray().Should().BeEquivalentTo(possibleOrientation ?? new Orientation[0]);
        }

        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        public void Throws_OnInvalid_CurrentPosition(int x, int y)
        {
            var position = new Position(new Point(x, y), Orientation.North);

            Action tryMove = () => _lawnMap.TryMove(position, 0, out _);
            Action canMove = () => _lawnMap.CanMove(position, 0);

            tryMove.Should().Throw<ArgumentOutOfRangeException>();
            canMove.Should().Throw<ArgumentOutOfRangeException>();
        }


        [Test]
        public void Cannot_Make_Negative_Steps()
        {
            var position = new Position(new Point(0, 0), Orientation.North);

            Action tryMove = () => _lawnMap.TryMove(position, -1, out _);
            Action canMove = () => _lawnMap.CanMove(position, -1);

            tryMove.Should().Throw<ArgumentOutOfRangeException>();
            canMove.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void TryMove_Returns_NewPosition()
        {
            var position = new Position(new Point(1, 1), Orientation.North);

            _lawnMap.TryMove(position, 1, out var newPosition);

            newPosition.Coordinates.Y.Should().Be(2);
        }
    }
}