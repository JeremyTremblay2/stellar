using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Modele
{
    /// <summary>
    /// La carte est une entitée contenant tous les éléments disposés dessus. Cela peut être des astres, des constellations...
    /// Elle utilise un système de coordonnées.
    /// </summary>
    public class Carte : IEquatable<Carte>
    {
        //Générateur de constellations aléatoire à la création de la carte.
        private static Random generateurAleatoire = new Random();

        //Dictionnaire de données permettant de retrouver facilement un Astre grâce à sa position.
        private Dictionary<Point, Astre> lesAstres;
        //Les constellations présentes sur la carte (liste de données).
        private List<Constellation> lesConstellations;

        /// <summary>
        /// Propriété en lecture seule concernant le dictionnaire permettant de retrouver un Astre facilement présent sur la Carte, 
        /// à partir de son point de coordonnées (sa position en somme).
        /// </summary>
        public ReadOnlyDictionary<Point, Astre> LesAstres { get; private set; }

        /// <summary>
        /// Propriété en lecture seule concernant la liste des constellations qui composent la carte, ce sont des points reliés entre 
        /// eux par des segments.
        /// </summary>
        public ReadOnlyCollection<Constellation> LesConstellations { get; private set; }

        /// <summary>
        /// Constructeur de Carte. Une Carte est caractérisée par un dictionnaire d'Astres (étoiles et planètes), qui sont facilement accessibles
        /// via leur Point de coordonnées. Une carte se compose aussi d'une liste de constellations (des points reliés entre eux par des segments).
        /// On instancie ici également les collections en lectures seules afin que les champs soient bien encapsulés.
        /// </summary>
        /// <param name="avecCreations">Paramètre permettant de créer une carte avec déjà quelques éléments aléatoires dessus (des constellations,
        /// étoiles et planètes). S'il vaut true, alors une telle carte sera générée, si false, la carte sera construite vide.
        /// </param>
        public Carte(bool avecCreations = true)
        {
            lesAstres = new Dictionary<Point, Astre>();
            lesConstellations = new List<Constellation>();

            LesAstres = new ReadOnlyDictionary<Point, Astre>(lesAstres);
            LesConstellations = new ReadOnlyCollection<Constellation>(lesConstellations);

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
            if (position == null || astre == null)
            { 
                throw new ArgumentException($"Le point et l'astre qui vont être ajoutés ne peuvent pas être null." +
                    $"Voici le point : {position}, voici l'astre : {astre}");
            }

            lesAstres[position] = astre;
        }
        
        /// <summary>
        /// Permet de déplacer un astre d'un ancien point de position à un nouveau.
        /// On vérifie qu'aucun des paramètres est null, sinon on retourne une exception. Si le point de l'ancienne position de l'astre ne 
        /// se trouve pas dans le dictionnaire, on lève aussi une exception.
        /// Si tout est correct, on vient récupérer l'Astre correspondant dans une variable.
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
            if (anciennePosition == null || nouvellePosition == null)
            {
                throw new ArgumentException($"Les deux points fournis en paramètres qui vont servir au déplacement  ne peuvent pas être" +
                    $" null, voici l'ancien point : {anciennePosition}, voici le nouveau point {nouvellePosition}");
            }

            if (!lesAstres.ContainsKey(anciennePosition))
            {
                throw new ArgumentException($"Le dictionnaire d'astres ne contient aucune astre ayant ce point pour coordonnées." +
                    $"Le point donné en paramètre : {anciennePosition}");
            }

            Astre astreADeplacer = lesAstres[anciennePosition];

            if (astreADeplacer is Etoile)
            {
                //On récupère une éventuelle constellation qui possède ce point.
                Constellation tempoConstel = ParcoursConstellations(anciennePosition);
                if (tempoConstel != null)
                {
                    //On appelle la méthode de déplacement du point sur cette constellation.
                    lesConstellations[lesConstellations.IndexOf(tempoConstel)].DeplacePoint(anciennePosition, nouvellePosition);
                }
            }

            lesAstres.Remove(anciennePosition);
            lesAstres[nouvellePosition] = astreADeplacer;
        }

        /// <summary>
        /// Supprime un Astre en fonction d'un point cliqué. On vérifie que le point cliqué correspond bien à un astre qui se trouve 
        /// dans le dictionnaire et que le point n'est pas null, sinon on retourne une exception.
        /// S'il s'agit d'une étoile dans une constellation les segments reliés à l'étoile seront supprimés.
        /// Par la suite, on divise la constellations tant qu'elle n'est pas connexe avec tous ces points, et on
        /// enregistre les nouvelles constelaltions qui ont étét créées suite à cette opération, dans la liste.
        /// On vérifie par la suite si cette constellation est devenue vide suite à cela, si c'est le cas, on la supprime de la liste.
        /// Si par contre il ne s'agit pas d'une étoile ou bien que l'étoile ne se trouve pas dans une constellation, alors on supprime
        /// directement l'astre du dictionnaire, et on retourne cet astre supprimé.
        /// </summary>
        /// <param name="position">Point de l'astre à supprimer.</param>
        /// <returns>L'astre précédemment supprimé (null si le point passé en paramètre n'existe pas sur la carte).</returns>
        public Astre SupprimerUnAstre(Point position)
        {
            if (position == null || !lesAstres.ContainsKey(position))
            {
                throw new ArgumentException($"Le point fournit en paramètre pour la suppression ne peut pas être null, et doit être un " +
                    $"point référencé dans le dictionnaire d'astres. Le point donné en paramètre : {position}");
            }

            Astre astreARetourner = lesAstres[position];

            //Parcours des constellations afin de regarder si une constellation possède ce point.
            Constellation tempoConstel = ParcoursConstellations(position);
            if (astreARetourner is Etoile && tempoConstel != null)
            {
                //Si c'est le cas, on appele une méthode qui va venir supprimer les segments liés à cette constellation.
                int indexConstel = lesConstellations.IndexOf(tempoConstel);
                lesConstellations[indexConstel].SupprimerLesLiens(position);

                //Tant qu'on peut diviser la constallation en plusieurs composantes connexes, on le fait et on enregistrer ces nouvelles
                //constellations dans notre liste.
                Constellation nConstel = lesConstellations[indexConstel].DiviserConstellation();
                while (nConstel != null)
                {
                    lesConstellations.Add(nConstel);
                    nConstel = lesConstellations[lesConstellations.IndexOf(nConstel)].DiviserConstellation();
                }

                //SI la constellation est devenue vide suite à la suppression des liens, on la supprime.
                if (tempoConstel.Vide)
                {
                    lesConstellations.Remove(tempoConstel);
                }
            }

            //Si ce n'est pas une constellation, c'est une suppression pure et simple.
            else                
            {
                lesAstres.Remove(position);
                return astreARetourner;
            }
                                
            return null;
        }

        /// <summary>
        /// Méthode permettant l'ajout d'une constellation (d'un segment), entre deux étoiles (entre deux points).
        /// On vérifie que les points ne sont pas null, sinon on lève une exception.
        /// On récupère les deux astres correpondant aux deux points depuis la liste, et on vérifie qu'il s'agit bien de 
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
        /// <param name="point1">Le premier point cliqué par l'utilisateur.</param>
        /// <param name="point2">Le second point cliqué par l'utilisateur.</param>
        public void RelierDeuxEtoiles(Point point1, Point point2)
        {
            if (point1 == null || point2 == null)
            {
                throw new ArgumentException($"Les deux points fournis en paramètres qui vont servir à relier les étoiles  ne peuvent pas " +
                    $"être null, voici le premier point : {point1}, voici le second point {point2}");
            }

            //On récupère les astres correspondant aux points.
            Astre astre1, astre2;
            lesAstres.TryGetValue(point1, out astre1);
            lesAstres.TryGetValue(point2, out astre2);

            //Il faut que les deux points soient des étoiles !
            if (!(astre1 is Etoile) || !(astre2 is Etoile))
            {
                throw new InvalidOperationException("Il n'est possible de relier que des étoiles, pas des planètes.");
            }

            //On récupère les éventuelles constellations liées aux points donnés en paramètres.
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
        /// Protocole d'égalité permettant de savoir si une carte passée en paramètre est égal à this, donc si elle possède les mêmes
        /// constelations, points et astres.
        /// </summary>
        /// <param name="autre">Une carte que l'on souhaite comparer à this.</param>
        /// <returns>Un booléen qui indique si la carte passée en paramètre est la même que this ou non.</returns>
        public bool Equals([AllowNull] Carte autre)
        {
            return LesAstres.Equals(autre.LesAstres)
                && LesConstellations.Equals(LesConstellations);
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est une Carte.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si cette Carte est égale à this (en la castant), donc si 
        /// elle possède les mêmes constellations, points et astres.
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'une carte (et s'il s'agit de la même 
        /// carte que this)</param>
        /// <returns>Un booléen qui indique si l'objet passé en paramètre est le même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Carte);
        }

        /// <summary>
        /// Permet la génération d'un HashCode, utilisé dans le cas des dictionnaires.
        /// Ce hascode est définit par le dictionnaire de points et d'astres, ainsi que la liste de constellations de la carte.
        /// </summary>
        /// <returns>Un entier représentant le hashcode de cette carte.</returns>
        public override int GetHashCode()
        {
            return lesAstres.GetHashCode() + lesConstellations.GetHashCode();
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
