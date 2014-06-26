using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Node_ns;

namespace Edge_ns
{
    class Edge
    {

        private Node start_node;
        private Node end_node;
        private double weight;

        public Edge(Node start_node, Node end_node)
        {
            this.start_node = start_node;
            this.end_node = end_node;
            //calculate weight => where do we store scale?
        }
    }
}
