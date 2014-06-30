using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class DjikstraPathFinder
    {
        private List<Path> paths;
        private List<Node> finishedNodes;

        private Node startNode;
        private Node endNode;

        public DjikstraPathFinder()
        {
            paths = new List<Path>();
            finishedNodes = new List<Node>();
            //startNode = None; //null node, just don't want it to try to find a path without a start and end node
            //endNode = None; //null node, just don't want it to try to find a path without a start and end node
        }

        public Path findPath(Node startNode, Node endNode)
        {
            this.startNode = startNode;
            this.endNode = endNode;
            Path initialPath = new Path(startNode);
            return explorePath(initialPath);

        }

        /**
         * Finds the shortest path starting with Path p. If path p already leads to
         * end node, then we have found the shortest path, so return that.
         * */
        private Path explorePath(Path p)
        {
            Node lastNode = p.LastNode;
            if (lastNode == endNode) return p;

            foreach (Edge e in lastNode.Edges)
            {
                if (!finishedNodes.Contains(e.otherNode(lastNode))) //if a shorter path to that node has not already been found...
                {
                    addEdgeToPath(e, p);
                }
            }
            finishedNodes.Add(lastNode);
            return explorePath(nextLowestPath());
        }

        private Path nextLowestPath()
        {
            paths.Sort();
            return paths[0];
        }

        private void addEdgeToPath(Edge e, Path p)
        {
            Node newLastNode = e.otherNode(p.LastNode);
            double newCost = p.Cost + e.Weight;

            paths.Remove(p);

            //Check if path to node already exists
            foreach (Path oldPath in paths)
            {
                if (oldPath.LastNode == newLastNode)
                {
                    //there is already a path in paths that leads to newLastNode
                    if (oldPath.Cost > newCost)
                    {
                        //if new path is shorter, remove old path and add new one.
                        p.addEdgeToPath(e);
                        paths.Add(p);
                        return; //there will be at most one path lastNode == newLastNode so we can stop looking

                    }
                    return;  //if there is already a shorter path that leads to this node, don't do anything.
                }
            }
            //there isn't a path that leads to newLastNode in paths yet, so add newPath
            Path newPath = new Path(p);
            newPath.addEdgeToPath(e);
            paths.Add(newPath);
            return;
        }

    }
}
