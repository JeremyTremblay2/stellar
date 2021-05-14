using Geometrie;
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
        /// test du constructeur, getter et de la méthode Deplacer
        /// </summary>
        [Fact]
        public void Test()
        {
            Point unPoint = new Point(42, 42);
            Assert.Equal(42, unPoint.X);
            unPoint.Deplacer(56, 42);
            Assert.Equal(56, unPoint.X);

        }

        /// <summary>
        /// test de la méthode equals
        /// </summary>
        [Fact]
        public void testEqual()
        {
            Point unPoint = new Point(42, 42);
            Point ptEqualTest1 = new Point(42, 42);
            Point ptEqualTest2 = new Point(56, 42);
            Assert.True(ptEqualTest1.Equals(unPoint));
            Assert.False(ptEqualTest2.Equals(unPoint));
        }
    }
}

