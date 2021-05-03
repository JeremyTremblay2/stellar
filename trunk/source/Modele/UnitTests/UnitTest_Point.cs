using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class UnitTest_Point
    {
        [Fact]
        public void TestConstructeur() //test du constructeur + getter
        {
            Point unPoint = new Point(42, 42);
            //Assert.IsNotNull(unPoint.x);
            //Assert.IsNotNull(unPoint.y);
            //Assert.AreEqual(unPoint.x, 42);
        }
    }
}
}
