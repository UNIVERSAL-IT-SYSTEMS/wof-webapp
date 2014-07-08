using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;

namespace pointTest
{
    [TestClass]
    public class pointTests
    {
        [TestMethod]
        public void testRotate()
        {
            Point test = new Point(2,1);
            test.rotate(90 * Math.PI / 180);
            Assert.AreEqual(-1, test.X, 0.1, "the resulting x coordinate is correct");
            Assert.AreEqual(2, test.Y, 0.1, "the resulting y coordinate is correct");
        }

        public void equalsOpTest()
        {
            Point a = new Point((float)1.11, (float)1.11);
            Point b = new Point((float)1.11, (float)1.11);
            Point c = new Point(2, (float)1.11);

            Assert.AreEqual(true, a == b, "the equals operator result is correctely true");
            Assert.AreEqual(true, a == a, "the equals operator result is correctely true");
            Assert.AreEqual(false, a == c, "the equals operator result is correctely false");
        }
        


    }
}
