using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator_ns;
using Line_ns;
using Point_ns;

namespace lineTest
{
    [TestClass]
    public class pointTests
    {
        [TestMethod]
        public void testRotate()
        {
            Line test = new Line(new Point(-2, 3), new Point(2,1));
            test.rotate(90 * Math.PI / 180);
            Assert.AreEqual(-1, test.getEndPoint().getX(), 0.1, "the resulting end x coordinate is correct");
            Assert.AreEqual(2, test.getEndPoint().getY(), 0.1, "the resulting end y coordinate is correct");
            Assert.AreEqual(-3, test.getStartPoint().getX(), 0.1, "the resulting start x coordinate is correct");
            Assert.AreEqual(-2, test.getStartPoint().getY(), 0.1, "the resulting start y coordinate is correct");
        }

        [TestMethod]
        public void testTranslate()
        {
            Line test = new Line(new Point(101.11, 3), new Point(33.3333, -36.38));
            test.translate(10, -1);
            Assert.AreEqual(43.3333, test.getEndPoint().getX(), 0.0001, "the resulting end x coordinate is correct");
            Assert.AreEqual(-37.38, test.getEndPoint().getY(), 0.0001, "the resulting end y coordinate is correct");
            Assert.AreEqual(111.11, test.getStartPoint().getX(), 0.0001, "the resulting start x coordinate is correct");
            Assert.AreEqual(2, test.getStartPoint().getY(), 0.0001, "the resulting start y coordinate is correct");
        }

        [TestMethod]
        public void ifLinesDoNotCrossThenReturnsNull()
        {
            Line a = new Line(new Point(0, 0), new Point(1, 1));
            Line b = new Line(new Point(-3, -3), new Point(-1, -1));
            
            Assert.AreEqual(null, a.crosses(b), "the crossing point is correctely null");
            
        }

        [TestMethod]
        public void testGetSlope()
        {
            Line test = new Line(new Point(10, 10), new Point(15, 20));   

            Assert.AreEqual(2, test.getSlope(), "the slope is correct");

        }

        [TestMethod]
        public void testGetConst()
        {
            Line test = new Line(new Point(10, 10), new Point(15, 20));

            Assert.AreEqual(-10, test.getConstant(), "the constant is correct");

        }



    }
}