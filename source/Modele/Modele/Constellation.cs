using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Une constellation est un ensemble de points reliés par des segments, le tout forme un graphe (Les étoiles et les liens entre elles)
    /// </summary>
    public class Constellation
    {
        private HashSet<Point> lesPoints = new HashSet<Point>();
        private HashSet<Segment> lesSegments = new HashSet<Segment>();

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
        /// Permets de relier deux points
        /// </summary>
        /// <param name="nvPt">Nouveau point à relier dans la constellation</param>
        /// <param name="pt">Point selectionné dans la constellation et devant être relier avec nvPt</param>
        public void Relier(Point nvPt, Point pt) //Ajoute un Point dans la liste lesPoints
        {
            if(!lesPoints.Contains(pt))
            {
                throw new ArgumentException("l'étoile selectionnée n'est pas dans la constellation");
            } else
            {
                lesPoints.Add(nvPt);
                lesPoints.Add(pt);
                lesSegments.Add(new Segment(nvPt, pt)); 
            }
        }
        /// <summary>
        /// Permets de supprmimer les liaisons avec le point
        /// </summary>
        /// <param name="pt">Point à supprimer de la constellation</param>
        public void SupprimerLesLiens(Point pt)
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

        public override string ToString()
        {
            string strPt = "Liste de points :\n";
            string strSeg = "Liste de segments :\n";
            
            foreach (Point pt in lesPoints)
            {
                strPt += $"{pt.ToString()}\n";
            }
            foreach (Segment seg in lesSegments)
            {
                strSeg += $"{seg.ToString()}\n";
            }
            return $"{strPt}\n{strSeg}\nvide : {Vide}";
        }
    }
}
