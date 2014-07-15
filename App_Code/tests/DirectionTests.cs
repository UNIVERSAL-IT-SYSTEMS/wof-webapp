using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PathFinding
{
    [TestClass]
    public class DirectionTests
    {
        [TestMethod]
        public void DirectionInitTests()
        {
            //Check initializing with two edges, three points, three nodes
            double expectedAngle = 90;
            double expectedDistance = 1;
            float x1, x2, x3, y1, y2, y3;
            x1 = 0;
            y1 = 0;
            x2 = 0;
            y2 = (float)expectedDistance;
            x3 = (float)expectedDistance;
            y3 = (float)expectedDistance;

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);
            Direction directionFromPoints = new Direction(p1, p2, p3);
            Assert.AreEqual(expectedAngle, directionFromPoints.angle, "Angle not being calculated correctly.");
            Assert.AreEqual(expectedDistance, directionFromPoints.distance, "Distance not being calculated correctly.");

            Node n1 = new Node(7421, x1, y1);
            Node n2 = new Node(7422, x2, y2);
            Node n3 = new Node(7423, x3, y3);
            Direction directionFromNodes = new Direction(n1, n2, n3);
            Assert.AreEqual(expectedAngle, directionFromNodes.angle, "Angle not being calculated correctly.");
            Assert.AreEqual(expectedDistance, directionFromNodes.distance, "Distance not being calculated correctly.");

            Edge e1 = new Edge(n1, n2, 3);
            Edge e2 = new Edge(n2, n3, 3);
            Direction directionFromEdges = new Direction(n1, n2, n3);
            Assert.AreEqual(expectedAngle, directionFromEdges.angle, "Angle not being calculated correctly.");
            Assert.AreEqual(expectedDistance, directionFromEdges.distance, "Distance not being calculated correctly.");
        }

        [TestMethod]
        public void GetAngleTest()
        {
            Point p1 = new Point(0, -1);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(1, 1);
            double expectedAngle = 45;

            Vector currentHeading = new Vector(p2.X - p1.X, p2.Y - p1.Y);
            Vector newHeading = new Vector(p3.X - p2.X, p3.Y - p2.Y);
            Assert.AreEqual(0, currentHeading.X, "Current heading x not correct.");
            Assert.AreEqual(1, currentHeading.Y, "Current heading y not correct.");
            Assert.AreEqual(1, newHeading.X, "New heading x not correct.");
            Assert.AreEqual(1, newHeading.Y, "New heading y not correct.");


            Direction directionFromPoints = new Direction(p1, p2, p3);
            Assert.AreEqual(expectedAngle, directionFromPoints.angle, "Angle 1 not being calculated correctly.");
            
            Point p4 = new Point(0, 1);
            expectedAngle = -135;
            directionFromPoints = new Direction(p2, p3, p4);
            Assert.AreEqual(expectedAngle, directionFromPoints.angle, "Angle 2 not being calculated correctly.");

            Point p5 = new Point(1, 1);
            expectedAngle = 180;
            directionFromPoints = new Direction(p3, p4, p5);
            Assert.AreEqual(expectedAngle, directionFromPoints.angle, "Angle 3 not being calculated correctly.");
        }

        [TestMethod]
        public void DistanceTest()
        {
            Point p1 = new Point(0, -1);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(1, 1);
            double expectedDistance = Math.Sqrt(2);
            Direction directionFromPoints = new Direction(p1, p2, p3);
            Assert.AreEqual(expectedDistance, directionFromPoints.distance, "Distance 1 not being calculated correctly.");
            
            Point p4 = new Point(0, 1);
            expectedDistance = 1;
            directionFromPoints = new Direction(p2, p3, p4);
            Assert.AreEqual(expectedDistance, directionFromPoints.distance, "Distance 2 not being calculated correctly.");

            Point p5 = new Point(1, 1);
            expectedDistance = 1;
            directionFromPoints = new Direction(p3, p4, p5);
            Assert.AreEqual(expectedDistance, directionFromPoints.distance, "Distance 3 not being calculated correctly.");
        }

        [TestMethod]
        public void DistanceJSONSerializationTest()
        {
            Point p1 = new Point(0, -1);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(1, 0);
            Direction direction = new Direction(p1, p2, p3);

            String expectedJSON = "{\"angle\":" + direction.angle +",\"distance\":" + direction.distance+ "}";
            String jsonDirection = direction.getJSONDirection();
            Assert.AreEqual(expectedJSON, jsonDirection, "Distance not being serialized to JSON correctly.");
        }

        [TestMethod]
        public void DirectionEqualsOverrideTest()
        {
            Assert.Fail();
        }
    }
}
