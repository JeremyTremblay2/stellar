using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Espace;
using Geometrie;
using Modele;
namespace Test
{
    /// <summary>
    /// Test de la classe Carte
    /// </summary>
    public static class Test_Carte
    {
        static Carte uneCarte = new Carte(true);
        /// <summary>
        /// Initialisation : ajout des étoiles dans uneCarte
        /// </summary>
        public static void Init()
        {
            uneCarte.AjouterUnAstre(new Point(42,42), 
                 new FabriqueDEtoile().Initialiser("Soleil")
                                      .AvecDescription("Le Soleil est l’étoile du Système solaire. Dans la classification astronomique, " +
                                      "c’est une étoile de type naine jaune d'une masse d'environ 1,989 1 × 1030 kg, composée d’hydrogène et d’hélium.")
                                      .AvecAge(460000000)
                                      .AvecMasse(1f)
                                      .AvecTemperature(5773)
                                      .AvecLuminosite(1)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      .Construire());
            uneCarte.AjouterUnAstre(new Point(56, 56), 
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
                                      .Construire());
            uneCarte.AjouterUnAstre(new Point(20, 98), 
                 new FabriqueDEtoile().Initialiser("Bételgeuse")
                                      .AvecDescription("Bételgeuse (α Orionis) est une étoile variable semi-régulière de type supergéante " +
                                      "rouge, dans la constellation d’Orion, située à une distance très difficile à établir.")
                                      .AvecAge(8000000)
                                      .AvecMasse(15)
                                      .AvecTemperature(3600)
                                      .AvecLuminosite(17)
                                      .EstDansLaConstellation("Orion")
                                      .AvecType(TypeEtoile.SupergeanteRouge)
                                      .Construire());
            uneCarte.AjouterUnAstre(new Point(32, 58), 
                 new FabriqueDEtoile().Initialiser("Castor")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire());


            uneCarte.AjouterUnAstre(new Point(75, 56),
                 new FabriqueDEtoile().Initialiser("etoile")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire());
            uneCarte.AjouterUnAstre(new Point(45, 98),
                 new FabriqueDEtoile().Initialiser("fdsfesdf")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire());
            uneCarte.AjouterUnAstre(new Point(58, 12),
                 new FabriqueDEtoile().Initialiser("esfesfes")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire());
            uneCarte.AjouterUnAstre(new Point(78, 64),
                 new FabriqueDEtoile().Initialiser("htfhtf")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire());
        }
        /// <summary>
        /// Test de la méthode DeplacerUnAstre
        /// </summary>
        public static void TestDeplacerUnAstre()
        {
            uneCarte.DeplacerUnAstre(new Point(42, 42), new Point(45, 67));
            Console.WriteLine($"DeplacerUnAstre : {uneCarte.ToString()}");
        }
        /// <summary>
        /// Test de la méthode RelierDeuxEtoiles
        /// </summary>
        public static void TestRelierDeuxEtoiles()
        {
            uneCarte.RelierDeuxEtoiles(new Point(42, 42), new Point(56, 56));
            Console.WriteLine($"RelierDeuxEtoile (E, E) : {uneCarte.ToString()}");
            uneCarte.RelierDeuxEtoiles(new Point(42, 42), new Point(20, 98));
            Console.WriteLine($"RelierDeuxEtoile (C, E) : {uneCarte.ToString()}");
            uneCarte.RelierDeuxEtoiles(new Point(20, 98), new Point(75, 56));
            uneCarte.RelierDeuxEtoiles(new Point(56, 56), new Point(45, 98));
            uneCarte.RelierDeuxEtoiles(new Point(75, 56), new Point(58, 12));
            uneCarte.RelierDeuxEtoiles(new Point(45, 98), new Point(78, 64));
            Console.WriteLine($"RelierDeuxEtoile : {uneCarte.ToString()}");
            //uneCarte.RelierDeuxEtoiles(new Point(42, 42), new Point(20, 98));
            //Console.WriteLine($"RelierDeuxEtoile (!) : {uneCarte.ToString()}");


        }
        public static void TestSupprimer()
        {
            uneCarte.SupprimerUnAstre(new Point(42, 42));
            Console.WriteLine($"SupprimerAstre : {uneCarte.ToString()}");
        }
    }
}
