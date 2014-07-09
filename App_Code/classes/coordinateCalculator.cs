using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public static class CoordinateCalculator
    {
        public static double getScale(Point start_pt, Point end_pt, double length)
        {
            if (length == 0)
                return 0;

            double scale = euclideanDistance(start_pt, end_pt) / length;
            return scale;
        }



        public static double euclideanDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static bool hasDifferenceLessThan(float a, float b, double difference)
        {
            return (Math.Abs(a - b) < difference);
        }

        public static bool isBetween(float coord, float coord_a, float coord_b, double epsilon)
        {
            return ((coord > coord_a || CoordinateCalculator.hasDifferenceLessThan(coord, coord_a, epsilon))
                    && (coord < coord_b || CoordinateCalculator.hasDifferenceLessThan(coord, coord_b, epsilon)));
        }

    }
}
