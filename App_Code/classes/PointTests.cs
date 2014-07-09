using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;

namespace pointTest
{
    [TestClass]
    public class pointTests
    {
        [TestMethod]
        public void xAndYAssignmentTest()
        {
            float x = (float)3.0;
            float y = (float)4.0;
            Point p = new Point(x, y);
            Assert.AreEqual(x, p.X, "X-Coordinate not being set properly at instantiation.");
            Assert.AreEqual(y, p.Y, "Y-Coordinate not being set properly at instantiation.");
        }

        [TestMethod]
        public void xAndYReassignmentTest()
        {
            Point p = new Point(0, 0);
            int newX = 1;
            int newY = 2;
            p.X = newX;
            Assert.AreEqual(newX, p.X, "X-Coordinate property not being re-set properly.");
            p.Y = newY;
            Assert.AreEqual(newY, p.Y, "Y-Coordinate property not being re-set properly.");
        }

        [TestMethod]
        public void TranslateTest()
        {
            Point p = new Point(0, 0);
            double translateX = 4.5;
            double translateY = 3.5;
            p.translate((float)translateX, (float)translateY);
            Assert.AreEqual(translateX, p.X, "X-Coordinate not being translated properly.");
            Assert.AreEqual(translateY, p.Y, "Y-Coordinate not being translated properly.");
            p.translate((float)translateX, (float)translateY);
            Assert.AreEqual(2*translateX, p.X, "X-Coordinate not being translated properly.");
            Assert.AreEqual(2*translateY, p.Y, "Y-Coordinate not being translated properly.");
        }

        [TestMethod]
        public void RotateTest()
        {
            Point test = new Point(2, 1);
            test.rotate(90);
            Assert.AreEqual(-1, test.X, 0.001, "X-Coordinate not being rotated correctly.");
            Assert.AreEqual(2, test.Y, 0.001, "Y-Coordinate not being rotated correctly.");
        }

        [TestMethod]
        public void TransformTest()
        {
            float startX = 0;
            float startY = 0;
            float translateX = 1;
            float translateY = 2;
            float degreeRotation = 90;

            float expectedX = -2;
            float expectedY = 1;

            Point newP = new Point(startX, startY);
            newP.translate(translateX, translateY);
            newP.rotate(degreeRotation);
            Assert.AreEqual(expectedX, newP.X, 0.001, "Tranform function not behaving like translate and then rotate with X-Coordinate.");
            Assert.AreEqual(expectedY, newP.Y, 0.001, "Tranform function not behaving like translate and then rotate with Y-Coordinate.");

            Point p = new Point(startX, startY);
            p.transform(translateX, translateY, degreeRotation);
            Assert.AreEqual(expectedX, p.X, 0.001, "X-Coordinate not being transformed correctly.");
            Assert.AreEqual(expectedY, p.Y, 0.001, "Y-Coordinate not being transformed correctly.");
            

        }

        [TestMethod]
        public void EqualsTest()
        {
            Point a = new Point((float)1.11, (float)1.11);
            Point b = new Point((float)1.11, (float)1.11);
            Point c = new Point(2, (float)1.11);

            Assert.AreEqual(true, a == a, "The == operator is incorrectly indicating that the a point is not equal to itself.");
            Assert.AreEqual(true, a == b, "The == operator is incorrectly indicating that points with equal coordinates are not equal.");
            Assert.AreEqual(false, a == c, "The == operator is incorrectly indicating that points with different coordinates are equal.");
        }
    }
}
