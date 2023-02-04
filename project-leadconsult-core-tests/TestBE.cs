using Microsoft.VisualStudio.TestTools.UnitTesting;
using project_leadconsult_core.BE;

namespace project_leadconsult_core_tests
{
    /// <summary>
    /// TestBE
    /// </summary>
    [TestClass]
    public class TestBE
    {
        [TestMethod]
        public void TestPoint()
        {
            Point point = new Point(1, -10, 20);

            Assert.AreEqual(1, point.No);
            Assert.AreEqual(-10, point.Coordinates.X);
            Assert.AreEqual(20, point.Coordinates.Y);
        }

        /// <summary>
        /// Tests the quadrant i.
        /// </summary>
        [TestMethod]
        public void TestQuadrantI()
        {
            Point point = new Point(1, 50, 50);

            Assert.AreEqual("Quadrant I", point.GetQuadrant());
        }

        /// <summary>
        /// Tests the quadrant ii.
        /// </summary>
        [TestMethod]
        public void TestQuadrantII()
        {
            Point point = new Point(1, -50, 50);

            Assert.AreEqual("Quadrant II", point.GetQuadrant());
        }

        /// <summary>
        /// Tests the quadrant iii.
        /// </summary>
        [TestMethod]
        public void TestQuadrantIII()
        {
            Point point = new Point(1, -50, -50);

            Assert.AreEqual("Quadrant III", point.GetQuadrant());
        }

        /// <summary>
        /// Tests the quadrant iv.
        /// </summary>
        [TestMethod]
        public void TestQuadrantIV()
        {
            Point point = new Point(1, 50, -50);

            Assert.AreEqual("Quadrant IV", point.GetQuadrant());
        }

        /// <summary>
        /// Tests the quadrant origin.
        /// </summary>
        [TestMethod]
        public void TestQuadrantOrigin()
        {
            Point point = new Point(1, 0, 0);

            Assert.AreEqual("Origin", point.GetQuadrant());
        }
    }
}