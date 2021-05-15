using Espace;
using System;
using Utilitaire;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Fonctionnels
{
    /// <summary>
    /// Classe permettant de tester les méthodes de recherches au sein d'une liste d'astre. 
    /// Ceci est la deuxième partie d'un gros fichier.
    /// </summary>
    public static partial class Test_Recherche
    {
        /// <summary>
        /// Méthode permettant l'affichage d'une liste d'astre dans la console.
        /// </summary>
        /// <param name="laListe">La liste d'astres à afficher.</param>
        public static void AfficherResultat(List<Astre> laListe)
        {
            foreach (Astre astre in laListe)
            {
                Console.WriteLine(astre);
            }
        }

        /// <summary>
        /// Méthode permettant de tester le tri par ordre alphabétique ascendant et descendant sur les noms des astres.
        /// </summary>
        public static void TestOrdreAlphabetique()
        {
            //Création de la liste qui va contenir notre tri.
            var lesAstresTries = new List<Astre>();

            Console.WriteLine("Voici la liste d'astres non triés :");
            AfficherResultat(lesAstres);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("-------------------------------------------------");

            //Appel du tri par ordre descendant.
            lesAstresTries = RechercheAstres.TriParOrdreAlphabetique(lesAstres, true);

            Console.WriteLine("Voici la liste d'astres après le tri par ordre alphabétique décroissant :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("-------------------------------------------------");

            //Appel du tri par ordre ascendant.
            lesAstresTries = RechercheAstres.TriParOrdreAlphabetique(lesAstres, false);

            Console.WriteLine("Voici la liste d'astres après le tri par ordre alphabétique croissant :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("-------------------------------------------------");
        }

        /// <summary>
        /// Méthode permettant de tester le filtrage des astres en favoris.
        /// </summary>
        public static void TestRechercheFavori()
        {
            var lesAstresTries = new List<Astre>();

            //Un astre sur deux de la liste sera en favori, sert à effectuer les tests qui vont suivre.
            for (int i = 0; i < lesAstres.Count; i = i + 2)
            {
                lesAstres[i].ModifierFavori();
            }

            lesAstresTries = RechercheAstres.RechercheParFavoris(lesAstres, false);

            //Affichage par défaut.
            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché tous les astres :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("-------------------------------------------------");

            lesAstresTries = RechercheAstres.RechercheParFavoris(lesAstres, true);

            //Affichage des favoris.
            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché uniquement les favoris :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("-------------------------------------------------");
        }

        /// <summary>
        /// Méthode de test permettant de vérifier le bon fonctionnement du filtrage par type.
        /// </summary>
        public static void TestRechercheParType()
        {
            var lesAstresTries = new List<Astre>();

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché tous les astres :");
            AfficherResultat(lesAstres);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("------------------------------------------------");

            lesAstresTries = RechercheAstres.RechercheParType(lesAstres, typeof(Etoile));

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché seulement les étoiles :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("-------------------------------------------------");

            lesAstresTries = RechercheAstres.RechercheParType(lesAstres, typeof(Planete));

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché seulement les planètes :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("-------------------------------------------------");
        }

        /// <summary>
        /// Méthode de test permettant de tester le filtrage des astres personnalisés / non personnalisés.
        /// </summary>
        public static void TestRechercheParPersonnalisation()
        {
            var lesAstresTries = new List<Astre>();

            lesAstresTries = RechercheAstres.RechercheParPersonnalisation(lesAstres, 0);

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché tous les astres (personnalisés ou non) :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("------------------------------------------------");

            //Uniquement les astres personnalisés.
            lesAstresTries = RechercheAstres.RechercheParPersonnalisation(lesAstres, 1);

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché seulement les astres personnalisés :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("Appuyez sur une touche pour voir la suite...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("-------------------------------------------------");

            //Uniquement les astres non personnalisés.
            lesAstresTries = RechercheAstres.RechercheParPersonnalisation(lesAstres, 2);

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché seulement les astres non personnalisés :");
            AfficherResultat(lesAstresTries);

            Console.WriteLine("-------------------------------------------------");
        }

        /// <summary>
        /// Méthode de test permettant de vérifier le bon fonctionnement de la recherche par nom.
        /// </summary>
        public static void TestRechercheParNom()
        {
            var lesAstresTries = new List<Astre>();

            Console.WriteLine("Voici la liste d'astres dans laquelle est affiché tous les astres :");
            AfficherResultat(lesAstres);

            //Récupération de l'entrée de l'utilisateur.
            Console.WriteLine("\n\nDonnez une chaîne de caractère qui sera recherchée dans le nom des astres :");
            string entree = Console.ReadLine();

            Console.WriteLine("------------------------------------------------");

            lesAstresTries = RechercheAstres.RechercheParNom(lesAstres, entree);

            if (lesAstresTries.Count == 0)
            {
                Console.WriteLine("Aucun astre dans la liste affichée précédemment ne contient la chaîne tappée en son nom.");
            }
            else
            {
                Console.WriteLine($"Voici les astres qui contiennent dans leur nom \"{entree}\" :");
                AfficherResultat(lesAstresTries);
            }

            Console.WriteLine("-------------------------------------------------");
        }
    }
}
