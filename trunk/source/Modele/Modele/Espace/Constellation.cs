using Geometrie;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Espace
{
    /// <summary>
    /// Une constellation est un ensemble de points reliés par des segments, le tout forme un graphe (les étoiles et les liens entre elles).
    /// </summary>
    public class Constellation : IEquatable<Constellation>
    {
        private HashSet<Point> lesPoints = new HashSet<Point>();
        private HashSet<Segment> lesSegments = new HashSet<Segment>();
        /*public HashSet<Point> LesPoints { get; private set; }
        public HashSet<Segment> LesSegments { get; private set; }*/
        /// <summary>
        /// Constructeur de Constellation
        /// </summary>
        /// <param name="pt1">Prends un premier point</param>
        /// <param name="pt2">Prends un deuxième point</param>
        public Constellation(Point pt1, Point pt2)
        {
            lesPoints.Add(pt1);
            lesPoints.Add(pt2);
            lesSegments.Add(new Segment(pt1, pt2));
            Vide = false;
        }

        public bool Vide { get; private set; }

        /// <summary>
        /// Permet de relier deux points ensembles via un segment.
        /// </summary>
        /// <param name="point1">Premier point à relier dans la constellation</param>
        /// <param name="point2">Second point à relier dans la constellation</param>
        public void Relier(Point pt, Point nvPt)
        {
			if (!lesPoints.Contains(pt))
            {
                throw new ArgumentException("Un des point ne se trouve pas la constellation.");
            } 
            else
            {
                lesPoints.Add(nvPt);
                lesSegments.Add(new Segment(pt, nvPt)); 
            }
        }

        /// <summary>
        /// Permets de supprmimer les liaisons avec le point
        /// </summary>
        /// <param name="pt">Point à supprimer de la constellation</param>
        public void SupprimerLesLiens(Point pt)
        {
            if (lesPoints.Contains(pt))
            {
                IEnumerable<Segment> tempo = lesSegments.Where(n => n.PtEquals(pt));
                lesSegments.ExceptWith(tempo);
                /*foreach (Segment seg in tempo)
                {
                    lesSegments.Remove(seg);
                }*/
                lesPoints.Remove(pt); // le point n'est plus dans la constellation
                CheckEtoiles();

                if (lesPoints.Count == 0 || lesSegments.Count == 0)
                {
                    Vide = true;
                }
            }
            
        }
        /// <summary>
        /// Permets de supprimer les points qui n'ont pas de liaison avec d'autres points
        /// </summary>
        private void CheckEtoiles()
        {
            if (lesSegments.Count == 0)
            {
                lesPoints.Clear();
            }
            else
            {
                foreach (Point pt in lesPoints)
                {
                    foreach (Segment seg in lesSegments)
                    {
                        if (!seg.PtEquals(pt))
                        {
                            lesPoints.Remove(pt);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Permets de déplacer un point et les segments impliqués
        /// </summary>
        /// <param name="ancienPt">Point à déplacer</param>
        /// <param name="nvPt">Nouvelles coordonnées</param>
        public void DeplacePoint(Point ancienPt, Point nvPt)
        {
            IEnumerable<Segment> tempo = lesSegments.Where(n => n.PtEquals(ancienPt));
            List<Segment> addSeg = new List<Segment>();
            foreach (Segment seg in tempo)
            {
                if (seg.PtEquals(ancienPt))
                {
                    if (seg.Point1.Equals(ancienPt))
                    {
                        addSeg.Add(new Segment(nvPt, seg.Point2));
                    }
                    else
                    {
                        addSeg.Add(new Segment(seg.Point1, nvPt));
                    }
                }
            }
            lesSegments.ExceptWith(tempo);
            lesSegments.UnionWith(addSeg);

            lesPoints.Remove(ancienPt);
            lesPoints.Add(nvPt);

        }

        /// <summary>
        /// Permets de fusionner de constellation. This fait union avec la constellation passée en paramètre.
        /// </summary>
        /// <param name="constel">Constellation avec laquelle fusionner</param>
        /// <param name="pt1">Premier point de liaison entre les deux constellations</param>
        /// <param name="pt2">Second point de liaison entre les deux constellations</param>
        public void FusionnerAvec(Constellation constel, Point pt1, Point pt2)
        {
            lesPoints.UnionWith(constel.lesPoints);
            lesSegments.UnionWith(constel.lesSegments);
            lesSegments.Add(new Segment(pt1, pt2));
        }

        public bool ContientLePoint(Point point) => lesPoints.Contains(point) ? true : false;

        public bool ContientLeSegment(Segment segment) => lesSegments.Contains(segment) ? true : false;


        public bool Equals([AllowNull] Constellation autre)
        {
            return lesPoints.Equals(autre.lesPoints) && lesSegments.Equals(autre.lesSegments);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Constellation);
        }

        public override string ToString()
        {
            StringBuilder chaine = new StringBuilder("Les points de la constellation :\n");

            foreach (Point point in lesPoints)
            {
                chaine.AppendFormat("\t{0}\n", point);
            }

            chaine.Append("Les segments de la constellation :\n");
            
            
            foreach (Segment segment in lesSegments)
            {
                chaine.AppendFormat("\t{0}\n", segment);
            }

            return chaine.ToString();
        }

        public override int GetHashCode()
        {
            return lesPoints.GetHashCode() + lesSegments.GetHashCode();
        }
    }
}
