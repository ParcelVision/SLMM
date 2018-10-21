using System.Drawing;

namespace SLMM.Domain.LawnMap
{
    public interface ILawnMap
    {
        bool CanMove(Point coordinates);
        bool CanMove(in Position position, int steps);
        bool TryMove(in Position position, int steps, out Position newPosition);
    }
}