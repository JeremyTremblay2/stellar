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
        /// test du constructeur, getter et la méthode Deplacer
        /// </summary>
        [Fact]
        public void Test()
        {
            Point unPoint = new Point(42, 42);
            Assert.Equal(42, unPoint.X);
            unPoint.Deplacer(56, 42);
            Assert.Equal(56, unPoint.X);
        }
    }
}

