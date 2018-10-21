using SLMM.Domain;
using SLMM.Domain.LawnMap;

namespace SLMM.Api.Requests
{
    public class PositionResponse
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Orientation Orientation { get; set; }
    }
}