using Microsoft.VisualStudio.TestTools.UnitTesting;
using project_leadconsult_core.BE;
using project_leadconsult_core.Utils;

namespace project_leadconsult_core_tests
{
    /// <summary>
    /// TestHelper
    /// </summary>
    [TestClass]
    public class TestHelper
    {
        /// <summary>
        /// Tests the euclidean plane.
        /// </summary>
        [TestMethod]
        public void TestEuclideanPlane()
        {
            Point p1 = new Point(1, 10, 20);
            Point p2 = new Point(2, 30, 40);

            Assert.AreEqual(28.284271247461902d, Helper.EuclideanPlane(p1, p2));
        }
    }
}