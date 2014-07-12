using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PathFinding
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void PathFromNodeTest()
        {
            Node a = new Node();
            Node b = new Node();

            Path path = new Path(a);

            Assert.AreEqual(0, path.Cost, "Initial path cost should be 0.");
            Assert.AreEqual(a, path.LastNode, "Path with single node A should have LastNode = A.");
            //Test that list is accurate?
        }

        [TestMethod]
        public void PathFromPathTest()
        {
            Node a = new Node();
            Node b = new Node();
            Edge ab = new Edge(a, b, 20);
            Path path = new Path(a);
            path.addEdgeToPath(ab);

            Path newPath = new Path(path);
            Assert.AreEqual(path.Cost, newPath.Cost, "Initial path cost should be 0.");
            Assert.AreEqual(path.LastNode, newPath.LastNode, "Path with single node A should have LastNode = A.");
            Assert.AreNotSame(path, newPath);
            Assert.AreNotSame(path.ListOfNodes, newPath.ListOfNodes);
            //Test that list is accurate?
        }

        [TestMethod]
        public void AddEdgeToPathTest()
        {
            Node a = new Node();
            Node b = new Node();
            Edge ab = new Edge(a, b, 20);            
            Path path = new Path(a);
            path.addEdgeToPath(ab);

            Assert.AreEqual(b, path.LastNode, "addEdgeToPath not editing path's LastNode correctly.");
            Assert.AreEqual(ab.Weight, path.Cost, "Initial path cost should be 0.");
            //Test that list is accurate?
        }

        [TestMethod, ExpectedException(typeof(Exception), "addEdgeToPath should throw an exception when an edge is added that doesn't connect to the current last node of the path.")]
        public void InappropriateEdgeAddedToPath()
        {
            Node a = new Node();
            Node b = new Node();
            Node c = new Node();

            Edge ab = new Edge(a, b, 20);
            Edge ac = new Edge(a, c, 10);

            Path path = new Path(a);
            path.addEdgeToPath(ab);
            path.addEdgeToPath(ac);
        }

        [TestMethod]
        public void PathCompareToTest()
        {
            Node a = new Node();

            Path path = new Path(a);
            Node b = new Node();
            Edge ab = new Edge(a, b, 20);
            path.addEdgeToPath(ab);

            Path shorterPath = new Path(a);
            Node c = new Node();
            Edge ac = new Edge(a, c, 10);
            shorterPath.addEdgeToPath(ac);

            Path equalPath = new Path(c);
            Node d = new Node();
            Edge equalEdge = new Edge(c, d, ab.Weight);
            equalPath.addEdgeToPath(equalEdge);

            Assert.IsTrue(path.CompareTo(shorterPath) > 0, "Shorter path should be less than longer path when being compared.");
            Assert.IsTrue(path.CompareTo(equalPath) == 0, "Paths with the same cost should be equal when being compared.");
        }

        [TestMethod]
        public void PathToStringTest()
        {
            //Testing Path ToString()
            Node one = new Node(1, 0, 0);
            Node two = new Node(2, 0, 0);
            Node three = new Node(3, 0, 0);
            Node four = new Node(4, 0, 0);
            Edge onetwo = new Edge(one, two, 5);
            Edge onethree = new Edge(one, three, 10);
            Edge twothree = new Edge(two, three, 15);
            Edge twofour = new Edge(two, four, 20);
            Path longPath = new Path(one);
            //Console.WriteLine("LongPath: " + longPath);
            longPath.addEdgeToPath(onethree);
            //Console.WriteLine("LongPath: " + longPath);
            longPath.addEdgeToPath(twothree);
            //Console.WriteLine("LongPath: " + longPath);
            longPath.addEdgeToPath(twofour);
            //Console.WriteLine("LongPath: "+longPath);
            Assert.AreEqual("Path: < (1) (3) (2) (4) >", longPath.ToString(), "ToString override not working as expected.");
        }

        [TestMethod]
        public void GetDirectionsFromPathTest()
        {
            Point p1 = new Point(0, -1);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(1, 1);
            Point p4 = new Point(0, 1);
            Point p5 = new Point(1, 1);

            Node n1 = new Node(1, p1);
            Node n2 = new Node(2, p2);
            Node n3 = new Node(3, p3);
            Node n4 = new Node(4, p4);
            Node n5 = new Node(5, p5);

            Edge onetwo = new Edge(n1, n2, 3);
            Edge twothree = new Edge(n1, n2, 5);
            Edge threefour = new Edge(n1, n2, 7);
            Edge fourfive = new Edge(n1, n2, 9);

            Path p = new Path(n1);
            p.addEdgeToPath(onetwo);
            p.addEdgeToPath(twothree);
            p.addEdgeToPath(threefour);
            p.addEdgeToPath(fourfive);

            //LinkedList<Direction> listOfDirections = p.getListOfDirections();


            double expectedAngle = 45;

            Vector currentHeading = new Vector(p2.X - p1.X, p2.Y - p1.Y);
            Vector newHeading = new Vector(p3.X - p2.X, p3.Y - p2.Y);
            Assert.AreEqual(0, currentHeading.X, "Current heading x not correct.");
            Assert.AreEqual(1, currentHeading.Y, "Current heading y not correct.");
            Assert.AreEqual(1, newHeading.X, "New heading x not correct.");
            Assert.AreEqual(1, newHeading.Y, "New heading y not correct.");


            Direction directionFromPoints = new Direction(p1, p2, p3);
            Assert.AreEqual(expectedAngle, directionFromPoints.Angle, "Angle 1 not being calculated correctly.");
            
            expectedAngle = -135;
            directionFromPoints = new Direction(p2, p3, p4);
            Assert.AreEqual(expectedAngle, directionFromPoints.Angle, "Angle 2 not being calculated correctly.");

            expectedAngle = 180;
            directionFromPoints = new Direction(p3, p4, p5);
            Assert.AreEqual(expectedAngle, directionFromPoints.Angle, "Angle 3 not being calculated correctly.");
           
            Assert.Fail();
        }
    }
}
