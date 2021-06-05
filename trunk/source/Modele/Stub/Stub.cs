using System;
using System.Collections.Generic;
using System.Diagnostics;
using Espace;
using Geometrie;
using Modele;

namespace Stub
{
    /// <summary>
    /// Le stub est un mode de sérialisation fictif qui implémente nos interfaces de chargement / sauvegarde.
    /// </summary>
    public class Stub : IPersistanceManager, IPersistanceCarte
    {
        /// <summary>
        /// Méthode de chargement des données, implémentée depuis l'inetrface du Manager.
        /// Instancie une liste d'astres avec les données en dur, puis la retourne.
        /// </summary>
        /// <returns>Une liste d'astres.</returns>
        public IEnumerable<Astre> ChargeDonnees()
        {
            var fabriqueDetoile = new FabriqueDEtoile();
            var fabriqueDePlanete = new FabriqueDePlanete();

            List<Astre> astres = new List<Astre>()
            {

                fabriqueDetoile.Initialiser("Sirius")
                                .AvecDescription("Sirius, également appelée Alpha Canis Majoris (α Canis Majoris/α CMa) par la " +
                                "désignation de Bayer, est l'étoile principale de la constellation du Grand Chien. Vue de la " +
                                "Terre, Sirius est l'étoile la plus brillante du ciel après le Soleil.")
                                .AvecAge(250000000)
                                .AvecMasse(1.03f)
                                .AvecTemperature(9900)
                                .AvecLuminosite(26.1f)
                                .EstDansLaConstellation("Grand Chien")
                                .AvecType(TypeEtoile.NaineBlanche)
                                .AvecImage("sirius.jpeg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Soleil")
                                .AvecDescription("Le Soleil est l’étoile du Système solaire. Dans la classification astronomique, " +
                                "c’est une étoile de type naine jaune d'une masse d'environ 1,989 1 × 1030 kg, composée d’hydrogène et d’hélium.")
                                .AvecAge(460000000)
                                .AvecMasse(1f)
                                .AvecTemperature(5773)
                                .EstDansLaConstellation("Aucune")
                                .AvecLuminosite(1)
                                .AvecType(TypeEtoile.NaineJaune)
                                .AvecImage("soleil.jpg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Bételgeuse")
                                .AvecDescription("Bételgeuse (α Orionis) est une étoile variable semi-régulière de type supergéante " +
                                "rouge, dans la constellation d’Orion, située à une distance très difficile à établir.")
                                .AvecAge(8000000)
                                .AvecMasse(15)
                                .AvecTemperature(3600)
                                .AvecLuminosite(17)
                                .EstDansLaConstellation("Orion")
                                .AvecType(TypeEtoile.SupergeanteRouge)
                                .AvecImage("bételgeuse.jpg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Castor")
                                .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                .AvecAge(370000000)
                                .AvecMasse(1.7f)
                                .AvecTemperature(8840)
                                .AvecLuminosite(14)
                                .EstDansLaConstellation("Gémeaux")
                                .AvecImage("castor.jpg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Pollux")
                                .AvecDescription("Pollux (β Gem / Beta Geminorum) est l'étoile la plus brillante de la " +
                                "constellation des Gémeaux et l'une des plus brillantes du ciel nocturne.")
                                .AvecAge(7400000000)
                                .AvecMasse(1.86f)
                                .AvecTemperature(4865)
                                .AvecLuminosite(32)
                                .EstDansLaConstellation("Gémeaux")
                                .AvecType(TypeEtoile.GeanteRouge)
                                .AvecImage("pollux.jpg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Polaris")
                                .AvecDescription("Polaris ou Alpha Ursae Minoris est une étoile multiple. Elle est l’étoile la plus brillante de la constellation de la Petite Ourse. " +
                                "Elle est aussi connue sous le nom de l’Étoile polaire car elle correspond à la direction du pôle nord céleste")
                                .AvecAge(70000000)
                                .AvecMasse(4.5f)
                                .AvecTemperature(7000)
                                .AvecLuminosite(2)
                                .EstDansLaConstellation("Petite Ourse")
                                .AvecType(TypeEtoile.SupergeanteRouge)
                                .AvecImage("pollux.jpg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Spica")
                                .AvecDescription("Spica ou α Virginis. C'est l'étoile la plus lumineuse de la constellation de la Vierge. " +
                                "On peut remarquer un scintillement lorsqu'on l'observe car Spica est une étoile binaire spectroscopique et une étoile variable ellipsoïdale. ")
                                .AvecAge(12500000)
                                .AvecMasse(11.43f)
                                .AvecTemperature(25300)
                                .AvecLuminosite(20512)
                                .EstDansLaConstellation("Vierge")
                                .AvecType(TypeEtoile.GeanteBleu)
                                .AvecImage("pollux.jpg")
                                .Construire(),

                fabriqueDetoile.Initialiser("Antarès")
                                .AvecDescription("Antarès ou α Scorpii est une étoile binaire en fin de vie.")
                                .AvecAge(11000000)
                                .AvecMasse(18f)
                                .AvecTemperature(3570)
                                .AvecLuminosite(97700)
                                .EstDansLaConstellation("Scorpion")
                                .AvecType(TypeEtoile.SupergeanteRouge)
                                .AvecImage("Antarès.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Mars")
                                .AvecDescription("Mars est la quatrième planète par ordre croissant de la distance au Soleil " +
                                "et la deuxième par ordre croissant de la taille et de la masse.")
                                .AvecAge(4500000000)
                                .AvecMasse(0.107f)
                                .AvecTemperature(210)
                                .PresenceDeVie("En cours de recherches")
                                .EstDansLeSysteme("Solaire")
                                .EauEstPresente(false)
                                .AvecType(TypePlanete.Tellurique)
                                .AvecImage("mars.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Vénus")
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
                                .AvecImage("vénus.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Terre")
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
                                .AvecImage("terre.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Mercure")
                                .AvecDescription("Mercure est la planète la plus proche du Soleil et la moins massive du Système " +
                                "solaire. Son éloignement au Soleil est compris entre 0,31 et 0,47 unité astronomique.")
                                .AvecAge(4000000000)
                                .AvecMasse(0.055f)
                                .AvecTemperature(440)
                                .PresenceDeVie("Non")
                                .EstDansLeSysteme("Solaire")
                                .EauEstPresente(false)
                                .AvecType(TypePlanete.Tellurique)
                                .AvecImage("mercure.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Jupiter")
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
                                .AvecImage("jupiter.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Saturne")
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
                                .AvecImage("saturne.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Uranus")
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
                                .AvecImage("uranus.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Neptune")
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
                                .AvecImage("neptune.jpg")
                                .Construire(),

                fabriqueDePlanete.Initialiser("Pluton")
                                .AvecDescription("Pluton, officiellement désigné par (134340) Pluton, est une planète naine, la plus volumineuse " +
                                "connue dans le Système solaire, et la deuxième en ce qui concerne sa masse. Elle ne fait plus partie du système solaire.")                               .AvecAge(4500000000)
                                .AvecMasse(0.002f)
                                .AvecTemperature(48)
                                .PresenceDeVie("Non")
                                .EstDansLeSysteme("Solaire externe")
                                .EauEstPresente(false)
                                .AvecType(TypePlanete.Naine)
                                .AvecImage("pluton.jpg")
                                .Construire(),
            };

            return astres;
        }

        /// <summary>
        /// Méthode de sauvegarde ne faisant rien.
        /// Simule un enregistrement des données.
        /// </summary>
        /// <param name="astres">Une liste d'astres à enregistrer.</param>
        public void SauvegardeDonnees(IEnumerable<Astre> astres)
        {
            Debug.WriteLine("Sauvegarde demandée.");
        }

        /// <summary>
        /// Méthode de chargement des données de la Carte.
        /// Simule un chargement des données, ne fait rien en réalité.
        /// </summary>
        /// <param name="cheminFichier">Le chemin du fichier dans lequel les données seront chargées.</param>
        /// <returns>Deux colelctions de données vides.</returns>
        public (Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations) ChargeDonneesCarte(string cheminFichier)
        {
            return (new Dictionary<Point, Astre>(), new List<Constellation>());
        }

        /// <summary>
        /// Méthode de sauvegarde des données fictive, qui ne fait rien en réalité.
        /// </summary>
        /// <param name="astres">Un dictionnaire d'astres à sérialiser.</param>
        /// <param name="constellations">Une collection de constellations à enregistrer.</param>
        /// <param name="cheminFichier">Le chemin du fichier dans lequel enregistrer les données.</param>
        public void SauvegardeDonneesCarte(Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations, string cheminFichier)
        {
            Debug.WriteLine("Sauvegarde de la carte demandée.");
        }
    }
}
