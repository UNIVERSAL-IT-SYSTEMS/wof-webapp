using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Net;
using System.Text;

namespace PathFinding
{

    /*
     * Worker role constantly runs on Azure server, 
     * ready to perform pathfinding once the specific room is entered
     */

    public class WorkerRole : RoleEntryPoint
    {

        private Graph my_graph;

        private string mobile_services_host = "[your host]";
        private string security_key = "[your key]";

        private string table_name = "offices";
        private string filter = "complete%20eq%20false%20and%20cancelled%20eq%20false";
        
        private int start_office = 7242;
        private string node_host = "[your host]";
        private double scale;

        ManualResetEvent CompletedEvent = new ManualResetEvent(false);


        public override void Run()
        {
            //Get any new requests from the database
            string sURL = "https://" + mobile_services_host + "/tables/" + table_name + "?$filter=(" + filter + ")";

            HttpWebRequest wrGETURL;

            while(true)
            {
                Thread.Sleep(10000);

                wrGETURL = (HttpWebRequest)WebRequest.Create(sURL);
                wrGETURL.Accept = "application/json";
                wrGETURL.Headers.Add("X-ZUMO-APPLICATION", security_key);
                wrGETURL.Host = mobile_services_host;

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null && sLine!= "[]")
                    {
                        Record record = new Record(sLine);
                        findPath(Convert.ToInt32(record.get("office")));
                        setRequestToComplete(record.get("id"));
                    }

                }
                objStream.Close();

            }
        }

        
       /*
        * Represents a record in a database 
        */
        private class Record
        {
                private Dictionary <string, string> properties;
                
                public Dictionary <string, string> Properties
                {
                    get {return properties;}
                    set {properties = value;}
                }

                public Record(string recordinfo)
                {
                    properties = new Dictionary <string, string> ();
                    string[] values = recordinfo.Split(',');
                    foreach (string value in values)
                    {
                        string property = value.Replace("\"", "");
                        property = property.Replace("{", "");
                        property = property.Replace("}", "");
                        property = property.Replace("[", "");
                        property = property.Replace("[", ""); 
                        string []  parts = property.Split(':');
                        if (parts.Length  == 2)
                        {
                            properties.Add(parts[0], parts[1]);
                        }
                    }
                }

                public string get(string property_name)
                {
                    return properties[property_name];
                }

            
        }

        /*
         * finds the path between start office and end office specified
         * @param end_office end office
         */
        private void findPath(int end_office)
        {
            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("maps");

            Node start_node = my_graph.findNodeByOfficeNumber(start_office);
            Node end_node = my_graph.findNodeByOfficeNumber(end_office);
            MinCostPathFinder pathfinder = new MinCostPathFinder();
            Path shortest_path = pathfinder.findPath(start_node, end_node);
            storeResultingPath(shortest_path);

            sendInstructions(shortest_path);
        }

        /*
         * OPTIONAL:
         * this function is for testing the results of the serach. It graphically draws the path on a map
         */
        private void storeResultingPath(Path path_to_draw)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("maps");


            //upload
            CloudBlockBlob mapBlob = container.GetBlockBlobReference("map.svg");

            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(@"map.svg"))
            {
                mapBlob.DownloadToStream(fileStream);
            }
            SVGcreator.drawPath("path.svg", "map.svg", path_to_draw);
            CloudBlockBlob graphblob = container.GetBlockBlobReference("path.svg");
            using (var fileStream = System.IO.File.OpenRead(@"path.svg"))
            {

                graphblob.UploadFromStream(fileStream);
            } 
        }

        /*
         * Sends the list of directions to the robot, using the nodebot host specified 
         * @param path the path to be sent to the robot
         */
        private void sendInstructions(Path path)
        {
            
            string body = path.getJSONDirections(scale);
            string sURL = "http://" + node_host + "/robot/list";

            HttpWebRequest wrPOSTURL;
            wrPOSTURL = (HttpWebRequest)WebRequest.Create(sURL);
            wrPOSTURL.Method = "POST";
            wrPOSTURL.Accept = "application/json";
            wrPOSTURL.ContentType = "application/json";
            wrPOSTURL.Host = node_host;
            wrPOSTURL.ContentLength = body.Length;
            WebProxy myProxy = new WebProxy();
            myProxy.BypassProxyOnLocal = true;

            wrPOSTURL.Proxy = myProxy;


            Stream stream = wrPOSTURL.GetRequestStream();
            byte[] byteArray = Encoding.UTF8.GetBytes(body);
            stream.Write(byteArray, 0, byteArray.Length);

            //TODO: CHECK IF RESPONSE IS OK
            //WebResponse resp = wrPOSTURL.GetResponse();
            stream.Close();
             
        }

        /*
         * Marks the request with specified id as complete in a database 
         * @param id record id
         */
        private void setRequestToComplete(string id)
        {

            string body = "{\"complete\":true}";
            string sURL = "https://" + mobile_services_host + "/tables/" + table_name + "/" + id;

            HttpWebRequest wrPATCHURL;
            wrPATCHURL = (HttpWebRequest)WebRequest.Create(sURL);
            wrPATCHURL.Method = "PATCH";
            wrPATCHURL.Accept = "application/json";
            wrPATCHURL.ContentType = "application/json";
            wrPATCHURL.Headers.Add("X-ZUMO-APPLICATION", security_key);
            wrPATCHURL.Host = mobile_services_host;
            wrPATCHURL.ContentLength = body.Length;

            Stream stream = wrPATCHURL.GetRequestStream();
            byte[] byteArray = Encoding.UTF8.GetBytes(body);
            stream.Write(byteArray, 0, byteArray.Length);
            
            //TODO: CHECK IF RESPONSE IS OK
            WebResponse resp = wrPATCHURL.GetResponse();
            stream.Close();
        }


        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            createGraph();

            return base.OnStart();
        }

        /*
         * Converts the map that is already in the storage into a graph object
         */
        private void createGraph()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("maps");


            //upload
            CloudBlockBlob mapBlob = container.GetBlockBlobReference("map.svg");

            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(@"map.svg"))
            {
                mapBlob.DownloadToStream(fileStream);
            }

            //TODO: make settable manually
            string filePath = "map.svg";
            double error = 0.1;
            scale = CoordinateCalculator.getScale(new Point(0, 792), new Point(162, 792), 15);

            double epsilon = error * scale;
            my_graph = Converter.convert(filePath, scale, epsilon);
        }

        public override void OnStop()
        {
            CompletedEvent.Set();
            base.OnStop();

            
        }
    }
}
