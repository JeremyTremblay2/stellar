using Microsoft.VisualStudio.TestTools.UnitTesting;
using StellarModele;
namespace StellarTest
{
    [TestClass]
    public class testPoint
    {
        [TestMethod]
        public void TestMethod1()
        {
            Point unPoint = new Point(42,42);
            int ptX = unPoint.x;
        }
    }
}
