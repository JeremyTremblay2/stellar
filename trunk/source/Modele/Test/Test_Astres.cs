using System;
using System.Collections.Generic;
using System.Linq;
using Modele;

namespace Test
{
    public class Test_Astres
    {
        public void Test()
        {
            List<Etoile> lesEtoiles = new List<Etoile>()
            {

                new FabriqueDEtoile().Initialiser(false)
                                      .AvecNom("Sirius")
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

                new FabriqueDEtoile().Initialiser(false)
                                      .AvecNom("Soleil")
                                      .AvecDescription("Le Soleil est l’étoile du Système solaire. Dans la classification astronomique, " +
                                      "c’est une étoile de type naine jaune d'une masse d'environ 1,989 1 × 1030 kg, composée d’hydrogène et d’hélium.")
                                      .AvecAge(460000000)
                                      .AvecMasse(1f)
                                      .AvecTemperature(5773)
                                      .AvecLuminosite(1)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      .Construire(),

                new FabriqueDEtoile().Initialiser(false)
                                      .AvecNom("Bételgeuse")
                                      .AvecDescription("Bételgeuse (α Orionis) est une étoile variable semi-régulière de type supergéante " +
                                      "rouge, dans la constellation d’Orion, située à une distance très difficile à établir.")
                                      .AvecAge(8000000)
                                      .AvecMasse(15)
                                      .AvecTemperature(3600)
                                      .AvecLuminosite(17)
                                      .EstDansLaConstellation("Orion")
                                      .AvecType(TypeEtoile.SupergeanteRouge)
                                      .Construire(),

                new FabriqueDEtoile().Initialiser(false)
                                      .AvecNom("Castor")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire(),

                new FabriqueDEtoile().Initialiser(false)
                                      .AvecNom("Pollux")
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
                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Mars")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Vénus")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Terre")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Mercure")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Jupiter")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Saturne")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Uranus")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Neptune")
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

                new FabriqueDePlanete().Initialiser(false)
                                       .AvecNom("Pluton")
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

            lesPlanetes.Sort();
            lesPlanetes.Reverse();
            lesEtoiles.Sort();
            lesEtoiles.Reverse();

            foreach (Planete planete in lesPlanetes)
            {
                Console.WriteLine(planete);
            }

            foreach (Etoile etoile in lesEtoiles)
            {
                Console.WriteLine(etoile);
            }

            List<Etoile> testEgalite = new List<Etoile>()
            {
                new Etoile("Sirius", "Description 1", 20, 20, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Faux", "Description 1", 20, 20, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 2", 20, 20, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 0, 20, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 20, 0, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 20, 20, 0, true, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, false, TypeEtoile.NaineNoire, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, true, TypeEtoile.TrouNoir, "Grande Ours", 1000),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, true, TypeEtoile.NaineNoire, "Cassiopée", 1000),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, true, TypeEtoile.NaineNoire, "Grande Ours", 50),
            };

            for(int i = 0; i < testEgalite.Count(); i++)
            {
                //Console.WriteLine($"e{0} == e{i} ? {testEgalite[0] == testEgalite[i]}");
                Console.WriteLine($"e{0} equals e{i} ? {testEgalite[0].Equals(testEgalite[i])}");
            }

            FabriqueDAstre<FabriqueDEtoile, Etoile> fabrique = new FabriqueDEtoile();
            Console.WriteLine(fabrique.Initialiser(false).AvecNom("test").AvecMasse(1.3f).EstDansLaConstellation("hého").Construire());
            Console.WriteLine(fabrique.Initialiser(true).AvecNom("test2").AvecAge(42).Construire());
        }
    }
}
