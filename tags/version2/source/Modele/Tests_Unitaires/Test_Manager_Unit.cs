using Espace;
using FluentAssertions;
using Geometrie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using DataContractPersistance;

namespace Tests_Unitaires
{
    /// <summary>
    /// Classe de tests unitaires permettant de vérifier que le Manager fonctionne.
    /// </summary>
    [TestClass]
    public class Test_Manager_Unit
    {
        //Instanciation d'un manager.
        Manager manager = new Manager(new DataContractPers());

        /// <summary>
        /// Méthode permettant de tester si la création du manager fonctionne, en vérifiant que la liste d'astres est bien vide à 
        /// l'instanciation.
        /// </summary>
        [TestMethod]
        public void TestCreationManager()
        {
            manager.LesAstres.Should().BeEmpty();
        }

        /// <summary>
        /// Méthode de test permettant de vérifier que l'ajout d'astres à la liste d'astre fonctionne.
        /// </summary>
        [TestMethod]
        public void TestAjoutAstre()
        {
            manager.AjouterUnAstre(new FabriqueDePlanete().Initialiser("Vénus")
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
                                       .Construire());

            manager.LesAstres.Should().NotBeEmpty();

            manager.AjouterUnAstre(new FabriqueDEtoile()
                                      .Initialiser("Castor")
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .Construire());

            manager.LesAstres.Should().HaveCount(2);

            //Il ne devrait plus rien rester dans la liste.
            manager.SupprimerTout();
            manager.LesAstres.Should().HaveCount(0);
        }

        /// <summary>
        /// Méthode de vérification qui se base sur la carte et le manager cette fois-ci.
        /// On vérifie que l'on arrive à ajouter des astres dans le manager et sur la carte, à partir de points, et qu'on arrive à les 
        /// effacer.
        /// </summary>
        [TestMethod]
        public void TestAjoutEtSuppressionAstre()
        {
            manager.LesAstres.Should().BeEmpty();
            //manager.Carte.LesAstres.Should().BeEmpty();
            //manager.Carte.LesConstellations.Should().BeEmpty();

            manager.AjouterUnAstre(new Point(23, 12),
                                   new Etoile("Sirius",
                                              "Sirius, également appelée Alpha Canis Majoris (α Canis Majoris/α CMa) par la " +
                                              "désignation de Bayer, est l'étoile principale de la constellation du Grand Chien. Vue de la " +
                                              "Terre, Sirius est l'étoile la plus brillante du ciel après le Soleil.",
                                              250000000,
                                              1.03f,
                                              9900,
                                              "Grand Chien",
                                              26.1f));

            //Ajout d'un astre sur la carte et dans le manager.
            manager.Carte.LesAstres.Should().HaveCount(1);
            manager.Carte.LesConstellations.Should().BeEmpty();
            manager.Carte.LesAstres.Should().HaveCount(1);

            manager.AjouterUnAstre(new Point(21, 56), new FabriqueDePlanete().Initialiser("Mercure")
                                                       .AvecDescription("Mercure est la planète la plus proche du Soleil et la moins massive du Système " +
                                                       "solaire. Son éloignement au Soleil est compris entre 0,31 et 0,47 unité astronomique.")
                                                       .AvecAge(4000000000)
                                                       .AvecMasse(0.055f)
                                                       .AvecTemperature(440)
                                                       .PresenceDeVie("Non")
                                                       .EstDansLeSysteme("Solaire")
                                                       .EauEstPresente(false)
                                                       .Construire());

            //Ajout d'un astre sur la carte et dans le manager.
            manager.Carte.LesAstres.Should().HaveCount(2);
            manager.Carte.LesConstellations.Should().BeEmpty();
            manager.Carte.LesAstres.Should().HaveCount(2);

            //Suppression du premier astre "Sirius" à partir de sa position.
            manager.SupprimerUnAstre(new Point(23, 12));
            manager.Carte.LesAstres.Should().HaveCount(1);
            manager.LesAstres.Should().HaveCount(1);

            //On efface tout.
            manager.SupprimerTout();
            manager.Carte.LesAstres.Should().BeEmpty();
            manager.LesAstres.Should().BeEmpty();
        }
    }
}
