using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Direction
    {
        private Point previousPoint, currentPoint, nextPoint;

        public double angle
        {
            get
            {
                return getDegreeAngle();
            }
        }

        private double _distance;
        public double distance
        {
            get { return _distance; }
        }


        public Direction(Point previousPoint, Point currentPoint, Point nextPoint)
        {
            this.previousPoint = previousPoint;
            this.currentPoint = currentPoint;
            this.nextPoint = nextPoint;
            _distance = CoordinateCalculator.euclideanDistance(currentPoint, nextPoint);
        }

        public Direction(Node previousNode, Node currentNode, Node nextNode)
        {
            this.previousPoint = previousNode.CrossingPoint;
            this.currentPoint = currentNode.CrossingPoint;
            this.nextPoint = nextNode.CrossingPoint;
            _distance = CoordinateCalculator.euclideanDistance(currentPoint, nextPoint);
        }

        /**
         * Order of edges matters!
         * 
         * @param edge1 is the edge that the robot is finishing. We need this to get current heading.
         * @param edge2 is the edge that the robot is traveling on next.
         */
        public Direction(Edge edge1, Edge edge2)
        {
            _distance = edge2.Weight;
            Node currentNode = edge1.commonNode(edge2);
            currentPoint = currentNode.CrossingPoint;
            previousPoint = edge1.otherNode(currentNode).CrossingPoint;
            nextPoint = edge2.otherNode(currentNode).CrossingPoint;
        }

        /**
         * Returns the negative angle because traditional coordinate positive and negative doesn't make sense for
         * the robot. With the robot, right is positive angle, left is negative angle.
         * 
         *            A'
         *            ^   C
         *            |  /
         *            | /
         *     _______|/______
         *            B
         *            |
         *            |
         *            |
         *            A
         *            
         * In the example graph above, given previous point A, current point B, and next point C,
         * current Heading would be the vector BA', and the new Heading would be the vector BC.
         * Degree angle would be 45* (not -45* as traditional coordinate system would dictate.) because
         * from here, the robot needs to go right 45*. 
         */
        private double getDegreeAngle(){
            //what if there is no angle1 (like for the first section of path?)
            if (previousPoint == null) { return 0; }//if there is no previousPoint (like at the start of a path), the robot doesn't have to turn first.
            Vector currentHeading = new Vector(currentPoint.X - previousPoint.X, currentPoint.Y - previousPoint.Y);
            Vector newHeading = new Vector(nextPoint.X - currentPoint.X, nextPoint.Y - currentPoint.Y);
            return -currentHeading.degreeAngleTo(newHeading);
        }
        private double getRadianAngle()
        {
            //what if there is no angle1 (like for the first section of path?)
            if (previousPoint == null) { return 0; }//if there is no previousPoint (like at the start of a path), the robot doesn't have to turn first.
            Vector currentHeading = new Vector(currentPoint.X - previousPoint.X, currentPoint.Y - previousPoint.Y);
            Vector newHeading = new Vector(nextPoint.X - currentPoint.X, nextPoint.Y - currentPoint.Y);
            return -currentHeading.radianAngleTo(newHeading);
        }

        public string getJSONDirection()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(this);
            return jsonString;
        }

        public override string ToString()
        {
            return getJSONDirection();
        }

        /**
         * Overrides the Equals function.
         * Returns true if the angle and distance values are equal.
         * 
         * @param obj the Object being compared to this instance.
         * @return whether obj has the same angle and distance as this instance.
         */

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Direction d = (Direction)obj;
            return (this.angle.Equals(d.angle) && this.distance.Equals(d.distance));
        }


        /**
         * Returns true if the x and y coordinates of two points are closer than epsilon
         * 
         * @param point the point to compare to
         * @param epsilon the acceptable error
         */
        public bool isCloseTo(Direction direction, double epsilon)
        {
            if (direction == null)
                return false;
            return (CoordinateCalculator.hasDifferenceLessThan((float)this.angle, (float)direction.angle, epsilon)
                && CoordinateCalculator.hasDifferenceLessThan((float)this.distance, (float)direction.distance, epsilon));
        }
            


    }
}
