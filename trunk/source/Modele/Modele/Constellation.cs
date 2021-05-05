using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Constellation répertorie tous les points et les segments lui faisant partie (Les étoiles et les liens entre elles)
    /// </summary>
    public class Constellation
    {
        private List<Point> lesPoints = new List<Point>();
        private List<Segment> lesSegments = new List<Segment>();

        /// <summary>
        /// Constructeur de Constellation
        /// </summary>
        /// <param name="points">Prends une liste de points</param>
        /// <param name="segments">Prends une liste de segments</param>
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
        /// Ajoute un Point dans la list lesPoints
        /// </summary>
        /// <param name="pt">Point à ajouter</param>
        public void AjoutPoint(Point pt)
        {
            lesPoints.Add(pt);
        }

        /// <summary>
        /// Ajoute un Segment dans la list lesSegments
        /// </summary>
        /// <param name="seg">Segment à ajouter</param>
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
