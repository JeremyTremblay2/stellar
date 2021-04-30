using Microsoft.VisualStudio.TestTools.UnitTesting;
using StellarModele;
namespace StellarTest
{
    /// <summary>
    /// Classe test pour Point
    /// </summary>
    [TestClass]
    public class testPoint
    {
        /// <summary>
        /// test du constructeur + getter
        /// </summary>
        [TestMethod]
        public void TestConstructeur()
        {
            Point unPoint = new Point(42,42);
            Assert.IsNotNull(unPoint.x);
            Assert.IsNotNull(unPoint.y);
            Assert.AreEqual(unPoint.x, 42);
        }
    }
}
