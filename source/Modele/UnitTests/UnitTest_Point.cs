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
        /// <summary>
        /// test du constructeur + getter
        /// </summary>
        [Fact]
        public void TestConstructeur()
        {
            Point unPoint = new Point(42, 42);
            //Assert.IsNotNull(unPoint.x);
            //Assert.IsNotNull(unPoint.y);
            //Assert.AreEqual(unPoint.x, 42);
        }
    }
}

