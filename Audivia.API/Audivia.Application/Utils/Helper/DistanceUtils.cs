namespace Audivia.Application.Utils.Helper
{
    public class DistanceUtils
    {
        private const double EARTH_RADIUS = 6371;
        public static double CalculateDistance(double startLat, double startLong, double endLat, double endLong)
        {

            double dLat = ToRadians((endLat - startLat));
            double dLong = ToRadians((endLong - startLong));

            startLat = ToRadians(startLat);
            endLat = ToRadians(endLat);

            double a = Haversine(dLat) + Math.Cos(startLat) * Math.Cos(endLat) * Haversine(dLong);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EARTH_RADIUS * c;
        }

        private static double Haversine(double val)
        {
            return Math.Pow(Math.Sin(val / 2), 2);
        }

        private static double ToRadians(double deg)
        {
            return (Math.PI / 180) * deg;
        }
    }
}
