using Geometrie;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Swordfish.NET.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Espace
{
    /// <summary>
    /// Une constellation est un ensemble de points reliés par des segments, le tout forme un graphe connexe (les étoiles et les 
    /// liens entre elles).
    /// </summary>
    [DataContract]
    public class Constellation : IEquatable<Constellation>
    {
        private static Random generateurCouleur = new Random();

        //Les hashsets de points et segments permettent de ne pas avoir de coordonnées en double.
        [DataMember]
        private HashSet<Point> lesPoints = new HashSet<Point>();
        [DataMember]
        private HashSet<Segment> lesSegments = new HashSet<Segment>();

        /// <summary>
        /// Propriété observable en lecture seule concernant l'hashset de points, qui sont l'ensemble des points 
        /// contenus dans une constellation.
        /// </summary>
        [DataMember]
        public ObservableCollection<Point> LesPoints { get; private set; } = new ObservableCollection<Point>();

        /// <summary>
        /// Propriété observable en lecture seule concernant l'hashset de segments, qui sont l'ensembles des liens 
        /// qui relient les étoiles entres elles.
        /// </summary>
        [DataMember]
        public ObservableCollection<Segment> LesSegments { get; private set; } = new ObservableCollection<Segment>();

        /// <summary>
        /// Propriété permettant de savoir si la constellation est vide, si elle ne contient pas de points et segments.
        /// </summary>
        [DataMember]
        public bool Vide { get; private set; }

        /// <summary>
        /// La couleur de la constellation, qui sera appliquée à tous ses segments.
        /// </summary>
        [DataMember]
        public string Couleur { get; set; } = GenerationCouleurAleatoire();

        /// <summary>
        /// Constructeur de Constellations. Prend deux points en paramètre et trace un segment.
        /// </summary>
        /// <param name="point1">Premier point de la constellation.</param>
        /// <param name="point2">Seconde point de la constellation.</param>
        public Constellation(Point point1, Point point2)
        {
            if (point1 == null || point2 == null)
            {
                throw new ArgumentException($"Un des point fournit lors de la création de la constellation est null." +
                    $"Le premier point : {point1}, le second : {point2}");
            }

            Segment s = new Segment(point1, point2);
            s.Couleur = Couleur;
            lesPoints.Add(point1);
            lesPoints.Add(point2);
            lesSegments.Add(s);
            MiseAJourCollections();

            Vide = false;
        }

        /// <summary>
        /// Constructeur de Constellations. Prend plusieurs points en paramètre et plusieurs segments. 
        /// Les ajoute ensuites aux ensembles.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="segments"></param>
        private Constellation(IEnumerable<Point> points, IEnumerable<Segment> segments)
        {
            if (points.Contains(null) || segments.Contains(null))
            {
                throw new ArgumentException($"Un des point ou segment fournit lors de la création de la constellation est null." +
                    $"Les points : {points}, les segments : {segments}");
            }

            lesPoints.UnionWith(points);
            lesSegments.UnionWith(segments);
            AppliqueCouleur();
            MiseAJourCollections();

            Vide = false;
        }

        /// <summary>
        /// Méthode permettant de relier deux points ensembles via un segment.
        /// Si le point passé en paramètre ne se trouve pas dans la constellation, alors une exception est levée.
        /// </summary>
        /// <param name="point">Premier point à contenu dans la constellation.</param>
        /// <param name="nouveauPoint">Second point qui va être ajouté à la constellation.</param>
        public void Relier(Point point, Point nouveauPoint)
        {
            if (!lesPoints.Contains(point))
            {
                throw new ArgumentException($"Le point donné en paramètre ne se trouve ps dans la constellation : {point}.");
            }

            Segment seg = new Segment(point, nouveauPoint);
            seg.Couleur = Couleur;
            lesPoints.Add(nouveauPoint);
            lesSegments.Add(seg);

            if (!LesPoints.Contains(nouveauPoint))
            {
                LesPoints.Add(nouveauPoint);
            }
            MiseAJourCollections();
        }

        /// <summary>
        /// Permet de supprmimer tous les segments d'un point, et ce même point (dans le cas d'une suppression par exemple).
        /// Lève une exception si le point passé en paramètre ne se trouve pas dans la constellation.
        /// </summary>
        /// <param name="point">Le point pour lequel on veut supprimer les liaisons.</param>
        public void SupprimerLesLiens(Point point)
        {
            if (!lesPoints.Contains(point))
            {
                throw new ArgumentException($"Le point donné en paramètre pour la suppression ne se trouve pas dans la constellation. " +
                    $"Point donné : {point}");
            }

            //On récupère tous les segments qui contenaient ce point, puis on les supprime du HashSet.
            IEnumerable<Segment> tempo = lesSegments.Where(n => n.PtEquals(point));
            lesSegments.ExceptWith(tempo);

            MiseAJourCollections();

            //Appel d'une méthode permettant de retirer de la constellation les étoiles seules (pas reliées).
            SuppressionPoints();

            if (lesPoints.Count == 0 || lesSegments.Count == 0)
            {
                Vide = true;
            }
        }

        /// <summary>
        /// Permet de déplacer un point et les segments impliqués à un nouveau point de position.
        /// Si un des point est null ou que le point à déplacer ne se trouve pas dans la constellation, alors on lève une exception.
        /// On récupère ensuite tous les segments qui contenaient l'ancien point dans une collection puis on les parcours. 
        /// Pour chaque segment, on crée un nouveau segment en fonction des coordonnées du nouveau point cette fois-ci.
        /// A la fin, on l'ajoute dans nos données, et on supprime les anciens segments. On fait de même pour le nouveau point.
        /// </summary>
        /// <param name="ancienPoint">Ancienne position du point.</param>
        /// <param name="nouveauPoint">Nouvelle position du point.</param>
        public void DeplacePoint(Point ancienPoint, Point nouveauPoint)
        {
            if (ancienPoint == null || nouveauPoint == null)
            {
                throw new ArgumentException($"Les points donnés en paramètre pour le déplacement ne peuvent pas être null. " +
                    $"Point de l'ancienne position : {ancienPoint}, point de la nouvelle position : {nouveauPoint}.");
            }
            if (!lesPoints.Contains(ancienPoint))
            {
                throw new ArgumentException($"Le point donné en paramètre pour le déplacement ne se trouve pas dans la constellation. " +
                    $"Point donné : {ancienPoint}.");
            }

            //On récupère tous les segments qui contiennent le point à déplacer.
            IEnumerable<Segment> tempo = lesSegments.Where(n => n.PtEquals(ancienPoint));
            List<Segment> addSeg = new List<Segment>();

            //On parcours les segments récupérés précédemment, et on les recrée en fonction du nouveau point.
            foreach (Segment seg in tempo)
            {
                if (seg.Point1.Equals(ancienPoint))
                {
                    Segment segTempo = new Segment(nouveauPoint, seg.Point2);
                    segTempo.Couleur = Couleur;
                    addSeg.Add(segTempo);
                }
                else
                {
                    Segment segTempo = new Segment(nouveauPoint, seg.Point1);
                    segTempo.Couleur = Couleur;
                    addSeg.Add(segTempo);
                }
            }

            //On supprime les anciens segments, et on ajoute les nouveaux.
            lesSegments.ExceptWith(tempo);
            lesSegments.UnionWith(addSeg);

            //On supprime l'ancien point et on ajoute le nouveau.
            lesPoints.Remove(ancienPoint);
            lesPoints.Add(nouveauPoint);

            MiseAJourCollections();
        }

        /// <summary>
        /// Permet de diviser une constellation en deux en utilisant un algorithme de parcours en largeur.
        /// Ne garde dans this qu'une seule composante connexe, et une retourne une constellation pouvant contenir plusieurs composantes
        /// connexes.
        /// </summary>
        /// <returns>Retourne une nouvelle constellation.</returns>
        public Constellation DiviserConstellation()
        {
            if (lesPoints.Count == 0) return null;
            //initialisation
            int i = 0, tailleLogique = 0, TaillePhysique = lesPoints.Count; //gestion de la pile
            Point[] pilePt = new Point[TaillePhysique]; //pile
            Point ptDeb = new Point(); //point de départ
            HashSet<Point> visite = new HashSet<Point>(); // les points déjà visités
            HashSet<Segment> segmentsConstel = new HashSet<Segment>(); // les segments dans la constellation actuelle

            ptDeb = lesPoints.First();

            //parcours en largeur
            pilePt[i] = ptDeb;
            tailleLogique++;
            while (pilePt[i] != null)
            {

                IEnumerable<Segment> tempoSeg = lesSegments.Where(seg => seg.PtEquals(pilePt[i]));
                foreach (Segment seg in tempoSeg)
                {
                    if (seg.Point1.Equals(pilePt[i]))
                    {
                        if (!visite.Contains(seg.Point2) && !pilePt.Contains(seg.Point2))
                        {
                            pilePt[tailleLogique] = seg.Point2;
                            tailleLogique++;
                        }
                    } 
                    else
                    {
                        if (!visite.Contains(seg.Point1) && !pilePt.Contains(seg.Point1))
                        {
                            pilePt[tailleLogique] = seg.Point1;
                            tailleLogique++;
                        }
                    }
                }
                segmentsConstel.UnionWith(tempoSeg);
                visite.Add(pilePt[i]);
                if (i == TaillePhysique - 1 || tailleLogique >= TaillePhysique)
                {
                    break;
                }
                i++;
            }

            //resultat
            if (tailleLogique >= TaillePhysique)
            {
                return null;
            }
            else
            {
                HashSet<Point> lesPt = new HashSet<Point>();
                HashSet<Segment> lesSeg = new HashSet<Segment>();
                lesPt.UnionWith(lesPoints);
                lesSeg.UnionWith(lesSegments);
                lesPoints.IntersectWith(visite);
                lesSegments.IntersectWith(segmentsConstel);
                lesPt.ExceptWith(visite);
                lesSeg.ExceptWith(segmentsConstel);
                MiseAJourCollections();
                return new Constellation(lesPt, lesSeg);
            }
        }

        /// <summary>
        /// Permet de fusionner deux constellations. This fait union avec la constellation passée en paramètre.
        /// On les lie par l'intermédiaire de deux points, et on crée un nouveau segment via ces deux points.
        /// </summary>
        /// <param name="constel">Constellation avec laquelle fusionner.</param>
        /// <param name="pt1">Premier point de liaison entre les deux constellations.</param>
        /// <param name="pt2">Second point de liaison entre les deux constellations.</param>
        public void FusionnerAvec(Constellation constel, Point point1, Point point2)
        {
            Segment seg = new Segment(point1, point2);

            lesPoints.UnionWith(constel.lesPoints);
            lesSegments.UnionWith(constel.lesSegments);
            lesSegments.Add(seg);
            AppliqueCouleur();
            MiseAJourCollections();
        }

        /// <summary>
        /// Méthode permettant de savoir si une constelaltion contient le point passé en paramètre.
        /// </summary>
        /// <param name="point">Le point dont on veut savoir s'il est contenu dans la constellation.</param>
        /// <returns>Un booléen indiquant si le point est contenu dans la constellation ou non.</returns>
        public bool ContientLePoint(Point point) => lesPoints.Contains(point) ? true : false;

        /// <summary>
        /// Protocole d'égalité permettant de savoir si une constellation passée en paramètre est égal à this, donc si elle possède 
        /// les mêmes points, mêmes segments au sein de ses collections.
        /// </summary>
        /// <param name="autre">Une constellation que l'on souhaite comparer à this, afin de voir si elles sont égales.</param>
        /// <returns>Un booléen qui indique si la constellation passée en paramètre est la même que this ou non.</returns>
        public bool Equals([AllowNull] Constellation autre)
        {
            return lesPoints.Equals(autre.lesPoints) && lesSegments.Equals(autre.lesSegments);
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est une constellation.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si cette constellation est égale à this, donc si elle 
        /// possède les mêmes points, les mêmes segments dans ses collections, en appellant la méthode Equals de cette constellation
        /// et en la castant en tant que tel.
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'une constellation</param>
        /// <returns>Un booléen qui indique si l'objet passé en paramètre est le même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Constellation);
        }

        /// <summary>
        /// Permet d'afficher une constellation sous format textuel. On affiche ses points et ses segments.
        /// </summary>
        /// <returns>Une chaîne de caractères représentant les données de la constellation.</returns>
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

        /// <summary>
        /// Permet la génération d'un hashcode, utilisé dans le cas des dictionnaire.
        /// Ce hashcode est définit par les points et segments contenus dans les collections de la constellation.
        /// </summary>
        /// <returns>Un entier représentatif du hashcode de cette constellation.</returns>
        public override int GetHashCode()
        {
            return lesPoints.GetHashCode() + lesSegments.GetHashCode();
        }

        /// <summary>
        /// Méthode permettant de supprimer les points qui n'ont pas de liaison avec d'autres points.
        /// Pour cela, on vérifie s'il n'y a plus de segments dans la constellation, et si c'est le cas, on peut effacer tous les points.
        /// Sinon, pour chaque point on parcours tous les segments, et on vérifie que le point est au moins contenu dans un segement.
        /// Si ce n'est pas le as, alors on l'ajoute à une collection.
        /// A la fin du traitement de tous les points, on supprime de notre hashset tous les points qui se trouvent dans la collection.
        /// </summary>
        private void SuppressionPoints()
        {
            //Les points qui seront effacés après le parcour se trouveront dans cette collection.
            HashSet<Point> pointsASupprimer = new HashSet<Point>();
            bool ptDansLesSegment = false;

            //S'il n'y a plus de segments, on peut effacer tous les points.
            if (lesSegments.Count == 0)
            {
                lesPoints.Clear();
            }

            foreach (Point pt in lesPoints)
            {
                foreach (Segment seg in lesSegments)
                {
                    //Si le point est contenu dans au moins un segment, alors on le mémorise dans une booléen.
                    if (seg.PtEquals(pt))
                    {
                        ptDansLesSegment = true;
                    }
                }

                //Si le point n'est contenu dans aucun segment, alors on l'ajoute à la liste des points à effacer.
                if (!ptDansLesSegment)
                {
                    pointsASupprimer.Add(pt);
                }
                ptDansLesSegment = false;
            }
            //On efface les points isolés que l'on a dans notre collection.
            lesPoints.ExceptWith(pointsASupprimer);

            foreach (Point point in pointsASupprimer)
            {
                if (LesPoints.Contains(point))
                {
                    LesPoints.Remove(point);
                }
            }
        }

        /// <summary>
        /// Méthode permettant la mise à jour des collections observables, en les vidant et en recopiant les données
        /// des hashsets.
        /// </summary>
        private void MiseAJourCollections()
        {
            LesSegments.Clear();
            LesPoints.Clear();
            foreach (Segment seg in lesSegments)
            {
                LesSegments.Add(seg);
            }
            foreach (Point pt in lesPoints)
            {
                LesPoints.Add(pt);
            }
        }

        /// <summary>
        /// Choisi une couleur aléatoire à l'instanciation de la constellation.
        /// </summary>
        private static string GenerationCouleurAleatoire()
        {
            System.Drawing.Color c = System.Drawing.Color.FromArgb(255, generateurCouleur.Next(256),
                generateurCouleur.Next(256), generateurCouleur.Next(256));
            c.ToArgb();
			return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");        }

        /// <summary>
        /// Applique la couleur sur tous les segments de la constellation.
        /// </summary>
        private void AppliqueCouleur()
        {
            foreach (Segment seg in lesSegments)
            {
                seg.Couleur = Couleur;
            }
        }
    }
}
