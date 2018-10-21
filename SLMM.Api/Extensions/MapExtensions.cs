using SLMM.Api.Requests;
using SLMM.Domain;
using SLMM.Domain.LawnMap;

namespace SLMM.Api.Extensions
{
    public static class MapExtensions {
        public static T To<T>(this Position position) where T : PositionResponse, new()
        {
            return new T { Orientation = position.Orientation, X = position.Coordinates.X, Y = position.Coordinates.Y };
        }
    }
}