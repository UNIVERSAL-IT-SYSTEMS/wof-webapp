using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PathFinding
{
    public class Edge
    {
        private Node n1, n2;

        public Node N1
        {
            get { return n1; }
            //set { n1 = value; }
        }
        public Node N2
        {
            get { return n2; }
            //set { n2 = value; }
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            //set { weight = value; }
        }
        public Edge(Node n1, Node n2, double weight)
        {
            this.n1 = n1;
            this.n1.addEdge(this);
            this.n2 = n2;
            this.n2.addEdge(this);
            this.weight = weight;
        }

        public Node otherNode(Node n)
        {
            if (n == this.N1)
            {
                return this.N2;
            }
            if (n == this.N2)
            {
                return this.N1;
            }
            throw new Exception("Invalid Node passed into Edge.otherNode.");
        }

        public override string ToString()
        {
            return "Edge: [" + this.N1.ToString() + " - " + this.N2.ToString() + "]";
        }

        public static bool operator == (Edge a, Edge b)
        {
            return (a.N1 == b.N1 && a.N2 == b.N2) || (a.N1 == b.N2 && a.N2 == b.N1);
        }

        public static bool operator != (Edge a, Edge b)
        {
            return !(a == b);
        }
    }
}
