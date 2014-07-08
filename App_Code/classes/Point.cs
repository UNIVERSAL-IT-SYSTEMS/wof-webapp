using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace PathFinding
{
    public class Point: Object
    {
        private float x, y;

        //X-Coordinate property
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        //Y-Coordinate property
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /**
         * Initializes a point instance with X-Coordinate equal to x, and Y-Coordinate equal to y.
         * 
         * @param x is the float that becomes the X-Coordinate of the new Point instance.
         * @param y is the float that becomes the Y-Coordinate of the new Point instance.
         */
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /**
         * Shifts the point by translateX and translateY.
         * 
         * @param translateX is the amount the point should be shifted in the X direction.
         * @param translateY is the amount the point should be shifted in the Y direction.
         */
        public void translate(float translateX, float translateY) 
        {
            x += translateX;
            y += translateY;

        }

        /**
         * Shifts the point by rotating it about the origin by angle degrees.
         * (Imagine a line segment from the point to the origin, and rotating
         * that line segment angle degrees. This method just updates the new 
         * position of our point after being rotated.)
         * 
         * @param angle the amount by which the point is rotated about the origin (specified in degrees).
         */
        public void rotate(double angle)
        {
            double radianAngle = angle * Math.PI / 180;
            double temp_x = Math.Cos(angle) * x - Math.Sin(angle) * y;
            double temp_y = Math.Sin(angle) * x + Math.Cos(angle) * y;
            x = (float)temp_x;
            y = (float)temp_y;
        }

        /**
         * Translates and then rotates the point.
         * 
         * @param translateX the amount by which the point is shifted in the X-direction.
         * @param translateY the amount by which the piont is shifted in the Y-direction.
         * @param angle the amount by which the piont is rotated about the origin (specified in degrees).
         */
        public void transform(float translateX, float translateY, float angle)
        {
            Matrix transformation = new Matrix();
            transformation.Translate(translateX, translateY);
            transformation.Rotate(angle);
            System.Drawing.PointF[] points = { new System.Drawing.PointF(x, y) };
            transformation.TransformPoints(points);
            x = points[0].X;
            y = points[0].Y;

        }
        /**
         * Overrides the Equals function. For two points to be equal, both the
         * X and Y must be within a certain epsilon of each other.
         * 
         * @param obj the Object being compared to this instance.
         * @return whether obj has the same coordinates as this instance.
         */
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Point p = (Point)obj;
            return (CoordinateCalculator.hasDifferenceLessThan(this.X, p.X, 0.0001)
                && CoordinateCalculator.hasDifferenceLessThan(this.Y, p.Y, 0.0001));
        }


        /**
         * Overrides == to return Equals.
         */
        public static bool operator == (Point a, Point b){

            return a.Equals(b);
                
        }

        /**
         * Overrides != to return the opposite of Equals.
         */
        public static bool operator!= (Point a, Point b)
        {
            return !a.Equals(b);

        }

    }
}
