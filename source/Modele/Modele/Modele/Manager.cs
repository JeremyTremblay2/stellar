using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Modele
{
    /// <summary>
    /// Le Manager est le coeur de l'application, c'est lui qui vient déléguer et gérer les diverses actions de l'application.
    /// Il se compose d'une Carte et d'une liste d'astres.
    /// </summary>
    public class Manager
    {
        //Données contenues dans le Manager.
        //La liste d'astres correspond au menu déroulant avec les informations des astres.
        //La carte correspond à l'ensemble des points et constellations qui se trouvent sur la partie éditeur.
        private List<Astre> lesAstres;

        /// <summary>
        /// Propriété en lecture seule concernant la liste d'astres, qui est l'ensembles de toutes les données (les astres) 
        /// de l'application.
        /// </summary>
        public ReadOnlyCollection<Astre> LesAstres { get; private set; }

        /// <summary>
        /// Propriété concernant la Carte, qui est l'endroit sur lequel se trouve tous les points (les astres), et 
        /// constellations de l'application.
        /// </summary>
        public Carte Carte { get; private set; }

        /// <summary>
        /// Constructeur de Manager. Il ne prend pas de paramètre.
        /// On instancie notre liste d'astre et notre Carte.
        /// </summary>
        public Manager()
        {
            lesAstres = new List<Astre>();
            LesAstres = new ReadOnlyCollection<Astre>(lesAstres);
            Carte = new Carte();
        }

        /// <summary>
        /// Méthode permettant l'ajout d'un astre à la carte et à la liste d'astres.
        /// Pour cela, on vérifie que le point et l'astre fournit en paramètre ne sont pas null.
        /// Si c'est le cas, on ajoute l'astre à la liste et on appelle une méthode dans notre Carte qui va venir l'ajouter à la position fournie.
        /// </summary>
        /// <param name="position">Le Point de la position de l'astre sur la carte.</param>
        /// <param name="astre">L'astre à ajouter à l'application.</param>
        public void AjouterUnAstre(Point position, Astre astre)
        {
            if (astre != null && position != null)
            {
                lesAstres.Add(astre);
                Carte.AjouterUnAstre(position, astre);
            }
        }

        /// <summary>
        /// Méthode permettant l'ajout d'un astre uniquement dans la liste d'astres (pas sur la carte).
        /// Pour cela, on vérifie que l'astre fournit en paramètre n'est pas null, et on l'ajoute à la liste.
        /// </summary>
        /// <param name="astre">L'astre à ajouter à la liste.</param>
        public void AjouterUnAstre(Astre astre)
        {
            if (astre != null)
            {
                lesAstres.Add(astre);
            }
        }

        /// <summary>
        /// Permet la suppression d'un astre sur la Carte. Nécéssite donc un point de coordonnées.
        /// La suppression du-dit astre se déroulera dans la classe Carte, cependant, l'astre éventuellement supprimé est retourné.
        /// On vérifie donc qu'il n'est pas null (si la suppression n'a pas eu lieu, si c'était par exemple un point dans une constellation),
        /// et qu'il s'agit d'un astre personnalisé (les astres pré-existants dans le logiciel ne peuvent jamais être effacés), et on peut
        /// alors le supprimer.
        /// </summary>
        /// <param name="position">Le Point correspondant à la position de l'astre a supprimer.</param>
        public void SupprimerUnAstre(Point position)
        {
            Astre astreASupprimer = Carte.SupprimerUnAstre(position);
            if (astreASupprimer != null && !astreASupprimer.Personnalise)
            {
                lesAstres.Remove(astreASupprimer);
            }
        }

        /// <summary>
        /// Méthode permettant de vider la manager de ses données, et donc d'effacer la liste d'astres et la Carte de ses données.
        /// Elle appelle la méthode contenue dans la Carte, qui va venir effacer son dictionnaire d'astres et sa liste de constellations.
        /// </summary>
        public void SupprimerTout()
        {
            lesAstres.Clear();
            Carte.SupprimerTout();
        }

        /// <summary>
        /// Méthode permettant de déplacer un astre qui se trouve sur la Carte, d'une position à une autre.
        /// Le manager ne fait que déléguer le travail à la Carte.
        /// </summary>
        /// <param name="anciennePosition">Un Point représentant l'ancienne position de l'astre.</param>
        /// <param name="nouvellePosition">Un Point représentant la nouvelle position de l'astre.</param>
        public void DeplacerUnAstre(Point anciennePosition, Point nouvellePosition)
            => Carte.DeplacerUnAstre(anciennePosition, nouvellePosition);

        /// <summary>
        /// Méthode permettant de relier deux étoiles qui se trouvent sur la Carte.
        /// Le manager ne fait que déléguer le travail à la Carte.
        /// </summary>
        /// <param name="point1">Le point de la première étoile à relier.</param>
        /// <param name="point2">Le point de la seconde étoile à relier.</param>
        public void RelierDeuxEtoiles(Point point1, Point point2)
            => Carte.RelierDeuxEtoiles(point1, point2);

        /// <summary>
        /// Méthode permettant l'affichage du Manager.
        /// Elle retourne une chaîne de caractères représentant le manager et les données qu'il contient, pour cela, elle affiche le nom 
        /// de chaque astre contenu dans la liste.
        /// </summary>
        /// <returns>Une chaîne de caractères qui permet de visualiser l'ensemble des astres du Manager.</returns>
        public override string ToString()
        {
            StringBuilder chaine = new StringBuilder();

            if (lesAstres.Count() == 0)
            {
                chaine.Append("\tAucun astre dans l'application.\n");
            }
            else
            {
                chaine.AppendFormat("{0} Astre(s) au total :\n", lesAstres.Count());
            }

            foreach (Astre astre in lesAstres)
            {
                chaine.AppendFormat("\t{0}\n", astre.Nom);
            }

            chaine.AppendLine("Voici la carte :");
            chaine.AppendFormat("{0}", Carte);

            return chaine.ToString();
        }
    }
}
