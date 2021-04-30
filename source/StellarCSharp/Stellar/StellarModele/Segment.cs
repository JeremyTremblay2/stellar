using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarModele
{
    /// <summary>
    /// Segment représente les coordonnées de deux points reliés entre eux (liaisons entre les étoiles sur la carte)
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Constructeur de Segment
        /// </summary>
        /// <param name="point1">coordonnées du premier point du segment</param>
        /// <param name="point2">coordonnées du deuxième point du segment</param>
        public Segment(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        public Point point1 { get; private set; }
        public Point point2 { get; private set; }
    }
}
