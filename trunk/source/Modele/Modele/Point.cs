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
        /// <param name="x">Coordonnée en x</param>
        /// <param name="y">Coordonnée en y</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; private set; } 
        public int Y { get; private set; }

        /// <summary>
        /// Permets de déplacer un point 
        /// </summary>
        /// <param name="x">Nouvelle coordonnée en abscisse qui ser affectée à x</param>
        /// <param name="y">Nouvelle coordonnée en ordonnée qui sera affectée à y</param>
        public void Deplacer(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}; {Y})";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            if (ReferenceEquals(obj, null) || !GetType().Equals(obj.GetType())) return false;
            Point pt = (Point) obj;
            return pt.X == this.X && pt.Y == this.Y;
        }

    }
}
