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
            Console.WriteLine("Done with Edge tests.");

            //Testing Path
            Console.WriteLine("Starting Path tests.");
            //Path tests
            Console.WriteLine("Done with Path tests.");

            //Testing DjikstraPathFinder
            Console.WriteLine("Starting DjikstraPathFinder tests.");
            DjikstraPathFinder pathFinder = new DjikstraPathFinder();
            Edge cb = new Edge(c, b, 5);
            //pathFinder.findPath(n, n);
            Console.WriteLine("Done with DjikstraPathFinder tests.");


            //So it doesn't close the window until I hit enter.
            Console.ReadLine();
        }
    }
}
