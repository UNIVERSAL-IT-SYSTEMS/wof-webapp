using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Node: Object
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
            return a.Equals(b);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Node n = (Node)obj;
            return (this.CrossingPoint.Equals(n.CrossingPoint)) && (this.OfficeLocation.Equals(n.OfficeLocation));
        }

        public static bool operator !=(Node a, Node b)
        {
            return (a.CrossingPoint != b.CrossingPoint);

        }
    }
}