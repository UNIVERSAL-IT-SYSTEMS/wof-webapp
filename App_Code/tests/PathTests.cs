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
            Assert.Fail();
        }
    }
}
