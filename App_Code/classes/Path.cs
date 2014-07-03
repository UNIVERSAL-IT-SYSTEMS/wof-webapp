using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Node_ns;
using Edge_ns;

namespace ShortestPath
{
    public class Path : IComparable<Path>
    {
        private double cost;
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        private Node lastNode;
        public Node LastNode
        {
            get { return lastNode; }
            set { lastNode = value; }
        }

        private LinkedList<Node> listOfNodes;
        public LinkedList<Node> ListOfNodes
        {
            get { return listOfNodes; }
            set { listOfNodes = value; }
        }
        public Path(Node n)
        {
            this.Cost = 0;
            this.LastNode = n;
            listOfNodes = new LinkedList<Node>();
            listOfNodes.AddLast(n);
        }

        public Path(Path p)
        {
            this.Cost = p.Cost;
            this.LastNode = p.LastNode;
            listOfNodes = new LinkedList<Node>(p.ListOfNodes);
        }

        public void addEdgeToPath(Edge e)
        {
            Node newLastNode = e.otherNode(this.LastNode);
            this.Cost = this.Cost + e.Weight;
            this.LastNode = newLastNode;
            listOfNodes.AddLast(newLastNode);
        }

        public int CompareTo(Path p)
        {
            return this.Cost.CompareTo(p.Cost);
        }

        public override string ToString()
        {
            String s = "Path: <";
            foreach (Node n in listOfNodes)
            {
                s += " (" + n.ToString() + ") ";
            }
            s += ">";
            return s;
        }


    }
}
