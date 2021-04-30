using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StellarModele;

namespace StellarTest
{
    /// <summary>
    /// Classe test pour Segment
    /// </summary>
    [TestClass]
    public class testSegment
    {
        /// <summary>
        /// test du constructeur + getter
        /// </summary>
        [TestMethod]
        public void TestConstructeur()
        {
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(1, 1);
            Segment unSegment = new Segment(pt1, pt2);
            Assert.AreEqual(unSegment.point1.x, 0);
        }
    }
}
