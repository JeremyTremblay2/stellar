using Espace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilitaire
{
    /// <summary>
    /// Méthode utilitaire utilisant LINQ, et permettant le filtrage d'une liste d'astres via diverses méthodes.
    /// </summary>
    public static class RechercheAstres
    {
        /// <summary>
        /// Méthode permettant de créer une liste d'astres ne contenant que la chaîne de caractères dans le nom passée en paramètre 
        /// à partir d'une liste pré-existante.
        /// </summary>
        /// <param name="lesAstres">La liste d'astres à filtrer.</param>
        /// <param name="nom">La chaîne de caractères qui va être recherchée dans le nom de tous les astres de la liste.</param>
        /// <returns>Une nouvelle liste d'astres qui ne contiennent que le nom fournit en paramètre.</returns>
        public static List<Astre> RechercheParNom(List<Astre> lesAstres, string nom)
            => lesAstres.Where(astre => astre.Nom.ToLower().Contains(nom.ToLower())).ToList();

        /// <summary>
        /// Méthode permettant d'ordonner une liste d'astres par ordre alphabétique de leurs noms, en fonction de la valeur passée en 
        /// paramètre (ascendant ou descendant).
        /// </summary>
        /// <param name="lesAstres">La liste d'astres à trier.</param>
        /// <param name="ordre">Un booléen indiquant l'ordre (ascendant ou descendant) du tri.
        ///                     -true -> ascendant
        ///                     -false -> descandant
        /// </param>
        /// <returns>Une nouvelle liste d'astres triée par ordre alphabétique du nom des astres.</returns>
        public static List<Astre> TriParOrdreAlphabetique(List<Astre> lesAstres, bool ordre)
            => ordre ? lesAstres.OrderBy(astre => astre.Nom).ToList() : lesAstres.OrderByDescending(astre => astre.Nom).ToList();

        /// <summary>
        /// Méthode permettant de créer une liste d'astres à partir d'une liste pré-existante, et ne contenant que les astres personnalisés, 
        /// les astres non personnalisés ou tous, en fonction de la valeur passée en paramètre. 
        /// </summary>
        /// <param name="lesAstres">La liste d'astres à filtrer.</param>
        /// <param name="personnalise">Un entier indiquant comment devra être filtré la liste d'astres. 
        ///                             -1 -> on ne garde que les astres personnalisés
        ///                             -2 -> on ne garde que les astres non personnalisés
        ///                             -autre valeur -> on garde tous les astres
        /// </param>
        /// <returns>Une nouvelle liste d'astres qui ne contiennent que les astres personnalisés, que les astres non personnalisés, 
        /// ou une liste inchangée.</returns>
        public static List<Astre> RechercheParPersonnalisation(List<Astre> lesAstres, byte personnalise)
        {
            switch(personnalise)
            {
                case 1:
                    return lesAstres.Where(astre => astre.Personnalise).ToList();
                case 2:
                    return lesAstres.Where(astre => !astre.Personnalise).ToList();
                default:
                    return lesAstres;
            }
        }

        /// <summary>
        /// Méthode permettant de créer une liste d'astres à partir d'une liste pré-existante, ne contenant que les astres d'un certain 
        /// type en fonction de celui passé en paramètre.
        /// </summary>
        /// <param name="lesAstres">La liste d'astres à filtrer.</param>
        /// <param name="type">Le type de l'astre a conserver (Etoile ou Planete).</param>
        /// <returns>Une nouvelle liste d'astres qui ne contiennent que le type d'astre fournit en paramètre.</returns>
        public static List<Astre> RechercheParType(List<Astre> lesAstres, Type type)
            => lesAstres.Where(astre => astre.GetType() == type).ToList();

        /// <summary>
        /// Méthode permettant de créer une liste d'astres à partir d'une liste pré-existante, ne contenant que les astres contenus dans 
        /// les favoris, ou tous, en fonction d'une valeur passée en paramètre.
        /// </summary>
        /// <param name="lesAstres">La liste d'astres à filtrer.</param>
        /// <param name="favori">Un booléen indiquant la manière de filtrer la liste :
        ///                       -true -> on ne garde que les astres personnalisés
        ///                       -false -> on garde la liste telle quelle.
        /// </param>
        /// <returns>Une nouvelle liste d'astres qui ne contiennent que les astres favoris, ou tous les astres.</returns>
        public static List<Astre> RechercheParFavoris(List<Astre> lesAstres, bool favori)
            => favori ? lesAstres.Where(astre => astre.Favori).ToList() : lesAstres;
    }
}
