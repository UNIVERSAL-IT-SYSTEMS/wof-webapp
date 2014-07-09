using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PathFinding
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
        public Point crosses(Line line, double epsilon)
        {
            if (CoordinateCalculator.hasDifferenceLessThan(end_pt.X, start_pt.X, epsilon)
                && !CoordinateCalculator.hasDifferenceLessThan(line.end_pt.X, line.start_pt.X, epsilon))
            {
                float crosing_x = end_pt.X;
                float crossing_y = line.getSlope() * crosing_x + line.getConstant();
                Point crossing_pt = new Point(crosing_x, crossing_y);
                if (this.containsInVector(crossing_pt, epsilon) && line.containsInVector(crossing_pt, epsilon))
                    return crossing_pt;
            }
            else if (!CoordinateCalculator.hasDifferenceLessThan(end_pt.X, start_pt.X, epsilon)
                && CoordinateCalculator.hasDifferenceLessThan(line.end_pt.X, line.start_pt.X, epsilon))
            {
                float crosing_x = line.end_pt.X;
                float crossing_y = getSlope() * crosing_x + getConstant();
                Point crossing_pt = new Point(crosing_x, crossing_y);
                if (this.containsInVector(crossing_pt, epsilon) && line.containsInVector(crossing_pt, epsilon))
                    return crossing_pt;
            }
            else if ((CoordinateCalculator.hasDifferenceLessThan(end_pt.X, start_pt.X, epsilon)
                && CoordinateCalculator.hasDifferenceLessThan(line.end_pt.X, line.start_pt.X, epsilon)) 
                || (this.getSlope() == line.getSlope()))
            {
                if (start_pt.isCloseTo(line.getStartPoint(), epsilon)
                    || start_pt.isCloseTo(line.getEndPoint(), epsilon))
                    return start_pt;
                else if (end_pt.isCloseTo(line.getEndPoint(), epsilon)
                    || end_pt.isCloseTo(line.getStartPoint(), epsilon))
                    return end_pt;
            }
            else  
            {

                float crosing_x = (line.getConstant() - this.getConstant()) / (this.getSlope() - line.getSlope());
                float crossing_y = this.getSlope() * crosing_x + this.getConstant();
                Point crossing_pt = new Point(crosing_x, crossing_y);
                if (this.containsInVector(crossing_pt, epsilon) && line.containsInVector(crossing_pt, epsilon))
                    return crossing_pt;
               
            }       

            return null;
        }

        public float getSlope()
        {
            if (end_pt.X == start_pt.X)
                throw new Exception();

            return (end_pt.Y - start_pt.Y) / (end_pt.X - start_pt.X);
        }

        public float getConstant()
        {
            return (end_pt.Y - (end_pt.X * getSlope()));
        }



        /**
         * checks if the point known to be on a geometric infinite line is in the interval set by the given vector (finite "line" with endpoints)
         */
        private bool containsInVector(Point point, double epsilon)
        {
            return ((CoordinateCalculator.isBetween(point.X, this.getStartPoint().X, this.getEndPoint().X, epsilon)
                || CoordinateCalculator.isBetween(point.X, this.getEndPoint().X, this.getStartPoint().X, epsilon))
                && (CoordinateCalculator.isBetween(point.Y, this.getStartPoint().Y, this.getEndPoint().Y, epsilon)
                || CoordinateCalculator.isBetween(point.Y, this.getEndPoint().Y, this.getStartPoint().Y, epsilon)));
        }

        public bool contains(Point point, double epsilon)
        {
            if (end_pt.X == start_pt.X)
            {
                return (CoordinateCalculator.hasDifferenceLessThan(point.X, start_pt.X, 0.001)
                    && containsInVector(point, epsilon));
            }
            else
            {
                float testY = getSlope() * point.X + getConstant();
                return (CoordinateCalculator.hasDifferenceLessThan(point.Y, testY, epsilon)
                    && containsInVector(point, epsilon));
            }
        }
    }
}
