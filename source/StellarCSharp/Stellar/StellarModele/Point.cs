using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarModele
{
    public class Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public int x { get; private set; } // coordonnées du point
        public int y { get; private set; }
    }
}
