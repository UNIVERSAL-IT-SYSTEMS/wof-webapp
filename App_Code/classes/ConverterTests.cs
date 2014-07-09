using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinding;

namespace calcTest
{
    [TestClass]
    public class converterTests
    {
      

        [TestMethod]
        public void testExtractTranslate()
        {
            Assert.AreEqual(830.029, Converter.extractTranslate("translate(830.029,-195.529) rotate(45)").X,
                0.01, "The x coordinate is correctely extracted");
            Assert.AreEqual(-195.529, Converter.extractTranslate("translate(830.029,-195.529) rotate(45)").Y,
                0.01, "The y coordinate is correctely extracted");
        }

        [TestMethod]
        public void testExtractRotate()
        {
            Assert.AreEqual<double>(45, Converter.extractRotate("translate(830.029,-195.529) rotate(45)"),
                "The angle is correctely extracted");
        }

        [TestMethod]
        public void testStartNewLine()
        {
            Assert.AreEqual<bool>(true, Converter.startNewLine("M0 345"), 
                "The new line is correctely told to be started");
            Assert.AreEqual<bool>(false, Converter.startNewLine("L85787.55 345"), 
                "The new line is correctely told not to be started");
            Assert.AreEqual<bool>(false, Converter.startNewLine("54567567 888"), 
                "The new line is correctely told not to be started");
        }

        [TestMethod]
        public void testGetCoordinate()
        {
            Assert.AreEqual(10.9, Converter.getCoordinateFromString("M10.9"), 
                0.01, "The positive coordinate is correctely calculated");
            Assert.AreEqual(-33.4, Converter.getCoordinateFromString("-33.4"), 
                0.01, "The negative coordinate is correctely calculated");
        }

        [TestMethod]
        public void testExtractOfficeNumber()
        {
            Assert.AreEqual<int>(7050, Converter.extractOfficeNumber("VT4(7050)"),
                "The office number is correctely extracted");
        }


    }
}
