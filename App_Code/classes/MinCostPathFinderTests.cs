using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PathFinding
{
    [TestClass]
    public class MinCostPathFinderTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Point p = new Point(0, 0);
            p.X = 1;
            if (p.X != 1) Console.WriteLine("X-Coordinate not being set properly. Should be 1. Returning " + p.X);
            p.Y = 2;
            if (p.Y != 2) Console.WriteLine("Y-Coordinate not being set properly. Should be 2. Returning " + p.Y);
            p = new Point((float)3.0, (float)4.0);
            if (p.X != 3.0) Console.WriteLine("X-Coordinate not being set properly. Should be 3. Returning " + p.X);
            if (p.Y != 4.0) Console.WriteLine("Y-Coordinate not being set properly. Should be 4. Returning " + p.Y);
            //test float decimal
            Console.WriteLine("\nDone with Point tests.");
            Assert.Fail();
        }
    }
}
