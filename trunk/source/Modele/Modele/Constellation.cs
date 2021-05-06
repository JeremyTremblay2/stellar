using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// UJne constellation est un ensemble de points reliés par des segments, le tout forme un graphe (Les étoiles et les liens entre elles)
    /// </summary>
    public class Constellation
    {
        private List<Point> lesPoints = new List<Point>();
        private List<Segment> lesSegments = new List<Segment>();

        /// <summary>
        /// Constructeur de Constellation
        /// </summary>
        /// <param name="points">Une liste de points qui vont tous être reliés</param>
        /// <param name="segments">Une liste des segments qui reliront les points entre eux</param>
        public Constellation(List<Point> points, List<Segment> segments)
        {
            lesPoints = points;
            lesSegments = segments;
        }
        /// <summary>
        /// Constructeur de Constellation
        /// </summary>
        /// <param name="pt1">Prends un premier point</param>
        /// <param name="pt2">Prends un deuxième point</param>
        /// <param name="seg">Prends un segment</param>
        public Constellation(Point pt1, Point pt2, Segment seg)
        {
            lesPoints.Add(pt1);
            lesPoints.Add(pt2);
            lesSegments.Add(seg);
        }
        /// <summary>
        /// Ajoute un Point dans la liste lesPoints
        /// </summary>
        /// <param name="pt">Le point à rajouter à la constellation</param>
        public void AjoutPoint(Point pt)
        {
            lesPoints.Add(pt);
        }

        /// <summary>
        /// Ajoute un Segment dans la liste lesSegments
        /// </summary>
        /// <param name="seg">Le segment à rajouter à la constellation</param>
        public void AjoutSegment(Segment seg)
        {
            lesSegments.Add(seg);
        }

        /// <summary>
        /// Supprime un Point dans la list lesPoints
        /// </summary>
        /// <param name="pt">Point à supprimer</param>
        public void SupprimerPoint(Point pt)
        {
            lesPoints.Remove(pt);
        }

        /// <summary>
        /// Supprime un Segment dans la list lesSegments
        /// </summary>
        /// <param name="seg">Segment à supprimer</param>
        public void SupprimerSegment(Segment seg)
        {
            lesSegments.Remove(seg);
        }
    }
}
