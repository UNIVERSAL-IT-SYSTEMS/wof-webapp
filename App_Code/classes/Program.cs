using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            //Point tests
            Console.WriteLine("Starting Point tests.");
            Point p = new Point();
            if (p.X != 0) Console.WriteLine("Point instanciated without x and y coordinates should have x set to zero.");
            if (p.Y != 0) Console.WriteLine("Point instanciated without x and y coordinates should have y set to zero.");
            p.X = 1;
            if (p.X != 1) Console.WriteLine("X-Coordinate not being set properly. Should be 1. Returning " + p.X);
            p.Y = 2;
            if (p.Y != 2) Console.WriteLine("Y-Coordinate not being set properly. Should be 2. Returning " + p.Y);
            p = new Point(3.0, 4.0);
            if (p.X != 3.0) Console.WriteLine("X-Coordinate not being set properly. Should be 3. Returning " + p.X);
            if (p.Y != 4.0) Console.WriteLine("Y-Coordinate not being set properly. Should be 4. Returning " + p.Y);
            Console.WriteLine("Done with Point tests.");

            //Node tests
            Console.WriteLine("Starting Node tests.");
            Node n = new Node();
            if (n.X != 0) Console.WriteLine("Node instanciated without parameters should have x set to zero.");
            if (n.Y != 0) Console.WriteLine("Node instanciated without parameters should have y set to zero.");
            if (n.OfficeLocation.ToString() != n.ToString()) Console.WriteLine("ToString() should print out the office number.");
            if (n.OfficeLocation != -1) Console.WriteLine("Node instanciated without parameters should have OfficeLocation set to -1.");
            n.X = 1;
            if (n.X != 1) Console.WriteLine("X-Coordinate not being set properly. Should be 1. Returning " + n.X);
            n.Y = 2;
            if (n.Y != 2) Console.WriteLine("Y-Coordinate not being set properly. Should be 2. Returning " + n.Y);
            n.OfficeLocation = 5;
            if (n.OfficeLocation != 5) Console.WriteLine("Y-Coordinate not being set properly. Should be 5. Returning " + n.OfficeLocation);
            n = new Node(7424, 3.0, 4.0);
            if (n.X != 3.0) Console.WriteLine("X-Coordinate not being set properly. Should be 3. Returning " + n.X);
            if (n.Y != 4.0) Console.WriteLine("Y-Coordinate not being set properly. Should be 4. Returning " + n.Y);
            if (n.OfficeLocation != 7424) Console.WriteLine("OfficeLocation not being set properly. Should be 7424. Returning " + n.OfficeLocation);
            Console.WriteLine("Should be 7424: " + n);
            Console.WriteLine("Done with Node tests.");

            //Edge Tests
            Console.WriteLine("Starting Edge tests.");
            Node a = new Node();
            Node b = new Node();
            Edge ab = new Edge(a, b, 20);
            if (ab.otherNode(a) != b) Console.WriteLine("Othernode not working. AB.otherNode(A) should be B.");
            if (ab.otherNode(b) != a) Console.WriteLine("Othernode not working. AB.otherNode(B) should be A.");
            if (ab.Weight != 20.0) Console.WriteLine("Edge weight not being set correctly.");
            if (!a.Edges.Contains(ab)) Console.WriteLine("Edge AB not added to A.Edges.");
            if (!b.Edges.Contains(ab)) Console.WriteLine("Edge AB not added to B.Edges.");
            Node c = new Node();
            Edge ac = new Edge(a, c, 10);
            if (!a.Edges.Contains(ac)) Console.WriteLine("Edge AC not added to A.Edges.");
            if (!a.Edges.Contains(ab)) Console.WriteLine("Edge AB no longer in A.Edges after AC added.");
            //test that AB == BA
            //test ToString()
            Console.WriteLine("Done with Edge tests.");

            //Testing Path
            Console.WriteLine("Starting Path tests.");
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
            //check that same results if adding path ba.
            //check that one of the nodes of edge to be added is the current lastNode of the path
            //test path from path
            //Console.WriteLine("Testing Path ToString().");

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
            if (a.ToString().CompareTo("Path: < (1) (3) (2) (4) >") == 0) Console.WriteLine("ToString() not working.");
            Console.WriteLine("Done with Path tests.");

            //Testing DjikstraPathFinder
            Console.WriteLine("Starting DjikstraPathFinder tests.");
            DjikstraPathFinder pathFinder = new DjikstraPathFinder();
            Path shortestPath = pathFinder.findPath(one, four);
            Console.WriteLine("Should be Path: < (1) (2) (4) >  ...  " + shortestPath);
            //pathFinder.findPath(n, n);
            Console.WriteLine("Done with DjikstraPathFinder tests.");


            //So it doesn't close the window until I hit enter.
            Console.ReadLine();
        }
    }
}
