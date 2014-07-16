using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PathFinding
{
    [TestClass]
    public class MinCostPathFinderTests
    {
        [TestMethod]
        public void MinCostPathFinderTest()
        {
            MinCostPathFinder pathFinder = new MinCostPathFinder();
            Node one = new Node(1, 0, 0);

            Path path1 = new Path(one);
            Path testPath = pathFinder.findPath(one, one);
            Assert.AreEqual(path1, pathFinder.findPath(one, one));

            Node two = new Node(2, 0, 0);
            Edge onetwo = new Edge(one, two, 5.0);

            Path path12 = new Path(one);
            path12.addEdgeToPath(onetwo);
            Assert.AreEqual(path12, pathFinder.findPath(one, two));

            Node three = new Node(3, 0, 0);
            Edge twothree = new Edge(two, three, 5.0);
            Path path123 = new Path(path12);
            path123.addEdgeToPath(twothree);
            Assert.AreEqual(path12, pathFinder.findPath(one, two));
            Assert.AreEqual(path123, pathFinder.findPath(one, three));

            Node four = new Node(4, 0, 0);
            Edge twofour = new Edge(two, four, 1.0);
            Assert.AreEqual(path123, pathFinder.findPath(one, three));

            Edge threefour = new Edge(three, four, 1.0);

            Path path1243 = new Path(path12);
            path1243.addEdgeToPath(twofour);
            path1243.addEdgeToPath(threefour);

            Assert.AreEqual(path1243, pathFinder.findPath(one, three));

            Path path3421 = new Path(three);
            path3421.addEdgeToPath(threefour);
            path3421.addEdgeToPath(twofour);
            path3421.addEdgeToPath(onetwo);

            Assert.AreEqual(path3421, pathFinder.findPath(three, one));
        }

        [TestMethod]
        public void MinCostPathFinderFromSVGTest()
        {
            string filePath = "../map.svg";
            double error = 0.1;
            double scale = CoordinateCalculator.getScale(new Point(0, 792), new Point(162, 792), 15);

            double epsilon = error * scale;
            Graph my_graph = Converter.downloadMap(filePath, scale, epsilon);

            //these parameters will be passed every time the search is activated
            int start_office = 7038;
            int end_office = 7236;

            Node start_node = my_graph.findNodeByOfficeNumber(start_office);
            Node end_node = my_graph.findNodeByOfficeNumber(end_office);
            MinCostPathFinder pathfinder = new MinCostPathFinder();
            Path shortest_path = pathfinder.findPath(start_node, end_node);
            String jsonInstructions = shortest_path.getJSONDirections();

            //Assert.Fail(); //uncomment to look at the jsonInstructions to make sure they make sense
        }
    }
}
