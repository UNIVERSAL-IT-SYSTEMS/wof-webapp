using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point_ns;

namespace Calculator_ns
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
            return Math.Sqrt(Math.Pow(a.getX() - b.getX(), 2) + Math.Pow(a.getY() - b.getY(), 2));
        }
    }
}
