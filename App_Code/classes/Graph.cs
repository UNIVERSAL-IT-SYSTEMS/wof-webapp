using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PathFinding
{
    public class Graph
    {
        private Edgelist edges;

        public Edgelist Edges
        {
            get { return edges; }
        }
        
        private Nodelist nodes;

        public Nodelist Nodes
        {
            get { return nodes; }
        }
        

        public Graph() 
        {
            edges = new Edgelist();
            nodes = new Nodelist();
        }

        public List<Edge> findBestPath(Node start_node, Node end_node){
            return new List <Edge> ();
        }

        public void addNode(Node new_node)
        {
            if (!nodes.Contains(new_node))
            {
                nodes.Add(new_node);
            }
        }

        public void addEdge(Node n1, Node n2, double scale)
        {
            double weight = CoordinateCalculator.euclideanDistance(n1.CrossingPoint, n2.CrossingPoint)/scale;
            Edge new_edge = new Edge(n1, n2, weight);
            addEdge(new_edge);
        }
        public void addEdge(Edge new_edge)
        {
            if (!edges.Contains(new_edge))
            {
                edges.Add(new_edge);
            }
        }

        public class Nodelist : List<Node>
        {
            public Nodelist()
            { }

            public new bool Contains(Node node)
            {
                for (int i = 0 ; i < Count; i++)
                {
                    if (node == this[i])
                    {
                        return true;
                    }
                }

                return false;
            }


        }

        public class Edgelist : List<Edge>
        {
            public Edgelist()
            { }

            public new bool Contains(Edge edge)
            {
                for (int i = 0 ; i < Count; i++)
                {
                    if (edge == this[i])
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public Node findNodeByOfficeNumber(int number)
        {
            return nodes.Find(x => (x.OfficeLocation == number));


        }



    }
}
