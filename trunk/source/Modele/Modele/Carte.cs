using System;
using System.Collections.Generic;
using System.Linq;

namespace Modele
{
    public class Carte
    {
        private static Random generateurAleatoire = new Random();

        private Dictionary<Point, Astre> lesAstres;
        private List<Constellation> lesConstellations;

        /// <summary>
        /// Constructeur de Carte. Une Carte est caractérisée par un dictionnaire d'Astres (étoiles et planètes), qui sont facilement accessibles
        /// via leur Point de coordonnées. Une carte se compose aussi d'une liste de constellations (des points reliés entre eux par des segments).
        /// </summary>
        /// <param name="avecCreation">Paramètre permettant de créer une carte avec déjà quelques éléments aléatoires dessus (des constellations,
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
        /// Pour cela, si le point ou l'astre n'est pas null, on vient l'ajouter au dictionnaire déjà existant.
        /// </summary>
        /// <param name="position">Le Point de l'Astre qui va être positionné sur la carte.</param>
        /// <param name="astre">L'astre qui va être positionné sur la carte.</param>
        public void AjouterUnAstre(Point position, Astre astre)
        {
            if (position != null && astre != null)
                lesAstres[position] = astre;
        }
        
        /// <summary>
        /// Permets de déplacer un astre
        /// </summary>
        /// <param name="anciennePosition">Position de l'astre à déplacer</param>
        /// <param name="nouvellePosition">Ca nouvelle position</param>
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
                        tempoConstel.DeplacePoint(anciennePosition, nouvellePosition);
                    }
                }

                lesAstres.Remove(anciennePosition);
                lesAstres[nouvellePosition] = astreADeplacer;
            }
        }

        /// <summary>
        /// Supprime un Astre. Si c'est une étoile dans une constellation les segments reliés à l'étoile seront supprimés.
        /// </summary>
        /// <param name="position">Astre à supprimer</param>
        /// <returns></returns>
        public Astre SupprimerUnAstre(Point position)
        {
            if (lesAstres.ContainsKey(position))
            {
                Astre astreARetourner = lesAstres[position];
                Constellation tempoConstel = ParcoursConstellations(position);
                if (astreARetourner is Etoile && tempoConstel != null)
                {
                    tempoConstel.SupprimerLesLiens(position);
                } else
                {
                    lesAstres.Remove(position);
                    return astreARetourner;
                }
                                
            }
            return null;
        }

        /// <summary>
        /// Méthode permettant l'ajout d'une constellation (d'un segment), entre deux étoiles (entre deux points).
        /// Pour ce faire, on crée un segment entre les deux points, et on vient parcourir les constellations.
        /// On vérifie que le segment n'existe pas déjà, et on relie les deux points ensembles s'ils se trouvent déjà dans la constellation,
        /// sinon on ajoute un point et on le relie au point qui était déjà existant.
        /// Si aucun des points n'existaient dans aucune constellation, alors on en crée une nouvelle.
        /// </summary>
        /// <param name="point1">Le premier point cliqué par l'utilisateur</param>
        /// <param name="point2">Le second point cliqué par l'utilisateur</param>
        public void RelierDeuxEtoiles(Point point1, Point point2)
        {
            Astre astre1, astre2;
            lesAstres.TryGetValue(point1, out astre1);
            lesAstres.TryGetValue(point2, out astre2);
            if (astre1 is Etoile && astre1 is Etoile)
            {
                Constellation const1 = ParcoursConstellations(point1);
                Constellation const2 = ParcoursConstellations(point2);

                if (const1 == null && const2 == null)
                {
                    lesConstellations.Add(new Constellation(point1, point2));
                } else if (const1 != null && const2 == null)
                {
                    const1.Relier(point1, point2);
                } else if (const1 == null && const2 != null)
                {
                    const2.Relier(point2, point1);
                } else if (const1 != null && const2 != null && !const1.Equals(const2))
                {
                    const1.FusionnerAvec(const2, point1, point2);
                    lesConstellations.Remove(const2);
                }
            }
        }

        /// <summary>
        /// Parcours la liste lesConstellations et retourne la constellation qui contient le point passé en paramètre
        /// </summary>
        /// <param name="position">Point contenu dans la constellation retournée</param>
        /// <returns></returns>
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
        /// <returns></returns>
        public override string ToString()
        {
            string chaine = "Astres présents sur la carte :\n";

            if (lesAstres.Count() == 0)
            {
                chaine += "\tAucun Astre.\n";
            }

            foreach(KeyValuePair<Point, Astre> kvp in lesAstres)
            {
                chaine += $"\t{kvp.Key} : {kvp.Value.Nom}\n";
            }

            chaine += "\nConstellations présentes sur la carte :\n";

            if (lesConstellations.Count() == 0)
            {
                chaine += "\tAucune constellation\n";
            }

            foreach (Constellation constellation in lesConstellations)
            {
                chaine += $"\t{constellation}\n";
            }

            return chaine;
        }
    }
}
