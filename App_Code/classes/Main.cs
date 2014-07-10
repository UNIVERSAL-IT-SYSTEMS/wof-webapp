using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinding;
using System.Xml;

namespace run_ns { 
    class run
    {

        static int Main(string[] args)
        {
            //these parameters need to be passed to Azure for each given map
            string filePath = "../../map.svg";
            double error = 0.1;
            double scale = CoordinateCalculator.getScale(new Point(0, 792), new Point(162, 792), 15);

            double epsilon = error * scale;
            Graph my_graph = Converter.downloadMap(filePath, scale, epsilon);


            //these parameters will be passed every time the search is activated
            int start_office = 7073;
            int end_office = 7236;

            Node start_node = my_graph.findNodeByOfficeNumber(start_office);
            Node end_node = my_graph.findNodeByOfficeNumber(end_office);
            MinCostPathFinder pathfinder = new MinCostPathFinder();
            Path shortest_path = pathfinder.findPath(start_node, end_node);
            
            //this function is for testing the results of the sserach. It graphically draws the path on a map
            SVGcreator.drawPath("path.svg", filePath, shortest_path);

            return 0;
        }
    }

}