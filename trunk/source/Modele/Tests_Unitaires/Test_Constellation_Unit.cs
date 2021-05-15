using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Espace;
using Geometrie;

namespace Tests_Unitaires
{
    [TestClass]
    public class Test_Constellation_Unit
    {
        [TestMethod]
        public void TestParcours()
        {
            Constellation constel = new Constellation(new Point(42, 42), new Point(56, 56));
            constel.Relier(new Point(42, 42), new Point(85, 36));
            constel.Relier(new Point(85, 36), new Point(20, 31));
            constel.Relier(new Point(42, 42), new Point(95, 52));
            constel.Relier(new Point(95, 52), new Point(12, 89));
            Assert.IsNull(constel.DiviserConstellation());
        }
    }
}
