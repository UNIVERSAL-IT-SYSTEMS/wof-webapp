using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PathFinding
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void PathTest()
        {
            //Path Tests
            Console.WriteLine("Starting Path tests.\n");
            Node a = new Node();
            Node b = new Node();
            Edge ab = new Edge(a, b, 20);
            Node c = new Node();
            Edge ac = new Edge(a, c, 10);

            Path path = new Path(a);
            if (path.Cost != 0) Console.WriteLine("Initial path cost should be 0.");
            if (path.LastNode != a) Console.WriteLine("Path with single node A should have LastNode = A.");
            path.addEdgeToPath(ab);
            if (path.LastNode != b) Console.WriteLine("addEdgeToPath not editing path's LastNode correctly.");
            if (path.Cost != ab.Weight) Console.WriteLine("addEdgeToPath not updating path's cost correctly.");
            Path shorterPath = new Path(a);
            shorterPath.addEdgeToPath(ac);
            if (path.CompareTo(shorterPath) < 0) Console.WriteLine("Shorter path should be less than longer path when being compared.");
            Path equalPath = new Path(c);
            Node d = new Node();
            Edge equalEdge = new Edge(c, d, ab.Weight);
            equalPath.addEdgeToPath(equalEdge);
            if (path.CompareTo(equalPath) != 0) Console.WriteLine("Paths with the same cost should be equal.");
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
            Console.WriteLine("Testing toString() override. Should be 'Path: < (1) (3) (2) (4) >': " + longPath.ToString());
            Console.WriteLine("\nDone with Path tests.");

            Assert.Fail();
        }
    }
}
