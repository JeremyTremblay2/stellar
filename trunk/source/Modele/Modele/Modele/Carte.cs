using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    /// <summary>
    /// La carte est une entitée contenant tous les éléments disposés dessus. Cela peut être des astres, des constellations...
    /// Elle utilise un système de coordonnées.
    /// </summary>
    public class Carte
    {
        private static Random generateurAleatoire = new Random();

        private Dictionary<Point, Astre> lesAstres;
        private List<Constellation> lesConstellations;

        /// <summary>
        /// Propriété concernant un dictionnaire permettant de retrouver un Astre facilement présent sur la Carte, à partir de son point
        /// de coordonnées (sa position en somme).
        /// </summary>
        public Dictionary<Point, Astre> LesAstres { get; private set; }

        /// <summary>
        /// Propriété concernant la liste des constellations qui composent la carte, ce sont des points reliés entre eux par des segments.
        /// </summary>
        public List<Constellation> LesConstellations { get; private set; }

        /// <summary>
        /// Constructeur de Carte. Une Carte est caractérisée par un dictionnaire d'Astres (étoiles et planètes), qui sont facilement accessibles
        /// via leur Point de coordonnées. Une carte se compose aussi d'une liste de constellations (des points reliés entre eux par des segments).
        /// </summary>
        /// <param name="avecCreations">Paramètre permettant de créer une carte avec déjà quelques éléments aléatoires dessus (des constellations,
        /// étoiles et planètes). S'il vaut true, alors une telle carte sera générée, si false, la carte sera construite vide.
        /// </param>
        public Carte(bool avecCreations)
        {
            lesAstres = new Dictionary<Point, Astre>();
            lesConstellations = new List<Constellation>();

            if (avecCreations)
            {
                //Ajouter des astres et constellations.
            }
        }
        
        /// <summary>
        /// Méthode permettant d'ajouter un astre à la carte (il peut s'agir d'une étoile ou d'une planète).
        /// Pour cela, si le point ou l'astre n'est pas null (on lève un exception le cas échéant), on vient l'ajouter au dictionnaire 
        /// déjà existant.
        /// </summary>
        /// <param name="position">Le Point de l'Astre qui va être positionné sur la carte.</param>
        /// <param name="astre">L'astre qui va être positionné sur la carte.</param>
        public void AjouterUnAstre(Point position, Astre astre)
        {
            if (position != null && astre != null)
                lesAstres[position] = astre;
            else
            {
                throw new ArgumentException("Le point et l'astre qui vont être ajoutés ne peuvent pas être null.");
            }
        }
        
        /// <summary>
        /// Permet de déplacer un astre d'un ancien point de position à un nouveau.
        /// Pour cela, on vérifie que le dictionnaire contient bien le premier point, et on vient récupérer l'Astre correspondant dans une variable.
        /// Si l'astre ne se trouve pas dans une constellation, alors on vient le supprimer du dictionnaire, puis on l'ajouter à nouveau,
        /// avec le nouveau point de position.
        /// Si l'astre est une étoile, on appelle la méthode de parcours de constellations afin de savoir si cette étoile se trouve dans une
        /// constellation. Si c'est le cas, on appelle une méthode sur cette constellation pour déplacer tous ses segments sur le nouveau point
        /// de coordonnées.
        /// A la fin, l'astre voit se position changer, ainsi que ses segments s'il s'agit d'une constellation.
        /// </summary>
        /// <param name="anciennePosition">Ancienne position de l'astre, qui va être modifiée.</param>
        /// <param name="nouvellePosition">Nouvelle position de l'astre, qui sera appliquée.</param>
        public void DeplacerUnAstre(Point anciennePosition, Point nouvellePosition)
        {
            if (lesAstres.ContainsKey(anciennePosition) && nouvellePosition != null)
            {
                Astre astreADeplacer = lesAstres[anciennePosition];

                if (astreADeplacer is Etoile)
                {
                    Constellation tempoConstel = ParcoursConstellations(anciennePosition);
                    if (tempoConstel != null)
                    {
                        lesConstellations[lesConstellations.IndexOf(tempoConstel)].DeplacePoint(anciennePosition, nouvellePosition);
                    }
                }

                lesAstres.Remove(anciennePosition);
                lesAstres[nouvellePosition] = astreADeplacer;
            }
        }

        /// <summary>
        /// Supprime un Astre en fonction d'un point cliqué. On vérifie qur le point cliqué correspond bien à un astre.
        /// S'il s'agit d'une étoile dans une constellation les segments reliés à l'étoile seront supprimés.
        /// On vérifie par la suite si cette constellation est devenue vide suite à cela, si c'est le cas, on la supprime de la liste.
        /// Si par contre il ne s'agit pas d'une étoile ou bien que l'étoile ne se trouve pas dans une constellation, alors on supprime
        /// directement l'astre du dictionnaire, et on retourne cet astre supprimé.
        /// </summary>
        /// <param name="position">Point de l'astre à supprimer.</param>
        /// <returns>L'astre précédemment supprimé (null si le point passé en paramètre n'existe pas sur la carte).</returns>
        public Astre SupprimerUnAstre(Point position)
        {
            if (lesAstres.ContainsKey(position))
            {
                Astre astreARetourner = lesAstres[position];
                Constellation tempoConstel = ParcoursConstellations(position);
                if (astreARetourner is Etoile && tempoConstel != null)
                {
                    lesConstellations[lesConstellations.IndexOf(tempoConstel)].SupprimerLesLiens(position);
                    if (tempoConstel.Vide)
                    {
                        lesConstellations.Remove(tempoConstel);
                    }
                }
                else                
                {
                    lesAstres.Remove(position);
                    return astreARetourner;
                }
                                
            }
            return null;
        }

        /// <summary>
        /// Méthode permettant l'ajout d'une constellation (d'un segment), entre deux étoiles (entre deux points).
        /// Pour ce faire, on récupère les deux astres correpondant aux deux points depuis la liste, et on vérifie qu'il s'agit bien de 
        /// deux étoiles. Si ce n'est pas le cas, on ne peut pas les relier et on lève une exception.
        /// On appelle par la suite une méthode qui vient réaliser un parcours de la liste des constellations. Elle retourne alors les 
        /// constellations qui contiennent ce point (s'il y en a). On a alors plusieurs cas de figure :
        ///     -Si les deux points ne se trouvent dans aucune constellation, alors il faut créer une nouvelle constellation à partir de
        ///     ces deux points, et l'ajouter à la liste.
        ///     -Si un des deux points se trouve dans une constellation, on vient alors réaliser une extension de la constellation, et on
        ///     relie ce premier point au deuxième (ou l'inverse).
        ///     -Si les deux points se trouvent déjà dans deux constellations distinctes, alors on fusionne ces deux constellations et on 
        ///     en supprime une de la liste.
        /// </summary>
        /// <param name="point1">Le premier point cliqué par l'utilisateur</param>
        /// <param name="point2">Le second point cliqué par l'utilisateur</param>
        public void RelierDeuxEtoiles(Point point1, Point point2)
        {
            //On récupère les astres correspondant aux points.
            Astre astre1, astre2;
            lesAstres.TryGetValue(point1, out astre1);
            lesAstres.TryGetValue(point2, out astre2);

            //Il faut que les deux points soient des étoiles !
            if (astre1 is Etoile && astre1 is Etoile)
            {
                Constellation const1 = ParcoursConstellations(point1);
                Constellation const2 = ParcoursConstellations(point2);

                if (const1 == null && const2 == null)
                {
                    lesConstellations.Add(new Constellation(point1, point2));
                } 
                else if (const1 != null && const2 == null)
                {
                    lesConstellations[lesConstellations.IndexOf(const1)].Relier(point1, point2);
                } 
                else if (const1 == null && const2 != null)
                {
                    lesConstellations[lesConstellations.IndexOf(const2)].Relier(point2, point1);
                } 
                else if (const1 != null && const2 != null && !const1.Equals(const2))
                {
                    lesConstellations[lesConstellations.IndexOf(const1)].FusionnerAvec(const2, point1, point2);
                    lesConstellations.Remove(const2);
                }
            }
            else
            {
                throw new InvalidOperationException("Il n'est possible de relier ques des étoiles, pas des planètes.");
            }
        }

        /// <summary>
        /// Méthode qui vient vider le dictionnaire d'Astres et supprimer toutes les constellations.
        /// </summary>
        public void SupprimerTout()
        {
            lesAstres.Clear();
            lesConstellations.Clear();
        }

        /// <summary>
        /// Méthode permettant l'affichage de la Carte. Une Carte affiche d'abord tous ses Astres (Point de l'astre et le nom de l'astre).
        /// On vient ensuite afficher toutes les constellation
        /// </summary>
        /// <returns>Une chaîne de caractères contenant les différents éléments de cette carte.</returns>
        public override string ToString()
        {
            StringBuilder chaine = new StringBuilder("Astres présents sur la carte :\n");

            if (lesAstres.Count() == 0)
            {
                chaine.Append("\tAucun Astre.\n");
            }

            foreach(KeyValuePair<Point, Astre> kvp in lesAstres)
            {
                chaine.AppendFormat("\t{0} : {1}\n", kvp.Key, kvp.Value.Nom);
            }

            chaine.Append("\nConstellations présentes sur la carte :\n");

            if (lesConstellations.Count() == 0)
            {
                chaine.Append("\tAucune constellation\n");
            }

            foreach (Constellation constellation in lesConstellations)
            {
                chaine.AppendFormat("\t{0}\n", constellation);
            }

            return chaine.ToString();
        }

        /// <summary>
        /// Parcours la liste des constellations et retourne la première constellation qui contient le point passé en paramètre.
        /// Si aucune constellation ne possède le point passé en paramètre, retourne null.
        /// </summary>
        /// <param name="position">Point contenu dans la constellation retournée.</param>
        /// <returns>Une constellation contenant le point passé en paramètre.</returns>
        private Constellation ParcoursConstellations(Point position)
        {
            foreach (Constellation constellation in lesConstellations)
            {
                if (constellation.ContientLePoint(position))
                {
                    return constellation;
                }
            }
            return null;
        }
    }
}
