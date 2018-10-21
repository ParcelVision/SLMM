using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SLMM.Domain.LawnMap;
using SLMM.Domain.UnitTests.Fakes;

namespace SLMM.Domain.UnitTests
{
    [TestFixture]
    public class LawnMowingMachineTests
    {
        private Mock<ILawnMap> _lawnMock;
        private NoDelayLawnMachine _machine;
        private bool _canMoveToAnyPosition;
        private Position _newPosition;

        [SetUp]
        public void Setup()
        {
            _canMoveToAnyPosition = true;
            _newPosition = new Position(new Point(1, 1), Orientation.North);

            _lawnMock = new Mock<ILawnMap>();

            _lawnMock.Setup(s => s.CanMove(It.IsAny<Point>())).Returns(() => _canMoveToAnyPosition);
            _lawnMock.Setup(s => s.CanMove(It.IsAny<Position>(), It.IsAny<int>())).Returns(() => _canMoveToAnyPosition);
            _lawnMock.Setup(s => s.TryMove(It.IsAny<Position>(), It.IsAny<int>(), out _newPosition)).Returns(() => _canMoveToAnyPosition);

            _machine = new NoDelayLawnMachine(_lawnMock.Object, new Position());
        }

        [Test]
        public void Check_Start_Position_IsWithinMap()
        {
            _canMoveToAnyPosition = false;

            // ReSharper disable once ObjectCreationAsStatement
            Action ctor = () => new LawnMowingMachine(_lawnMock.Object, new Position());

            ctor.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public async Task Advance_Updates_CurrentPosition()
        {
            await _machine.AdvanceAsync(1);

            _machine.Position.Should().Be(_newPosition);
        }

        [Test]
        public void Advance_AtLeast_OneStep()
        {
            Func<Task> advanceAsync = () => _machine.AdvanceAsync(0);

            advanceAsync.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public async Task Advance_NoParams_Moves_OneStep()
        {
            await _machine.AdvanceAsync();

            _lawnMock.Verify(s => s.TryMove(It.IsAny<Position>(), 1, out _newPosition));
        }

        [Test]
        public void Advance_Throws_OnInvalid_Location()
        {
            _canMoveToAnyPosition = false;

            Func<Task> advanceAsync = () => _machine.AdvanceAsync();

            advanceAsync.Should().Throw<InvalidOperationException>();
        }

        delegate bool TryMove(in Position position, int steps, out Position newPosition);

        [Test]
        public async Task Advance_Queues_Work()
        {
            var calls = new List<int>();
            var sync = TaskContinuationOptions.ExecuteSynchronously;

            Position _;

            _lawnMock.Setup(s => s.TryMove(It.IsAny<Position>(), It.IsAny<int>(), out _)).Returns(true);

            await Task.WhenAll(
                _machine.AdvanceAsync(1).ContinueWith(t => calls.Add(1), sync),
                _machine.AdvanceAsync(2).ContinueWith(t => calls.Add(2), sync),
                _machine.AdvanceAsync(4).ContinueWith(t => calls.Add(4), sync),
                _machine.AdvanceAsync(6)).ContinueWith(t => calls.Add(6), sync);

            calls.ToArray().Should().BeEquivalentTo(new[] {1, 2, 4, 6});
        }

        [Test]
        public void Advance_DoesNotUpdate_Position_OnCancelled()
        {
            _machine.TestAdvanceDelay = TimeSpan.FromMilliseconds(50);

            Func<Task> advanceAsync = () => _machine.AdvanceAsync(1, new CancellationTokenSource(5).Token);

            advanceAsync.Should().Throw<TaskCanceledException>();
        }

        [TestCase(LawnMowingMachine.TurnDegrees, new[] {Orientation.East, Orientation.South, Orientation.West, Orientation.North})]
        [TestCase(-LawnMowingMachine.TurnDegrees, new[] {Orientation.West, Orientation.South, Orientation.East, Orientation.North})]
        public async Task Turn_Updates_Orientation(int degrees, Orientation[] expected)
        {
            foreach (var orientation in expected)
            {
                await _machine.TurnAsync((short) degrees);

                _machine.Position.Orientation.Should().Be(orientation);
            }
        }

        [Test]
        public async Task Turn_Keeps_Position()
        {
            var original = _machine.Position;

            await _machine.TurnAsync(LawnMowingMachine.TurnDegrees);

            _machine.Position.Coordinates.Should().Be(original.Coordinates);
        }

        [TestCase(new[] {0, 270, -270, 360, -360})]
        public void Turn_Validates_Degrees(int[] degrees)
        {
            foreach (var degree in degrees)
            {
                Func<Task> turnAsync = () => _machine.TurnAsync((short) degree);

                turnAsync.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _machine.Dispose();
        }
    }
}