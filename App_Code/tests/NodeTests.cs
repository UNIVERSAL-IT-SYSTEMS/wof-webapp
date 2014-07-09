using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PathFinding
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void instantiationTest()
        {
            //No Arguments
            Node n = new Node();
            Assert.AreEqual(0, n.CrossingPoint.X, "Node instantiated without parameters should have x set to zero.");
            Assert.AreEqual(0, n.CrossingPoint.Y, "Node instantiated without parameters should have y set to zero.");
            Assert.AreEqual(-1, n.OfficeLocation, "Node instantiated without parameters should have OfficeLocation set to -1.");

            //officeNumber, x-coordinate, y-coordinate
            n = new Node(1, 0, 0);
            //TODO: Add more tests

            //officeNumber, point
            Point p = new Point(3, 4);
            n = new Node(1, p);
            //TODO: Add more tests

            Assert.Fail();
        }
        [TestMethod]
        public void propertyAssignmentTest()
        {
            
            Point newCrossingPoint = new Point(1,2);
            int newOfficeLocation = 7424;
            
            Node n = new Node();
            
            n.CrossingPoint = newCrossingPoint;
            Assert.AreEqual(newCrossingPoint, n.CrossingPoint, "CrossingPoint not being reassigned properly.");
            
            n.OfficeLocation = newOfficeLocation;
            Assert.AreEqual(newOfficeLocation, n.OfficeLocation, "OfficeLocation not being reassigned properly.");
        }

        [TestMethod]
        public void ToStringTest()
        {
            Node n = new Node();
            int newOfficeLocation = 7424;
            n.OfficeLocation = newOfficeLocation;
            Assert.AreEqual(newOfficeLocation.ToString(), n.ToString(), "ToString() should print out the office number.");
        }

        [TestMethod]
        public void addEdgeTest()
        {
            //Test adding edges
            Assert.Fail();
        }
    }
}
