using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point_ns;
using Edge_ns;


namespace Node_ns
{
    public class Node
    {
        private int officeLocation;

        public int OfficeLocation
        {
            get { return officeLocation; }
            set { officeLocation = value; }
        }

        private List<Edge> edges;

        public List<Edge> Edges
        {
            get { return edges; }
        }

        private Point crossing_pt;

        public Point CrossingPoint
        {
            get { return crossing_pt; }
            set { crossing_pt = value; }
        }

        public Node()
        {
            officeLocation = -1;
            edges = new List<Edge>();
            crossing_pt = new Point(0,0);
        }

        public Node(int officeLocation, float x, float y)
        {
            this.OfficeLocation = officeLocation;
            edges = new List<Edge>();
            crossing_pt = new Point(x, y);
        }

        public Node(int officeLocation, Point crossing_pt)
        {
            this.OfficeLocation = officeLocation;
            edges = new List<Edge>();
            this.crossing_pt = crossing_pt;
        }

        public void addEdge(Edge e)
        {
            edges.Add(e);
        }

        public override string ToString()
        {
            return this.OfficeLocation.ToString();
        }

        public static bool operator == (Node a, Node b)
        {
            return (a.CrossingPoint == b.CrossingPoint);

        }

        public bool Equals(Node other)
        {
            return (this == other);

        }

        public static bool operator !=(Node a, Node b)
        {
            return (a.CrossingPoint != b.CrossingPoint);

        }
    }
}