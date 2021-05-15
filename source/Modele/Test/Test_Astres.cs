using System;
using System.Collections.Generic;
using System.Linq;
using Espace;
using Modele;

namespace Test
{
    /// <summary>
    /// Classe de tests fonctionnels des Astres.
    /// </summary>
    public static class Test_Astres
    {
        /// <summary>
        /// Méthode de test principal, qui vient tester l'affichage des astres.
        /// </summary>
        public static void TestCreationAstres()
        {
            List<Etoile> lesEtoiles = new List<Etoile>()
            {

                new FabriqueDEtoile().Initialiser("Sirius")
                                      .AvecDescription("Sirius, également appelée Alpha Canis Majoris (α Canis Majoris/α CMa) par la " +
                                      "désignation de Bayer, est l'étoile principale de la constellation du Grand Chien. Vue de la " +
                                      "Terre, Sirius est l'étoile la plus brillante du ciel après le Soleil.")
                                      .AvecAge(250000000)
                                      .AvecMasse(1.03f)
                                      .AvecTemperature(9900)
                                      .AvecLuminosite(26.1f)
                                      .EstDansLaConstellation("Grand Chien")
                                      .AvecType(TypeEtoile.NaineBlanche)
                                      .Construire(),

                new FabriqueDEtoile().Initialiser("Soleil")
                                      .AvecDescription("Le Soleil est l’étoile du Système solaire. Dans la classification astronomique, " +
                                      "c’est une étoile de type naine jaune d'une masse d'environ 1,989 1 × 1030 kg, composée d’hydrogène et d’hélium.")
                                      .AvecAge(460000000)
                                      .AvecMasse(1f)
                                      .AvecTemperature(5773)
                                      .AvecLuminosite(1)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      .Construire(),

                new FabriqueDEtoile().Initialiser("Bételgeuse")
                                      .AvecDescription("Bételgeuse (α Orionis) est une étoile variable semi-régulière de type supergéante " +
                                      "rouge, dans la constellation d’Orion, située à une distance très difficile à établir.")
                                      .AvecAge(8000000)
                                      .AvecMasse(15)
                                      .AvecTemperature(3600)
                                      .AvecLuminosite(17)
                                      .EstDansLaConstellation("Orion")
                                      .AvecType(TypeEtoile.SupergeanteRouge)
                                      .Construire(),

                new FabriqueDEtoile().Initialiser("Castor")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire(),

                new FabriqueDEtoile().Initialiser("Pollux")
                                      .AvecDescription("Pollux (β Gem / Beta Geminorum) est l'étoile la plus brillante de la " +
                                      "constellation des Gémeaux et l'une des plus brillantes du ciel nocturne.")
                                      .AvecAge(7400000000)
                                      .AvecMasse(1.86f)
                                      .AvecTemperature(4865)
                                      .AvecLuminosite(32)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire(),
            };

            List<Planete> lesPlanetes = new List<Planete>()
            {
                new FabriqueDePlanete().Initialiser("Mars")
                                       .AvecDescription("Mars est la quatrième planète par ordre croissant de la distance au Soleil " +
                                       "et la deuxième par ordre croissant de la taille et de la masse.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(0.107f)
                                       .AvecTemperature(210)
                                       .PresenceDeVie("En cours de recherches")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Tellurique)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Vénus")
                                       .AvecDescription("Vénus est la deuxième planète du Système solaire par ordre d'éloignement au Soleil, " +
                                       "et la sixième plus grosse aussi bien par la masse que le diamètre. Elle doit son nom à la déesse " +
                                       "romaine de l'amour.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(0.815f)
                                       .AvecTemperature(737)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Tellurique)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Terre")
                                       .AvecDescription("La Terre est la troisième planète par ordre d'éloignement au Soleil et la cinquième " +
                                       "plus grande du Système solaire aussi bien par la masse que le diamètre. Par ailleurs, elle est le seul " +
                                       "objet céleste connu pour abriter la vie.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(1)
                                       .AvecTemperature(288)
                                       .PresenceDeVie("Oui")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(true)
                                       .AvecType(TypePlanete.Tellurique)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Mercure")
                                       .AvecDescription("Mercure est la planète la plus proche du Soleil et la moins massive du Système " +
                                       "solaire. Son éloignement au Soleil est compris entre 0,31 et 0,47 unité astronomique.")
                                       .AvecAge(4000000000)
                                       .AvecMasse(0.055f)
                                       .AvecTemperature(440)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Tellurique)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Jupiter")
                                       .AvecDescription("Jupiter est une planète géante gazeusea. Il s'agit de la plus grosse planète " +
                                       "du Système solaire, plus volumineuse et massive que toutes les autres planètes réunies, " +
                                       "et la cinquième planète par sa distance au Soleil.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(317.8f)
                                       .AvecTemperature(125)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Gazeuse)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Saturne")
                                       .AvecDescription("Saturne est la sixième planète du Système solaire par ordre d'éloignement " +
                                       "au Soleil, et la deuxième plus grande par la taille et la masse après Jupiter, qui est " +
                                       "comme elle une planète géante gazeuse.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(95.15f)
                                       .AvecTemperature(93)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Gazeuse)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Uranus")
                                       .AvecDescription("Uranus est la septième planète du Système solaire par ordre d'éloignement " +
                                       "au Soleil. Elle est la première planète découverte à l’époque moderne avec un télescope " +
                                       "et non connue depuis l'Antiquité.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(14.53f)
                                       .AvecTemperature(61)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Gazeuse)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Neptune")
                                       .AvecDescription("Neptune est la huitième planète par ordre d'éloignement au Soleil et la plus éloignée " +
                                       "connue du Système solaire. La distance de la planète à la Terre lui donnant une très faible taille apparente, " +
                                       "son étude est difficile avec des télescopes situés sur la Terre.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(17.14f)
                                       .AvecTemperature(58)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Gazeuse)
                                       .Construire(),

                new FabriqueDePlanete().Initialiser("Pluton")
                                       .AvecDescription("Pluton, officiellement désigné par (134340) Pluton, est une planète naine, la plus volumineuse " +
                                       "connue dans le Système solaire, et la deuxième en ce qui concerne sa masse. Après sa découverte par l'astronome " +
                                       "américain Clyde Tombaugh en 1930, Pluton était considérée comme la neuvième planète du Système solaire.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(0.002f)
                                       .AvecTemperature(48)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire externe")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Naine)
                                       .Construire(),
            };

            //Affichages.

            Console.WriteLine("Affichage des planètes :");

            foreach (Planete planete in lesPlanetes)
            {
                Console.WriteLine(planete);
                planete.SePresenter();
            }

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Affichage des étoiles :");

            foreach (Etoile etoile in lesEtoiles)
            {
                Console.WriteLine(etoile);
                etoile.SePresenter();
            }

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine($"Modification du favori de l'étoile {lesEtoiles[0].Nom} :");
            lesEtoiles[0].ModifierFavori();
            Console.WriteLine(lesEtoiles[0]);

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine($"Modification une seconde fois du favori de l'étoile {lesEtoiles[0].Nom} :");
            lesEtoiles[0].ModifierFavori();
            Console.WriteLine(lesEtoiles[0]);

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine($"{lesPlanetes[8].Nom} equals {lesPlanetes[3].Nom} ?  =>  {lesPlanetes[8].Equals(lesPlanetes[3])}");
            Console.WriteLine($"{lesPlanetes[4].Nom} equals {lesPlanetes[2].Nom} ?  =>  {lesPlanetes[4].Equals(lesPlanetes[2])}");
            Console.WriteLine($"{lesPlanetes[0].Nom} equals {lesPlanetes[1].Nom} ?  =>  {lesPlanetes[0].Equals(lesPlanetes[1])}");
            Console.WriteLine($"{lesPlanetes[0].Nom} equals {lesPlanetes[0].Nom} ?  =>  {lesPlanetes[0].Equals(lesPlanetes[0])}");

            Console.WriteLine("----------------------------------------------");

            Console.WriteLine($"{lesEtoiles[4].Nom} equals {lesEtoiles[1].Nom} ?  =>  {lesEtoiles[4].Equals(lesEtoiles[1])}");
            Console.WriteLine($"{lesEtoiles[2].Nom} equals {lesEtoiles[0].Nom} ?  =>  {lesEtoiles[2].Equals(lesEtoiles[0])}");
            Console.WriteLine($"{lesEtoiles[3].Nom} equals {lesEtoiles[3].Nom} ?  =>  {lesEtoiles[3].Equals(lesEtoiles[3])}");

            Console.WriteLine("----------------------------------------------");
        }
    }
}
