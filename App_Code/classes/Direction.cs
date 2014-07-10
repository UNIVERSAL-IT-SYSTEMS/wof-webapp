using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class Direction
    {
        private Point previousPoint, currentPoint, nextPoint;

        private double distance;
        public double Distance
        {
            get { return distance; }
        }
        public double Angle
        {
            get
            {
                return getAngle();
            }
        }

        public Direction(Point previousPoint, Point currentPoint, Point nextPoint)
        {
            this.previousPoint = previousPoint;
            this.currentPoint = currentPoint;
            this.nextPoint = nextPoint;
            distance = CoordinateCalculator.euclideanDistance(currentPoint, nextPoint);
        }

        public Direction(Node previousNode, Node currentNode, Node nextNode)
        {
            this.previousPoint = previousNode.CrossingPoint;
            this.currentPoint = currentNode.CrossingPoint;
            this.nextPoint = nextNode.CrossingPoint;
            distance = CoordinateCalculator.euclideanDistance(currentPoint, nextPoint);
        }

        /**
         * Order of edges matters!
         * 
         * @param edge1 is the edge that the robot is finishing. We need this to get current heading.
         * @param edge2 is the edge that the robot is traveling on next.
         */
        public Direction(Edge edge1, Edge edge2)
        {
            distance = edge2.Weight;
            Node currentNode = edge1.commonNode(edge2);
            currentPoint = currentNode.CrossingPoint;
            previousPoint = edge1.otherNode(currentNode).CrossingPoint;
            nextPoint = edge2.otherNode(currentNode).CrossingPoint;
        }

        private double getAngle(){
            //what if there is no angle1 (like for the first section of path?)
            if (previousPoint == null) { return 0; }//if there is no previousPoint (like at the start of a path), the robot doesn't have to turn first.
            Vector currentHeading = new Vector(currentPoint.X - previousPoint.X, currentPoint.Y - previousPoint.Y);
            Vector newHeading = new Vector(nextPoint.X - currentPoint.X, nextPoint.Y - currentPoint.Y);
            return currentHeading.radianAngleTo(newHeading);
        }



    }
}
