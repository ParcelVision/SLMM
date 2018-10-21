using System.Drawing;

namespace SLMM.Domain.LawnMap
{
    public interface IRectLawnMap : ILawnMap
    {
        Rectangle Geometry { get; set; }
    }
}