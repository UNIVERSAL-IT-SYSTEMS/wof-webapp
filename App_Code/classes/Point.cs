using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point_ns
{
    public class Point
    {
        
        private double x = 0.0;

        private double y = 0.0;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public void setX(double x)
        {
            this.x = x;
        }

        public void setY(double y)
        {
            this.y = y;
        }

        //switch to SVG library with SVG point and matricies?
        public void translate(double translateX, double translateY) 
        {
            x += translateX;
            y += translateY;

        }

        public void rotate(double angle)
        {
            double temp_x = Math.Cos(angle) * x - Math.Sin(angle) * y;
            double temp_y = Math.Sin(angle) * x + Math.Cos(angle) * y;
            x = temp_x;
            y = temp_y;
        }
    }
}
