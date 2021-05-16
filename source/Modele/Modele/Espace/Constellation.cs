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

        private Constellation(IEnumerable<Point> pt, IEnumerable<Segment> seg)
        {
            lesPoints.UnionWith(pt);
            lesSegments.UnionWith(seg);
            Vide = false;
        }

        public bool Vide { get; private set; }

        /// <summary>
        /// Permet de relier deux points ensembles via un segment.
        /// </summary>
        /// <param name="point1">Premier point à relier dans la constellation</param>
        /// <param name="point2">Second point à relier dans la constellation</param>
        public void Relier(Point point, Point nouveauPoint)
        {
			if (!lesPoints.Contains(point))
            {
                throw new ArgumentException("Un des point ne se trouve pas la constellation.");
            } 
            else
            {
                lesPoints.Add(nouveauPoint);
                lesSegments.Add(new Segment(point, nouveauPoint)); 
            }
        }

        /// <summary>
        /// Permets de supprmimer les liaisons avec le point
        /// </summary>
        /// <param name="pt">Point à supprimer de la constellation</param>
        public void SupprimerLesLiens(Point point)
        {
            if (lesPoints.Contains(point))
            {
                IEnumerable<Segment> tempo = lesSegments.Where(n => n.PtEquals(point));
                lesSegments.ExceptWith(tempo);
                /*foreach (Segment seg in tempo)
                {
                    lesSegments.Remove(seg);
                }*/
                //lesPoints.Remove(pt); // le point n'est plus dans la constellation
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
            HashSet<Point> pointsASupprimer = new HashSet<Point>();
            bool ptDansLesSegment = false;
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
                        if (seg.PtEquals(pt))
                        {
                            ptDansLesSegment = true;
                        }
                    }
                    if(!ptDansLesSegment)
                    {
                        pointsASupprimer.Add(pt);
                    }
                    ptDansLesSegment = false;
                }
                lesPoints.ExceptWith(pointsASupprimer);
            }
        }

        /// <summary>
        /// Permets de déplacer un point et les segments impliqués
        /// </summary>
        /// <param name="ancienPt">Point à déplacer</param>
        /// <param name="nvPt">Nouvelles coordonnées</param>
        public void DeplacePoint(Point ancienPoint, Point nouveauPoint)
        {
            IEnumerable<Segment> tempo = lesSegments.Where(n => n.PtEquals(ancienPoint));
            List<Segment> addSeg = new List<Segment>();
            foreach (Segment seg in tempo)
            {
                if (seg.PtEquals(ancienPoint))
                {
                    if (seg.Point1.Equals(ancienPoint))
                    {
                        addSeg.Add(new Segment(nouveauPoint, seg.Point2));
                    }
                    else
                    {
                        addSeg.Add(new Segment(seg.Point1, nouveauPoint));
                    }
                }
            }
            lesSegments.ExceptWith(tempo);
            lesSegments.UnionWith(addSeg);

            lesPoints.Remove(ancienPoint);
            lesPoints.Add(nouveauPoint);

        }
        /// <summary>
        /// Permets de diviser une constellation en deux en utilisant un algorithme de parcours en largeur.
        /// </summary>
        /// <returns>Retourne une nouvelle constellation</returns>
        public Constellation DiviserConstellation()
        {
            //initialisation
            int i=0, tailleLogique = 0, TaillePhysique = lesPoints.Count; //gestion de la pile
            Point[] pilePt = new Point[TaillePhysique]; //pile
            Point ptDeb = new Point(); //point de départ
            HashSet<Point> visite = new HashSet<Point>(); // les points déjà visités
            HashSet<Segment> visiteSeg = new HashSet<Segment>(); // les points déjà visités
            foreach (Point pt in lesPoints) //sélection d'un point de départ
            {
                ptDeb = pt;
                break;
            }
            //parcours en largeur
            pilePt[i] = ptDeb;
            tailleLogique++;
            while(pilePt[i] != null)
            {
                
                IEnumerable<Segment> tempoSeg = lesSegments.Where(n => n.PtEquals(pilePt[i]));
                foreach(Segment seg in tempoSeg)
                {
                    if(seg.Point1.Equals(pilePt[i]))
                    {
                        if (!visite.Contains(seg.Point2))
                        {
                            pilePt[tailleLogique] = seg.Point2;
                            tailleLogique++;
                        }
                    } else
                    {
                        if (!visite.Contains(seg.Point1))
                        {
                            pilePt[tailleLogique] = seg.Point1;
                        tailleLogique++;
                        }
                    }
                }
                visiteSeg.UnionWith(tempoSeg);
                visite.Add(pilePt[i]);
                if (i == TaillePhysique-1)
                {
                    break;
                }
                i++;
            }

            //resultat
            if (visite.Count < lesPoints.Count)
            {
                HashSet<Point> lesPt = new HashSet<Point>();
                HashSet<Segment> lesSeg = new HashSet<Segment>();
                lesPt.UnionWith(lesPoints);
                lesSeg.UnionWith(lesSegments);
                lesPoints.IntersectWith(visite);
                lesSegments.IntersectWith(visiteSeg);
                lesPt.ExceptWith(visite);
                lesSeg.ExceptWith(visiteSeg);
                return new Constellation(lesPt, lesSeg);
            } else
            {
                return null;
            }
        }

        /// <summary>
        /// Permets de fusionner de constellation. This fait union avec la constellation passée en paramètre.
        /// </summary>
        /// <param name="constel">Constellation avec laquelle fusionner</param>
        /// <param name="pt1">Premier point de liaison entre les deux constellations</param>
        /// <param name="pt2">Second point de liaison entre les deux constellations</param>
        public void FusionnerAvec(Constellation constel, Point point1, Point point2)
        {
            lesPoints.UnionWith(constel.lesPoints);
            lesSegments.UnionWith(constel.lesSegments);
            lesSegments.Add(new Segment(point1, point2));
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
