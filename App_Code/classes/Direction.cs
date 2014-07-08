using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class Direction
    {
        private Edge edge1, edge2;
        
        private double angle;

        public double Angle
        {
            get {
                if (angle == NaN)
                {
                    angle = calculateAngle(edge1, edge2);
                }
                return angle;
            }
            //set { angle = value; }
        }

        private double distance;

        public double Distance
        {
            get {
                if (distance == NaN)
                {
                    distance = calculateDistance(edge1, edge2);
                }
                return distance; 
            }
            //set { distance = value; }
        }
        /**
         * Order of edges matters!
         * 
         * @param edge1 is the edge that the robot is finishing. We need this to get current heading.
         */
        public Direction(Edge edge1, Edge edge2)
        {
            this.edge1 = edge1;
            this.edge2 = edge2;
        }

        public Direction(double angle, double distance)
        {
            this.angle = angle;
            this.distance = distance;
        }

        private static double calculateAngle(Edge edge1, Edge edge2)
        {
            return 0.0;
        }
        private double calculateDistance(Edge edge1, Edge edge2)
        {
            return 0.0;
        }

    }
}
