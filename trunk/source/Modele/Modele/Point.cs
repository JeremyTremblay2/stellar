using System;

namespace Modele
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
            X = x;
            Y = y;
        }
        public int X { get; private set; } 
        public int Y { get; private set; }

        public void Deplacer(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}; {Y})";
        }
    }
}
