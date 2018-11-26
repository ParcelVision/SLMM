using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SLMM.Common.Navigation;
using SLMM.Core.Devices;

namespace SLMM.Tests
{
    public class LawnMowerTests
    {
        private ILawnMower _mower;


        [SetUp]
        public void Setup()
        {
            _mower = new VirtualLawnMower();
        }

        [Test]
        public async Task Mower_IdleOnStart()
        {
            var status = await _mower.GetStatusAsync();
            status.Should().Be(OperationStatus.Idle);
        }


        [Test]
        public async Task Mower_Concurrency_CannotOperateWhileNotIdle()
        {
            _mower.TurnAsync(TurnDirection.Clockwise);
            var status = await _mower.GetStatusAsync();
            status.Should().Be(OperationStatus.Turning);
            var turn = await _mower.TurnAsync(TurnDirection.Clockwise);
            turn.Should().Be(false);
        }
    }
}