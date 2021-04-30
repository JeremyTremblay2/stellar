using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StellarModele;

namespace StellarTest
{
    [TestClass]
    public class testSegment
    {
        [TestMethod]
        public void TestConstructeur() //test du constructeur + getter
        {
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(1, 1);
            Segment unSegment = new Segment(pt1, pt2);
            Assert.AreEqual(unSegment.point1.x, 0);
        }
    }
}
