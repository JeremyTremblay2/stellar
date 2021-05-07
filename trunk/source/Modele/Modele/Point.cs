using System;
using System.Diagnostics.CodeAnalysis;

namespace Modele
{   
    /// <summary>
    /// Un point représente les coordonnées d'un point sur la carte (l'emplacement des astres)
    /// </summary>
    public class Point //: IEquatable<Point>
    {
        /// <summary>
        /// Constructeur de Point
        /// </summary>
        /// <param name="x">Coordonnées en abscisses</param>
        /// <param name="y">Coordonnées en ordonnées</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; } 

        public int Y { get; private set; }

        /// <summary>
        /// Permet de déplacer un point 
        /// </summary>
        /// <param name="x">Nouvelle coordonnée en abscisse qui sera affectée à x</param>
        /// <param name="y">Nouvelle coordonnée en ordonnée qui sera affectée à y</param>
        public void Deplacer(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Permet d'afficher un point sous forme d'une paire de coordonées (x; y).
        /// </summary>
        /// <returns>Retourne la chaîne de caractère représentant les coordonées du point.</returns>

        public override string ToString()
        {
            return $"({X}; {Y})";
        }

        public bool Equals([AllowNull] Point autre) //NEW
        {
            return X.Equals(autre.X) && Y.Equals(autre.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Point);
        }

        /*public override bool Equals(object obj) //OLD
        {
            if (ReferenceEquals(obj, this)) return true;
            if (ReferenceEquals(obj, null) || !GetType().Equals(obj.GetType())) return false;
            return (obj as Point).X == this.X && (obj as Point).Y == this.Y;

        }*/


        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}
