using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
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

        public IPersistanceManager Persistance { get; set; }

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

        /// <summary>
        /// Evènement permettant la notification du changement d'une propriété.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Propriété concernant l'astre sélectionné dans l'application, et notifie la vue de son changement en cas de modifications.
        /// </summary>
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
        /// On instancie notre liste d'astre et notre Carte. On vient instancie nos méthodes de persistance pour la Carte et le Manager.
        /// <param name="persistance">Le type de persistance utilisé par le Manager.</param>
        /// <param name="persistanceCarte">LE type de persistance utilisé par la Carte.</param>
        /// </summary>
        public Manager(IPersistanceManager persistance, IPersistanceCarte persistanceCarte)
        {
            Persistance = persistance;
            lesAstres = new ObservableCollection<Astre>();
            InitCollections();
            Carte = new Carte(persistanceCarte);
        }

        /// <summary>
        /// Permet l'instanciation des collections.
        /// </summary>
        /// <param name="sc"></param>
        [OnDeserialized]
        void InitCollections(StreamingContext sc = new StreamingContext())
        {
            LesAstres = new ReadOnlyObservableCollection<Astre>(lesAstres);
            lesAstresTries = new ObservableCollection<Astre>(lesAstres);
            LesAstresTries = new ReadOnlyObservableCollection<Astre>(lesAstresTries);
        }

        /// <summary>
        /// Méthode appellant le chargement des données de la persistance instanciée.
        /// Elle vient ajouter les données chargées dans ses listes.
        /// </summary>
        public void ChargeDonnees()
        {
            var donnees = Persistance.ChargeDonnees();
            foreach(Astre astre in donnees)
            {
                lesAstres.Add(astre);
            }
            InitCollections();
        }

        /// <summary>
        /// Méthode appellant la sauvegarde des données de la persistance instanciée.
        /// On enregistre que les astres non personnalisés.
        /// </summary>
        public void SauvegardeDonnees()
        {
            var astres = new List<Astre>();
            astres = lesAstres.Where(astre => !astre.Personnalise).ToList();
            Persistance.SauvegardeDonnees(astres);
        }

        /// <summary>
        /// Méthode permettant le chargement des données contenues dans la carte.
        /// On appelle la méthode correspondante dans la carte, puis on vient ajouter les astres personnalisés dans nos listes qui ont pu 
        /// être incorporés suite au chargement de la carte.
        /// </summary>
        /// <param name="nomFichier">Le nom du fichier sous forme de chaîne de caractères, dans lequel on va lire les données de la carte.</param>
        public void ChargeDonneesCarte(string nomFichier)
        {
            Carte.ChargeDonnees(nomFichier);

            for (int i = 0; i < lesAstres.Count; i++)
            {
                if (lesAstres[i].Personnalise)
                {
                    lesAstres.Remove(lesAstres[i]);
                    i--;
                }
            }

            foreach (KeyValuePair<Point, Astre> kvp in Carte.LesAstres)
            {
                if (!lesAstres.Contains(kvp.Value))
                {
                    lesAstres.Add(kvp.Value);
                    lesAstresTries.Add(kvp.Value);
                }
            }
            InitCollections();
        }

        /// <summary>
        /// Méthode déléguant la sauvegarde de la Carte, à la Carte.
        /// </summary>
        /// <param name="nomFichier">Le nom du fichier qui sera utilisé pour sauvegarder des données de la carte.</param>
        public void SauvegardeDonneesCarte(string nomFichier)
            => Carte.SauvegardeDonnees(nomFichier);

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

        /// <summary>
        /// Méthode permettant de retourner l'astre correpondant au nom fournit au paramètre, ou null s'il n'existe pas.
        /// </summary>
        /// <param name="nom">Le nom de l'astre que l'on veut chercher.</param>
        /// <returns>L'astre correspondant ou null si aucun ne satisfait le nom passé en paramètre.</returns>
        public Astre RecupererAstre(string nom)
            => lesAstres.SingleOrDefault(astre => astre.Nom.Equals(nom));

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
    }
}
