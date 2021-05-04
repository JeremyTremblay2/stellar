using Modele;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest_Segment
    {
        /// <summary>
        /// test du constructeur, getter
        /// </summary>
        [Fact]
        public void Test()
        {
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(1, 1);
            Segment unSegment = new Segment(pt1, pt2);
            Assert.Equal(0, unSegment.Point1.X);
        }
    }
}
