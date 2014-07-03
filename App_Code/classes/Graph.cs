using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Node_ns;
using Edge_ns;

namespace Graph_ns
{
    class Graph
    {
        private List<Edge> edges;
        private Nodelist nodes;

        public Graph() 
        {
            edges = new List<Edge>();
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

        private class Nodelist : List<Node>
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


    }
}
