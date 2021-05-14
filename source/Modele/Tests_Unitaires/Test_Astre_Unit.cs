using Espace;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests_Unitaires
{
    /// <summary>
    /// Classe de test unitaires portant sur les Astres (Etoiles + Planetes). Divers tests de comparaison, d'égalité ont lieux.
    /// </summary>
    [TestClass]
    public class Test_Astre_Unit
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

        /// <summary>
        /// Méthode de vérification des attributs des étoiles créées.
        /// </summary>
        [TestMethod]
        public void TestCreationEtoiles()
        {
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

            mercure.Favori.Should().BeFalse();
            mercure.ModifierFavori();
            mercure.Favori.Should().BeTrue();

            mercure.Should().NotBeSameAs(terre);

        }
    }
}
