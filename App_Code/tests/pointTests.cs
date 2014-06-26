using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator_ns;
using Point_ns;

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
            Assert.AreEqual(-1, test.getX(), 0.1, "the resulting x coordinate is correct");
            Assert.AreEqual(2, test.getY(), 0.1, "the resulting y coordinate is correct");
        }

        


    }
}
