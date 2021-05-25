using Espace;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilitaire;

namespace Tests_Unitaires
{
    /// <summary>
    /// Classe de test unitaires portant sur les Astres (Etoiles + Planetes). Divers tests de comparaison, d'égalité ont lieux.
    /// </summary>
    [TestClass]
    public class Test_Astre_Unit
    {
        /// <summary>
        /// Méthode de vérification des attributs des étoiles créées.
        /// </summary>
        [TestMethod]
        public void TestCreationEtoiles()
        {
            // Création de quelques étoiles
            Etoile sirius = new Etoile("Sirius",
                                        "Sirius, également appelée Alpha Canis Majoris (α Canis Majoris/α CMa) par la " +
                                        "désignation de Bayer, est l'étoile principale de la constellation du Grand Chien. Vue de la " +
                                        "Terre, Sirius est l'étoile la plus brillante du ciel après le Soleil.",
                                        250000000,
                                        1.03f,
                                        9900,
                                        "Grand Chien",
                                        26.1f);

            Etoile pollux = new FabriqueDEtoile().Initialiser("Pollux", true)
                                                .AvecDescription("Pollux (β Gem / Beta Geminorum) est l'étoile la plus brillante de la " +
                                                "constellation des Gémeaux et l'une des plus brillantes du ciel nocturne.")
                                                .AvecAge(7400000000)
                                                .AvecMasse(1.86f)
                                                .AvecTemperature(4865)
                                                .AvecLuminosite(32)
                                                .EstDansLaConstellation("Gémeaux")
                                                .AvecType(TypeEtoile.SupergeanteRouge)
                                                .Construire();

            //Divers tests sur Sirius et ses données
            sirius.Should().NotBeNull();
            sirius.Should().BeOfType(typeof(Etoile));
            sirius.Personnalise.Should().BeFalse();

            sirius.Nom.Should().NotBeEmpty();
            //Vérification du type étoile apr défaut, qui doit être "NaineBlanche".
            sirius.Type.Should().Be(TypeEtoile.NaineBlanche);
            sirius.Constellation.Should().BeEquivalentTo("Grand Chien");
            sirius.Temperature.Should().Be(9900);
            sirius.Luminosite.Should().Be(26.1f);

            //Vérification de la méthode permettant de changer les favoris.
            sirius.Favori.Should().BeFalse();
            sirius.ModifierFavori();
            sirius.Favori.Should().BeTrue();

            sirius.Should().BeSameAs(sirius);


            //Divers tests sur pollux, permet de vérifier que la fabrique d'étoiles fonctionne.
            pollux.Personnalise.Should().BeTrue();

            pollux.Nom.Should().NotBeNull();
            pollux.Description.Should().Contain("ciel nocturne");
            pollux.Age.Should().BeGreaterThan(7000000000);
            pollux.Age.Should().BeLessThan(8000000000);
            pollux.Masse.Should().BePositive();
            pollux.Temperature.Should().Be(4865);
            pollux.Luminosite.Should().BeGreaterOrEqualTo(30);
            pollux.Constellation.Should().Contain("meaux");
            pollux.Type.Should().Be(TypeEtoile.SupergeanteRouge);

            //Vérification de la méthode permettant de changer les favoris.
            pollux.Favori.Should().BeFalse();
            pollux.ModifierFavori();
            pollux.Favori.Should().BeTrue();
            pollux.ModifierFavori();
            pollux.Favori.Should().BeFalse();

            pollux.Should().NotBeSameAs(sirius);

        }

        /// <summary>
        /// Méthode de vérification des attributs des planètes créées.
        /// </summary>
        [TestMethod]
        public void TestCreationPlanetes()
        {
            // Création de quelques planètes
            Planete terre = new Planete("Terre",
                                        "La Terre est la troisième planète par ordre d'éloignement au Soleil et la cinquième " +
                                        "plus grande du Système solaire aussi bien par la masse que le diamètre. Par ailleurs, elle est le seul " +
                                        "objet céleste connu pour abriter la vie.",
                                        4500000000,
                                        1,
                                        288,
                                        "Oui",
                                        true,
                                        "Solaire",
                                        TypePlanete.Tellurique,
                                        true);

            Planete mercure = new FabriqueDePlanete().Initialiser("Mercure")
                                                       .AvecDescription("Mercure est la planète la plus proche du Soleil et la moins massive du Système " +
                                                       "solaire. Son éloignement au Soleil est compris entre 0,31 et 0,47 unité astronomique.")
                                                       .AvecAge(4000000000)
                                                       .AvecMasse(0.055f)
                                                       .AvecTemperature(440)
                                                       .PresenceDeVie("Non")
                                                       .EstDansLeSysteme("Solaire")
                                                       .EauEstPresente(false)
                                                       .Construire();

            //Divers tests sur la Terre et ses données
            terre.Should().NotBeNull();
            terre.Should().BeOfType<Planete>();
            terre.Personnalise.Should().BeTrue();

            terre.Nom.Should().Be("Terre");
            terre.Description.Should().NotBeNull();
            terre.Type.Should().Be(TypePlanete.Tellurique);
            terre.Age.Should().Be(4500000000);
            terre.Masse.Should().BeGreaterThan(0.99f);
            terre.Systeme.Should().Contain("Solai");
            terre.Temperature.Should().BeGreaterThan(250);
            terre.Temperature.Should().BeLessThan(290);
            terre.EauPresente.Should().BeTrue();
            terre.Vie.Should().NotBeNull();

            //Vérification de la méthode permettant de changer les favoris.
            terre.Favori.Should().BeFalse();
            terre.ModifierFavori();
            terre.Favori.Should().BeTrue();

            terre.Should().BeSameAs(terre);

            //Divers tests sur Mercure et ses données, permet de vérifier que la fabrique de planète fonctionne
            mercure.Personnalise.Should().BeFalse();

            mercure.Nom.Should().Be("Mercure");
            mercure.Description.Length.Should().BeGreaterThan(160);
            mercure.Age.Should().BeInRange(3900000000, 4100000000);
            mercure.Masse.Should().Be(0.055f);
            mercure.Vie.ToLower().Should().Contain("non");
            mercure.Systeme.Should().NotBeNull();
            mercure.Temperature.Should().BeGreaterThan(400);
            mercure.EauPresente.Should().BeFalse();
            mercure.Type.Should().Be(TypeEtoile.NaineBlanche);

            //Vérification de la méthode permettant de changer les favoris.
            mercure.Favori.Should().BeFalse();
            mercure.ModifierFavori();
            mercure.Favori.Should().BeTrue();

            mercure.Should().NotBeSameAs(terre);

            //Vérification des méthodes de conversion.
            float temperatureCelsius = ConvertisseurTemperature.ToCelsius(mercure.Temperature);
            temperatureCelsius.Should().BeApproximately(166.85f, 2);
            float temperatureKelvin = ConvertisseurTemperature.ToKelvin(temperatureCelsius);
            temperatureKelvin.Should().Be(mercure.Temperature);
        }

        /// <summary>
        /// Méthode de vérification des protocoles d'égalité des étoiles.
        /// </summary>
        [TestMethod]
        public void TestEgaliteEtoiles()
        {
            //Création d'une liste d'étoiles ne comportant que des étoiles différentes.
            var lesEtoiles = new List<Etoile>()
            {
                new Etoile("Sirius", "Description 1", 20, 20, 5000, "Constellation", 1,TypeEtoile.NaineNoire, true),
                new Etoile("Pollux", "Description 1", 20, 20, 5000, "Constellation", 1,TypeEtoile.NaineNoire, true),
                new Etoile("Sirius", "Description 1", 500, 20, 5000, "Constellation", 1,TypeEtoile.NaineNoire, true),
                new Etoile("Sirius", "Description 1", 20, 500, 5000, "Constellation", 1,TypeEtoile.NaineNoire, true),
                new Etoile("Sirius", "Description 1", 20, 20, 50000, "Constellation", 1,TypeEtoile.NaineNoire, true),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, "Test", 1,TypeEtoile.NaineNoire, true),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, "Constellation", 56,TypeEtoile.NaineNoire, true),
                new Etoile("Sirius", "Description 1", 20, 20, 5000, "Constellation", 1,TypeEtoile.TrouNoir, true),
            };

            //Début de quelques vérifications.
            lesEtoiles[0].GetHashCode().Should().Be(lesEtoiles[0].GetHashCode());
            lesEtoiles[0].GetHashCode().Should().NotBe(lesEtoiles[1].GetHashCode());

            lesEtoiles[0].Equals(lesEtoiles[0]).Should().BeTrue();
            lesEtoiles[0].Equals(lesEtoiles[1]).Should().BeFalse();


            //Parcours de la liste et vérification des protocoles d'égalités.
            for (int i = 0; i < lesEtoiles.Count(); i++)
            {
                for(int j = 0; j < lesEtoiles.Count(); j++)
                {
                    //Les étoiles ici sont normalement différentes (et leurs hashcodes respectifs).
                    if (i != j)
                    {
                        lesEtoiles[i].Equals(lesEtoiles[j]).Should().BeFalse();
                        lesEtoiles[i].GetHashCode().Should().NotBe(lesEtoiles[j].GetHashCode());
                    }
                    //Les étoiles ici doivent être les mêmes (et leurs hashcodes respectifs).
                    else
                    {
                        lesEtoiles[i].Equals(lesEtoiles[j]).Should().BeTrue();
                        lesEtoiles[i].GetHashCode().Should().Be(lesEtoiles[j].GetHashCode());
                    }
                }
            }

            //Les paramètres qui n'influent pas l'égalité de deux étoiles : description, favori, personnalisé.
            var etoile1 = new Etoile("Sirius", "Description 1", 20, 20, 5000, "Constellation", 1, TypeEtoile.TrouNoir, true);
            var etoile2 = new Etoile("Sirius", "blabla", 20, 20, 5000, "Constellation", 1, TypeEtoile.TrouNoir, false);

            etoile1.Equals(etoile2).Should().BeTrue();
            etoile1.GetHashCode().Should().Be(etoile2.GetHashCode());

            //Le favori n'influe pas dans l'égalité de deux étoiles.
            etoile1.ModifierFavori();

            etoile2.Equals(etoile1).Should().BeTrue();
            etoile1.GetHashCode().Should().Be(etoile2.GetHashCode());
        }

        /// <summary>
        /// Méthode de vérification des protocoles d'égalité des planètes.
        /// </summary>
        [TestMethod]
        public void TestEgalitePlanetes()
        {
            //Création d'une liste de planètes ne comportant que des planètes différentes.
            var lesPlanetes = new List<Planete>()
            {
                new Planete("Mars", "Description 1", 20, 20, 5000, "Aucune vie", true, "Systeme solaire", TypePlanete.Gazeuse, true),
                new Planete("Mercure", "Description 1", 20, 20, 5000, "Aucune vie", true, "Systeme solaire", TypePlanete.Gazeuse, true),
                new Planete("Mars", "Description 1", 50, 20, 5000, "Aucune vie", true, "Systeme solaire", TypePlanete.Gazeuse, true),
                new Planete("Mars", "Description 1", 20, 50, 5000, "Aucune vie", true, "Systeme solaire", TypePlanete.Gazeuse, true),
                new Planete("Mars", "Description 1", 20, 20, 50000, "Aucune vie", true, "Systeme solaire", TypePlanete.Gazeuse, true),
                new Planete("Mars", "Description 1", 20, 20, 5000, "De la vie", true, "Systeme solaire", TypePlanete.Gazeuse, true),
                new Planete("Mars", "Description 1", 20, 20, 5000, "Aucune vie", true, "Andromede", TypePlanete.Gazeuse, true),
                new Planete("Mars", "Description 1", 20, 20, 5000, "Aucune vie", true, "Systeme solaire", TypePlanete.Naine, true),
            };

            //Début de quelques vérifications.
            lesPlanetes[0].GetHashCode().Should().Be(lesPlanetes[0].GetHashCode());
            lesPlanetes[0].GetHashCode().Should().NotBe(lesPlanetes[1].GetHashCode());

            lesPlanetes[0].Equals(lesPlanetes[0]).Should().BeTrue();
            lesPlanetes[0].Equals(lesPlanetes[1]).Should().BeFalse();


            //Parcours de la liste et vérification des protocoles d'égalités.
            for (int i = 0; i < lesPlanetes.Count(); i++)
            {
                for (int j = 0; j < lesPlanetes.Count(); j++)
                {
                    //Les planètes ici sont normalement différentes (et leurs hashcodes respectifs).
                    if (i != j)
                    {
                        lesPlanetes[i].Equals(lesPlanetes[j]).Should().BeFalse();
                        lesPlanetes[i].GetHashCode().Should().NotBe(lesPlanetes[j].GetHashCode());
                    }
                    //Les planètes ici doivent être les mêmes (et leurs hashcodes respectifs).
                    else
                    {
                        lesPlanetes[i].Equals(lesPlanetes[j]).Should().BeTrue();
                        lesPlanetes[i].GetHashCode().Should().Be(lesPlanetes[j].GetHashCode());
                    }
                }
            }

            //Les paramètres qui n'influent pas l'égalité de deux planètes : description, favori, personnalisé, eau présente.
            var planete1 = new Planete("Mars", "Description 1", 20, 20, 5000, "Aucune vie", true, "Systeme solaire", TypePlanete.Gazeuse, true);
            var planete2 = new Planete("Mars", "blablabla", 20, 20, 5000, "Aucune vie", false, "Systeme solaire", TypePlanete.Gazeuse, false);

            planete1.Equals(planete2).Should().BeTrue();
            planete1.GetHashCode().Should().Be(planete2.GetHashCode());

            //Le favori n'influe pas dans l'égalité de deux planètes.
            planete1.ModifierFavori();

            planete2.Equals(planete1).Should().BeTrue();
            planete1.GetHashCode().Should().Be(planete2.GetHashCode());
        }
    }
}
