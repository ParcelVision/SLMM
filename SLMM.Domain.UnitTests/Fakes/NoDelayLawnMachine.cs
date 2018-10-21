using System;
using SLMM.Domain.LawnMap;

namespace SLMM.Domain.UnitTests.Fakes
{
    public class NoDelayLawnMachine : LawnMowingMachine
    {
        public NoDelayLawnMachine(ILawnMap map, Position startPosition) : base(map, startPosition)
        {
            TestAdvanceDelay = TimeSpan.Zero;
            TestTurnDelay = TimeSpan.Zero;
        }

        public TimeSpan TestAdvanceDelay
        {
            get => AdvanceDelay;
            set => AdvanceDelay = value;
        }

        public TimeSpan TestTurnDelay
        {
            get => TurnDelay;
            set => TurnDelay = value;
        }
    }
}