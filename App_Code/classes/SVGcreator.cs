using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PathFinding
{
    class SVGcreator
    {

        public static void drawPath(string destination_file, string original_map, Path shortest_path)
        {
            if (shortest_path.ListOfNodes.Count == 0)
                return;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;
            XmlReader reader = XmlReader.Create(original_map, settings);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNode main = doc.LastChild;
            XmlNodeList main_nodes = main.ChildNodes;
            XmlElement g_tag = (XmlElement)main_nodes[5];

            string path = "M" + shortest_path.ListOfNodes.ElementAt(0).CrossingPoint.X +
                " " + shortest_path.ListOfNodes.ElementAt(0).CrossingPoint.Y;

            for (int i = 1; i < shortest_path.ListOfNodes.Count; i++)
            {
                path += " L" + shortest_path.ListOfNodes.ElementAt(i).CrossingPoint.X
                    + " " + shortest_path.ListOfNodes.ElementAt(i).CrossingPoint.Y;
            }


            XmlElement new_elem = doc.CreateElement("g", "http://www.w3.org/2000/svg");
            XmlElement path_tag = doc.CreateElement("path", "http://www.w3.org/2000/svg");
            path_tag.SetAttribute("d", path);
            path_tag.SetAttribute("style", "stroke:red");
            new_elem.AppendChild(path_tag);
            g_tag.AppendChild(new_elem);


            doc.Save(destination_file);
            reader.Close();
        }

        public static void drawNodes(string destination_file, string original_map, Graph my_graph)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;
            XmlReader reader = XmlReader.Create(original_map, settings);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNode main = doc.LastChild;
            XmlNodeList main_nodes = main.ChildNodes;
            XmlElement g_tag = (XmlElement)main_nodes[5];

            for (int i = 0; i < my_graph.Nodes.Count; i++)
            {
                XmlElement new_elem = doc.CreateElement("g", "http://www.w3.org/2000/svg");
                XmlElement rect = doc.CreateElement("rect", "http://www.w3.org/2000/svg");
                rect.SetAttribute("x", Convert.ToString(my_graph.Nodes[i].CrossingPoint.X));
                rect.SetAttribute("y", Convert.ToString(my_graph.Nodes[i].CrossingPoint.Y));
                rect.SetAttribute("width", "2");
                rect.SetAttribute("height", "2");
                rect.SetAttribute("style", "stroke:green");
                new_elem.AppendChild(rect);
                g_tag.AppendChild(new_elem);

            }

            doc.Save(destination_file);
            reader.Close();
        }

        public static void drawEdges(string destination_file, string original_map, Graph my_graph)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;
            XmlReader reader = XmlReader.Create(original_map, settings);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNode main = doc.LastChild;
            XmlNodeList main_nodes = main.ChildNodes;
            XmlElement g_tag = (XmlElement)main_nodes[5];

            for (int i = 0; i < my_graph.Edges.Count; i++)
            {
                string path = "M" + my_graph.Edges[i].N1.CrossingPoint.X +
                " " + my_graph.Edges[i].N1.CrossingPoint.Y
                + " L" + my_graph.Edges[i].N2.CrossingPoint.X
                + " " + my_graph.Edges[i].N2.CrossingPoint.Y;

                XmlElement new_elem = doc.CreateElement("g", "http://www.w3.org/2000/svg");
                XmlElement path_tag = doc.CreateElement("path", "http://www.w3.org/2000/svg");
                path_tag.SetAttribute("d", path);
                path_tag.SetAttribute("style", "stroke:yellow");
                new_elem.AppendChild(path_tag);
                g_tag.AppendChild(new_elem);

            }

            doc.Save(destination_file);
            reader.Close();
        }
    }
}
