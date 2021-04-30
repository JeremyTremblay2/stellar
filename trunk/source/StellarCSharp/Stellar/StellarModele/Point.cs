using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarModele
{
    /// <summary>
    /// Point représente les coordonnées d'un point sur la carte (l'emplacement des astres)
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Constructeur de Point
        /// </summary>
        /// <param name="x">coordonnée x</param>
        /// <param name="y">coordonnée y</param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; private set; } // coordonnées du point
        public int y { get; private set; }
    }
}
