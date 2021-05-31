using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Geometrie
{
    /// <summary>
    /// Un segment représente les coordonnées de deux points reliés entre eux (liaisons entre les étoiles sur la carte).
    /// </summary>
    [DataContract]
    public class Segment : IEquatable<Segment>
    {
        /// <summary>
        /// Propriété représentant le premier point du segment, sous forme de type Point.
        /// </summary>
        [DataMember]
        public Point Point1 { get; private set; }

        /// <summary>
        /// Propriété représentant le second point du segment, sous forme de type Point.
        /// </summary>
        [DataMember]
        public Point Point2 { get; private set; }

        /// <summary>
        /// La couleur d'un segment, permet de l'identifier et est jaune par défaut.
        /// </summary>
        [DataMember]
        public string Couleur { get; internal set; } = "#ffffff";

        /// <summary>
        /// Constructeur de Segment.
        /// </summary>
        /// <param name="point1">Premier point du segment.</param>
        /// <param name="point2">Deuxième point du segment.</param>
        public Segment(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        /// <summary>
        /// Méthode permettant de savoir si le point passé en paramètre est contenu dans le segment (si c'est un des deux points).
        /// </summary>
        /// <param name="point">Le point dont on veut savoir s'il se trouve dans le segment.</param>
        /// <returns>Un booléen indiquant si le point se trouvait dans le segment ou non.</returns>
        public bool PtEquals(Point point)
        {
            return point.Equals(this.Point1) || point.Equals(this.Point2);
        }

        /// <summary>
        /// Permet d'afficher un segment. Pour cela, on affiche les deux points du segment correspondant, et une flèche 
        /// entre eux (pour marquer la liaison).
        /// </summary>
        /// <returns>Retourne la chaîne de caractère représentant le segment.</returns>
        public override string ToString()
        {
            return $"{Point1} => {Point2}";
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un segment passé en paramètre est égal à this, donc s'il possède les mêmes 
        /// points (pas forcément dans le même ordre, s'ils possèdent una paire de points similaires, alors ce sont les mêmes).
        /// </summary>
        /// <param name="autre">Un segment que l'on souhaite comparer à this.</param>
        /// <returns>Un booléen qui indique si le segment passé en paramètre est le même que this ou non.</returns>
        public bool Equals([AllowNull] Segment autre)
        {
            return Point1.Equals(autre.Point1) && Point2.Equals(autre.Point2) ||
                Point1.Equals(autre.Point2) && Point2.Equals(autre.Point1);
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est un Segment.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si ce segment est égal à this, donc s'il possède les mêmes points.
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'un segment (et s'il s'agit du même segment que this)</param>
        /// <returns>Un booléen qui indique si l'objet passé en paramètre est le même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Segment);
        }

        /// <summary>
        /// Permet la génération d'un HashCode, utilisé dans le cas des dictionnaires.
        /// Ce hascode est définit par les deux points du segment.
        /// </summary>
        /// <returns>Un entier représentant le hashcode de ce point.</returns>
        public override int GetHashCode()
        {
            return Point1.GetHashCode() + Point2.GetHashCode();
        }
    }
}
