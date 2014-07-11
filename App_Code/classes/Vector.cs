using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class Vector
    {
        private double x, y;
        public double X
        {
            get { return x; }
        }
        public double Y
        {
            get { return y; }
        }
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double dotProduct(Vector v)
        {
            return (this.X * v.X) + (this.Y * v.Y);
        }

        public double scalarCrossProduct(Vector v)
        {
            return (this.X * v.Y) - (this.Y * v.X);
        }

        public double magnitude()
        {
            double sumOfSquares = (this.X * this.X)+ (this.Y * this.Y);
            return Math.Sqrt(sumOfSquares);
        }

        public double radianAngleTo(Vector v)
        {
            return Math.Atan2(this.scalarCrossProduct(v), this.dotProduct(v));
        }

        public double degreeAngleTo(Vector v)
        {
            return radianAngleTo(v) * 180 / Math.PI;
        }
    }
}
