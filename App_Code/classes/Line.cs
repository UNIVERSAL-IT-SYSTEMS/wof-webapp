using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point_ns;

namespace Line_ns
{
    public class Line
    {
        private Point start_pt;
        private Point end_pt;

        //need to create objects first?
        public Line(Point start_pt, Point end_pt)
        {
            this.start_pt = start_pt;
            this.end_pt = end_pt;
        }

        public Point getStartPoint()
        {
            return start_pt;
        }

        public Point getEndPoint()
        {
            return end_pt;
        }

        public void setStartPoint(Point start_pt)
        {
            this.start_pt = start_pt;
        }

        public void setEndPoint(Point end_pt)
        {
            this.end_pt = end_pt;
        }

        //switch to SVG library with SVG point and matricies?
        public void translate(double translateX, double translateY)
        {
            start_pt.translate(translateX, translateY);
            end_pt.translate(translateX, translateY);

        }

        public void rotate(double angle)
        {
            start_pt.rotate(angle);
            end_pt.rotate(angle);
        }

        //returns null if no cross, pt if crosses
        public Point crosses(Line line)
        {
            return null;
        }

        public double getSlope()
        {
            return (end_pt.getY() - start_pt.getY()) / (end_pt.getX() - start_pt.getX());
        }

        public double getConstant()
        {
            return (end_pt.getY() - (end_pt.getX() * getSlope()));
        }
    }
}
