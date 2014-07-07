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

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /**
        public float getX()
        {
            return x;
        }

        public float getY()
        {
            return y;
        }

        public void setX(float x)
        {
            this.x = x;
        }

        public void setY(float y)
        {
            this.y = y;
        }
        */
        //switch to SVG library with SVG point and matricies?
        public void translate(float translateX, float translateY) 
        {
            x += translateX;
            y += translateY;

        }

        public void rotate(double angle)
        {
            double temp_x = Math.Cos(angle) * x - Math.Sin(angle) * y;
            double temp_y = Math.Sin(angle) * x + Math.Cos(angle) * y;
            x = (float)temp_x;
            y = (float)temp_y;
        }

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
        public static bool operator== (Point a, Point b){

            return (CoordinateCalculator.hasDifferenceLessThan(a.X, b.X, 0.0001) 
                && CoordinateCalculator.hasDifferenceLessThan(a.Y, b.Y, 0.0001));
                
        }

        public static bool operator!= (Point a, Point b)
        {
            return !(a == b);

        }


        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Point p = (Point)obj;
            return (CoordinateCalculator.hasDifferenceLessThan(this.X, p.X, 0.0001)
                && CoordinateCalculator.hasDifferenceLessThan(this.Y, p.Y, 0.0001));
        }


    }
}
