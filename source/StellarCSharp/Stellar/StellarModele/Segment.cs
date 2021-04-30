using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarModele
{
    public class Segment
    {
        public Segment(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        public Point point1 { get; private set; } // coordonnées du premier point du segment
        public Point point2 { get; private set; } // coordonnées du deuxième point du segment
    }
}
