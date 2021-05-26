using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Utilitaire;

namespace Modele
{
    /// <summary>
    /// Le Manager est le coeur de l'application, c'est lui qui vient déléguer et gérer les diverses actions de l'application.
    /// Il se compose d'une Carte et d'une liste d'astres.
    /// </summary>
    public class Manager : INotifyPropertyChanged
    {
        //Données contenues dans le Manager.
        //La liste d'astres correspond au menu déroulant avec les informations des astres.
        //La carte correspond à l'ensemble des points et constellations qui se trouvent sur la partie éditeur.
        private ObservableCollection<Astre> lesAstres;

        //Cette collection observable est la liste d'astres triés, elle ne contient pas forcément tous les astres de l'autre liste.
        private ObservableCollection<Astre> lesAstresTries;

        //L'astre sélectionné dans l'application.
        private Astre astreSelectionne;

        /// <summary>
        /// Propriété en lecture seule concernant la liste d'astres, qui est l'ensembles de toutes les données (les astres) 
        /// de l'application.
        /// </summary>
        public ReadOnlyObservableCollection<Astre> LesAstres { get; private set; }

        /// <summary>
        /// Propriété en lecture seule concernant la liste triée d'astre qui se trouve dans l'application.
        /// </summary>
        public ReadOnlyObservableCollection<Astre> LesAstresTries { get; private set; }

        /// <summary>
        /// Propriété concernant la Carte, qui est l'endroit sur lequel se trouve tous les points (les astres), et 
        /// constellations de l'application.
        /// </summary>
        public Carte Carte { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Astre AstreSelectionne
        {
            get => astreSelectionne;
            set
            {
                if (astreSelectionne != value)
                {
                    astreSelectionne = value;
                    OnPropertyChanged(nameof(AstreSelectionne));
                }
            }
        }

        /// <summary>
        /// Méthode permettant de notifier l'application d'un changement de propriété dans les données, afin qu'elle puisse se
        /// mettre à jour.
        /// </summary>
        /// <param name="nomPropriete">Le nom de la propriété changée sous forme de chaîne de caractères.</param>
        void OnPropertyChanged(string nomPropriete)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));

        /// <summary>
        /// Constructeur de Manager. Il ne prend pas de paramètre.
        /// On instancie notre liste d'astre et notre Carte.
        /// </summary>
        public Manager()
        {
            lesAstres = new ObservableCollection<Astre>();
            LesAstres = new ReadOnlyObservableCollection<Astre>(lesAstres);
            lesAstresTries = new ObservableCollection<Astre>(lesAstres);
            LesAstresTries = new ReadOnlyObservableCollection<Astre>(lesAstresTries);
            Carte = new Carte();
        }

        /// <summary>
        /// Méthode permettant l'ajout d'un astre à la carte et à la liste d'astres.
        /// Pour cela, on vérifie que le point et l'astre fournit en paramètre ne sont pas null.
        /// Si c'est le cas, on ajoute l'astre à la liste et on appelle une méthode dans notre Carte qui va venir l'ajouter à la position fournie.
        /// </summary>
        /// <param name="position">Le Point de la position de l'astre sur la carte.</param>
        /// <param name="astre">L'astre à ajouter à l'application.</param>
        public void AjouterUnAstre(Point position, Astre astre)
        {
            Carte.AjouterUnAstre(position, astre);
            if (!lesAstres.Contains(astre))
            {
                lesAstres.Add(astre);
                lesAstresTries.Add(astre);
            }
        }

        /// <summary>
        /// Méthode permettant l'ajout d'un astre uniquement dans la liste d'astres (pas sur la carte).
        /// Pour cela, on vérifie que l'astre fournit en paramètre n'est pas null (sinon on lève une exception), et on l'ajoute à la liste,
        /// s'il n'est pas déjà existant.
        /// </summary>
        /// <param name="astre">L'astre à ajouter à la liste.</param>
        public void AjouterUnAstre(Astre astre)
        {
            if (astre == null)
            {
                throw new ArgumentException($"L'astre ne peut pas être crée car l'astre passé en paramètre est null. " +
                $"Astre : {astre}");
            }

            if (!lesAstres.Contains(astre))
            {
                lesAstres.Add(astre);
                lesAstresTries.Add(astre);
            }
        }

        /// <summary>
        /// Permet la suppression d'un astre sur la Carte. Nécéssite donc un point de coordonnées.
        /// La suppression du-dit astre se déroulera dans la classe Carte, cependant, l'astre éventuellement supprimé est retourné.
        /// On vérifie donc qu'il n'est pas null (si la suppression n'a pas eu lieu, si c'était par exemple un point dans une constellation),
        /// et qu'il s'agit d'un astre personnalisé (les astres pré-existants dans le logiciel ne peuvent jamais être effacés), et on peut
        /// alors le supprimer.
        /// </summary>
        /// <param name="position">Le Point correspondant à la position de l'astre a supprimer.</param>
        public void SupprimerUnAstre(Point position)
        {
            Astre astreASupprimer = Carte.SupprimerUnAstre(position);
            if (astreASupprimer != null && astreASupprimer.Personnalise)
            {
                lesAstres.Remove(astreASupprimer);
                lesAstresTries.Remove(astreASupprimer);
            }
        }

        /// <summary>
        /// Méthode permettant de vider la manager de ses données, et donc d'effacer la liste d'astres personnalisés et la Carte de ses données.
        /// Elle appelle la méthode contenue dans la Carte, qui va venir effacer son dictionnaire d'astres et sa liste de constellations.
        /// </summary>
        public void SupprimerTout()
        {
            for (int i = 0; i < lesAstres.Count; i++)
            {
                if (lesAstres[i].Personnalise)
                {
                    lesAstres.Remove(lesAstres[i]);
                    i--;
                }
            }

            for (int i = 0; i < lesAstresTries.Count; i++)
            {
                if (lesAstresTries[i].Personnalise)
                {
                    lesAstresTries.Remove(lesAstresTries[i]);
                    i--;
                }
            }

            Carte.SupprimerTout();
            OnPropertyChanged(nameof(LesAstresTries));
        }

        /// <summary>
        /// Méthode permettant de déplacer un astre qui se trouve sur la Carte, d'une position à une autre.
        /// Le manager ne fait que déléguer le travail à la Carte.
        /// </summary>
        /// <param name="anciennePosition">Un Point représentant l'ancienne position de l'astre.</param>
        /// <param name="nouvellePosition">Un Point représentant la nouvelle position de l'astre.</param>
        public void DeplacerUnAstre(Point anciennePosition, Point nouvellePosition)
            => Carte.DeplacerUnAstre(anciennePosition, nouvellePosition);

        /// <summary>
        /// Méthode permettant de relier deux étoiles qui se trouvent sur la Carte.
        /// Le manager ne fait que déléguer le travail à la Carte.
        /// </summary>
        /// <param name="point1">Le point de la première étoile à relier.</param>
        /// <param name="point2">Le point de la seconde étoile à relier.</param>
        public void RelierDeuxEtoiles(Point point1, Point point2)
            => Carte.RelierDeuxEtoiles(point1, point2);

        /// <summary>
        /// Permet de trier la liste d'astres en appellant des méthodes de tris, et donc de mettre à jour la liste triée.
        /// </summary>
        /// <param name="favori">Un booléen indiquant si l'on veut afficher uniquement les favoris ou non.</param>
        /// <param name="personnalise">Un entier indiquant si l'on veut uniquement les astres personnalisés ou non.</param>
        /// <param name="type">Un type permettant de filtrer les astres par celui-ci.</param>
        /// <param name="alphabetique">Un booléen indiquant l'ordre alphabétique.</param>
        /// <param name="nom">Une chaîne de caractères à rechercher dans le nom des astres.</param>
        public void Filtrage(bool favori, byte personnalise, Type type, bool alphabetique, string nom)
        {
            var listeTriee = lesAstres.ToList();

            if (!string.IsNullOrWhiteSpace(nom))
                listeTriee = listeTriee.RechercheParNom(nom);

            if (favori)
                listeTriee = listeTriee.RechercheParFavoris(favori);

            if (!(type == typeof(Astre)))
                listeTriee = listeTriee.RechercheParType(type);

            listeTriee = listeTriee.RechercheParPersonnalisation(personnalise);
            listeTriee = listeTriee.TriParOrdreAlphabetique(alphabetique);

            lesAstresTries = new ObservableCollection<Astre>(listeTriee);
            LesAstresTries = new ReadOnlyObservableCollection<Astre>(lesAstresTries);

            OnPropertyChanged(nameof(LesAstresTries));
        }

        public Astre RecupererAstre(string nom)
        {
            return lesAstres.SingleOrDefault(astre => astre.Nom.Equals(nom));
        }

        /// <summary>
        /// Méthode permettant l'affichage du Manager.
        /// Elle retourne une chaîne de caractères représentant le manager et les données qu'il contient, pour cela, elle affiche le nom 
        /// de chaque astre contenu dans la liste.
        /// </summary>
        /// <returns>Une chaîne de caractères qui permet de visualiser l'ensemble des astres du Manager.</returns>
        public override string ToString()
        {
            StringBuilder chaine = new StringBuilder();

            if (lesAstres.Count == 0)
            {
                chaine.Append("\tAucun astre dans l'application.\n");
            }
            else
            {
                chaine.AppendFormat("{0} Astre(s) au total :\n", lesAstres.Count);
            }

            foreach (Astre astre in lesAstres)
            {
                chaine.AppendFormat("\t{0}\n", astre.Nom);
            }

            chaine.AppendLine("Voici la carte :");
            chaine.AppendFormat("{0}", Carte);

            return chaine.ToString();
        }

        public void ChargeDonnees()
        {
            AjouterUnAstre(new Point(10, 10), new FabriqueDEtoile().Initialiser("Sirius")
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
                                      .Construire());

            AjouterUnAstre(new Point(90, 120), new FabriqueDEtoile().Initialiser("Soleil")
                                      .AvecDescription("Le Soleil est l’étoile du Système solaire. Dans la classification astronomique, " +
                                      "c’est une étoile de type naine jaune d'une masse d'environ 1,989 1 × 1030 kg, composée d’hydrogène et d’hélium.")
                                      .AvecAge(460000000)
                                      .AvecMasse(1f)
                                      .AvecTemperature(5773)
                                      .EstDansLaConstellation("Aucune")
                                      .AvecLuminosite(1)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      .AvecImage("soleil.jpg")
                                      .Construire()); ;

            AjouterUnAstre(new Point(130, 104), new FabriqueDEtoile().Initialiser("Bételgeuse")
                                      .AvecDescription("Bételgeuse (α Orionis) est une étoile variable semi-régulière de type supergéante " +
                                      "rouge, dans la constellation d’Orion, située à une distance très difficile à établir.")
                                      .AvecAge(8000000)
                                      .AvecMasse(15)
                                      .AvecTemperature(3600)
                                      .AvecLuminosite(17)
                                      .EstDansLaConstellation("Orion")
                                      .AvecType(TypeEtoile.SupergeanteRouge)
                                      .AvecImage("bételgeuse.jpg")
                                      .Construire());

            AjouterUnAstre(new Point(180, 89), new FabriqueDEtoile().Initialiser("Castor", true)
                                      .AvecDescription("Castor (ou Alpha Geminorum) est la seconde étoile la plus brillante de la " +
                                      "constellation des Gémeaux et une des plus brillantes étoiles du ciel nocturne.")
                                      .AvecAge(370000000)
                                      .AvecMasse(1.7f)
                                      .AvecTemperature(8840)
                                      .AvecLuminosite(14)
                                      .EstDansLaConstellation("Gémeaux")
                                      .AvecImage("castor.jpg")
                                      .Construire());

            AjouterUnAstre(new Point(250, 89), new FabriqueDEtoile().Initialiser("Pollux", true)
                                      .AvecDescription("Pollux (β Gem / Beta Geminorum) est l'étoile la plus brillante de la " +
                                      "constellation des Gémeaux et l'une des plus brillantes du ciel nocturne.")
                                      .AvecAge(7400000000)
                                      .AvecMasse(1.86f)
                                      .AvecTemperature(4865)
                                      .AvecLuminosite(32)
                                      .EstDansLaConstellation("Gémeaux")
                                      .AvecImage("pollux.jpg")
                                      .Construire());

            AjouterUnAstre(new Point(400, 400), new FabriqueDePlanete().Initialiser("Mars", true)
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
                                       .Construire());

            AjouterUnAstre(new FabriqueDePlanete().Initialiser("Vénus")
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
                                       .Construire());

            AjouterUnAstre(new FabriqueDePlanete().Initialiser("Terre")
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
                                       .Construire());

            AjouterUnAstre(new Point(120, 235), new FabriqueDePlanete().Initialiser("Mercure")
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
                                       .Construire());

            AjouterUnAstre(new FabriqueDePlanete().Initialiser("Jupiter")
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
                                       .Construire());

            AjouterUnAstre(new Point(500, 235), new FabriqueDePlanete().Initialiser("Saturne")
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
                                       .Construire());

            AjouterUnAstre(new FabriqueDePlanete().Initialiser("Uranus")
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
                                       .Construire());

            AjouterUnAstre(new Point(120, 450), new FabriqueDePlanete().Initialiser("Neptune")
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
                                       .Construire());

            AjouterUnAstre(new Point(630, 120), new FabriqueDePlanete().Initialiser("Pluton", true)
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
                                       .AvecImage("pluton.jpg")
                                       .Construire());

            for(int i = 0; i < lesAstres.Count; i++)
            {
                if (i%2 == 0)
                    lesAstres[i].ModifierFavori();
            }

            Carte.RelierDeuxEtoiles(new Point(130, 104), new Point(180, 89));
            Carte.RelierDeuxEtoiles(new Point(130, 104), new Point(250, 89));
            Carte.RelierDeuxEtoiles(new Point(180, 89), new Point(250, 89));
            Carte.RelierDeuxEtoiles(new Point(90, 120), new Point(10, 10));

        }
    }
}
