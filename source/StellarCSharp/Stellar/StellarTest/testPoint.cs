using Microsoft.VisualStudio.TestTools.UnitTesting;
using StellarModele;
namespace StellarTest
{
    [TestClass]
    public class testPoint
    {
        [TestMethod]
        public void TestConstructeur() //test du constructeur + getter
        {
            Point unPoint = new Point(42,42);
            Assert.IsNotNull(unPoint.x);
            Assert.IsNotNull(unPoint.y);
            Assert.AreEqual(unPoint.x, 42);
        }
    }
}
