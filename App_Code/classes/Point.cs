using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator_ns;
using System.Drawing.Drawing2D;

namespace Point_ns
{
    public class Point 
    {
        
        private float x;

        private float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

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

            return (CoordinateCalculator.hasDifferenceLessThan(a.getX(), b.getX(), 0.0001) 
                && CoordinateCalculator.hasDifferenceLessThan(a.getY(), b.getY(), 0.0001));
                
        }

        public static bool operator!= (Point a, Point b)
        {
            return !(a == b);

        }

    }
}
