using NetTopologySuite.Geometries;

namespace OPS_API.Resources
{
    public class MapCoordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public MapCoordinates() : this(0, 0) { }

        public MapCoordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public static MapCoordinates FromPoint(Point point)
            => new MapCoordinates(point.X, point.Y);
    }
}