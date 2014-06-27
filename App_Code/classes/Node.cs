using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    public class Node: Point
    {
        private double officeLocation;
        public double OfficeLocation
        {
            get { return officeLocation; }
            set { officeLocation = value; }
        }
        private List<Edge> edges;
        public List<Edge> Edges
        {
            get { return edges; }
        }


        public Node()
            : base()
        {
            officeLocation = -1;
            edges = new List<Edge>();
        }
        public Node(int officeLocation, double x, double y): base(x, y)
        {
            this.OfficeLocation = officeLocation;

        }

        public void addEdge(Edge e)
        {
            edges.Add(e);
        }
    }
}
