using System;
using System.Diagnostics.CodeAnalysis;

namespace Modele
{
    /// <summary>
    /// Un segment représente les coordonnées de deux points reliés entre eux (liaisons entre les étoiles sur la carte)
    /// </summary>
    public class Segment : IEquatable<Segment>
    {
        /// <summary>
        /// Constructeur de Segment
        /// </summary>
        /// <param name="point1">Coordonnées du premier point du segment</param>
        /// <param name="point2">Coordonnées du deuxième point du segment</param>
        public Segment(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        /// <summary>
        /// Permet d'afficher un segment. Pour cela, on affiche les deux points du segment correspondant.
        /// </summary>
        /// <returns>Retourne la chaîne de caractère représentant le segment.</returns>
        public override string ToString()
        {
            return $"Segment entre les points {Point1} et {Point2}";
        }

        public bool Equals([AllowNull] Segment autre)
        {
            return Point1.Equals(autre.Point1) && Point2.Equals(autre.Point2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Segment);
        }

        public bool PtEquals(Point pt)
        {
            return pt.Equals(this.Point1) || pt.Equals(this.Point2);
        }

        public override int GetHashCode()
        {
            return Point1.GetHashCode() + Point2.GetHashCode();
        }
    }
}
