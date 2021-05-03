using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Segment
    {
        public Segment(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        public override string ToString()
        {
            return $"Segment entre les points {Point1} et {Point2}";
        }
    }
}
