using Geometrie;
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

        /// <summary>
        /// test de la method Equals
        /// </summary>
        [Fact]
        public void testEqual()
        {
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(1, 1);
            Point pt3 = new Point(0, 0);
            Point pt4 = new Point(1, 1);
            Segment seg1 = new Segment(pt1, pt2);
            Segment seg2 = new Segment(pt3, pt4);
            Segment seg3 = new Segment(pt1, pt3);
            Assert.True(seg1.Equals(seg2));
            Assert.False(seg1.Equals(seg3));
        }
    }
}
