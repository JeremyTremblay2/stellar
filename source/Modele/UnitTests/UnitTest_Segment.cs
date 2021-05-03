using Modele;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest_Segment
    {
        [Fact]
        public void TestConstructeur() //test du constructeur + getter
        {
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(1, 1);
            Segment unSegment = new Segment(pt1, pt2);
            //Assert.AreEqual(unSegment.point1.x, 0);
        }
    }
}
