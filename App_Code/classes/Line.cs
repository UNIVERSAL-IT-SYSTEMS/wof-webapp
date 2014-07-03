using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point_ns;
using Calculator_ns;

namespace Line_ns
{
    public class Line
    {
        private Point start_pt;
        private Point end_pt;
        private int officeNumber;

        public Line(Point start_pt, Point end_pt)
        {
            this.start_pt = start_pt;
            this.end_pt = end_pt;
            this.officeNumber = -1;
        }

        public Line(Point start_pt, Point end_pt, int officeNumber)
        {
            this.start_pt = start_pt;
            this.end_pt = end_pt;
            this.officeNumber = officeNumber;
        }


        public Point getStartPoint()
        {
            return start_pt;
        }

        public Point getEndPoint()
        {
            return end_pt;
        }

        public int getOfficeNumber()
        {
            return officeNumber;
        }

        public void setStartPoint(Point start_pt)
        {
            this.start_pt = start_pt;
        }

        public void setEndPoint(Point end_pt)
        {
            this.end_pt = end_pt;
        }

        public void setOfficeNumber(int number)
        {
            officeNumber = number;
        }

        public void translate(float translateX, float translateY)
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
            if (CoordinateCalculator.hasDifferenceLessThan(end_pt.getX(), start_pt.getX(), 0.1) 
                && !CoordinateCalculator.hasDifferenceLessThan(line.end_pt.getX(), line.start_pt.getX(), 0.1))
            {
                float crosing_x = end_pt.getX();
                float crossing_y = line.getSlope() * crosing_x + line.getConstant();
                Point crossing_pt = new Point(crosing_x, crossing_y);
                if (this.contains(crossing_pt) && line.contains(crossing_pt))
                    return crossing_pt;
            }
            else if (!CoordinateCalculator.hasDifferenceLessThan(end_pt.getX(), start_pt.getX(), 0.1) 
                && CoordinateCalculator.hasDifferenceLessThan(line.end_pt.getX(), line.start_pt.getX(), 0.1))
            {
                float crosing_x = line.end_pt.getX();
                float crossing_y = getSlope() * crosing_x + getConstant();
                Point crossing_pt = new Point(crosing_x, crossing_y);
                if (this.contains(crossing_pt) && line.contains(crossing_pt))
                    return crossing_pt;
            }
            else if ((CoordinateCalculator.hasDifferenceLessThan(end_pt.getX(), start_pt.getX(), 0.1)
                && CoordinateCalculator.hasDifferenceLessThan(line.end_pt.getX(), line.start_pt.getX(), 0.1)) 
                || (this.getSlope() == line.getSlope()))
            {
                if (start_pt == line.getStartPoint() || start_pt == line.getEndPoint())
                    return start_pt;
                else if (end_pt == line.getEndPoint() || end_pt == line.getStartPoint())
                    return end_pt;
            }
            else  
            {
                float crosing_x = (line.getConstant() - this.getConstant()) / (this.getSlope() - line.getSlope());
                float crossing_y = this.getSlope() * crosing_x + this.getConstant();
                Point crossing_pt = new Point(crosing_x, crossing_y);
                if (this.contains(crossing_pt) && line.contains(crossing_pt))
                    return crossing_pt;
               
            }       

            return null;
        }

        public float getSlope()
        {
            if (end_pt.getX() == start_pt.getX())
                throw new Exception();

            return (end_pt.getY() - start_pt.getY()) / (end_pt.getX() - start_pt.getX());
        }

        public float getConstant()
        {
            return (end_pt.getY() - (end_pt.getX() * getSlope()));
        }

        public bool contains(Point point)
        {
            return ((CoordinateCalculator.isBetween(point.getX(), this.getStartPoint().getX(), this.getEndPoint().getX())
                || CoordinateCalculator.isBetween(point.getX(), this.getEndPoint().getX(), this.getStartPoint().getX()))
                && (CoordinateCalculator.isBetween(point.getY(), this.getStartPoint().getY(), this.getEndPoint().getY())
                || CoordinateCalculator.isBetween(point.getY(), this.getEndPoint().getY(), this.getStartPoint().getY())));
        }
    }
}
