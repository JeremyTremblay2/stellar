using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Utilitaire;

namespace Espace
{
    /// <summary>
    /// Un Astre est un objet abstrait, il s'agit d'un élément quelconque de l'Univers.
    /// </summary>
    [DataContract, KnownType(typeof(Etoile)), KnownType(typeof(Planete))]
    public abstract class Astre : IEquatable<Astre>, IComparable<Astre>, IComparable, INotifyPropertyChanged, IDataErrorInfo
    {
        //Nom de l'astre.
        private string nom;

        //Permet de savoir si l'astre se trouve dans les favoris.
        private bool favori;

        //Sert à envoyer des notifications quand des données sont modifiées.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Propriété représentant le nom de l'astre sous forme d'une chaîne de caractères. Il ne peut pas être vide.
        /// Le nom ne peut être qu'écrit sous format titre (Xxxx Xx Xxxx).
        /// </summary>
        [DataMember (EmitDefaultValue = false, Order = 0)]
        [Required(ErrorMessage = "Le nom doit être renseigné")]
        [MaxLength(15, ErrorMessage = "Nom trop long.")]
        public string Nom
        {
            get => nom;
            set
            {
                nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            }
        }

        /// <summary>
        /// Propriété représentant l'âge de l'astre en valeur entière.
        /// </summary>
        [DataMember (EmitDefaultValue = false, Order = 1)]
        [Required(ErrorMessage = "L'âge doit être renseigné")]
        [Range(0, long.MaxValue, ErrorMessage = "L'âge doit être positif.")]
        public long Age { get; set; }

        /// <summary>
        /// Propriété représentant une description quelconque de l'astre sous forme d'une chaîne de caractère.
        /// </summary>
        [DataMember (EmitDefaultValue = false, Order = 2)]
        [MaxLength(200, ErrorMessage = "Description trop longue.")]
        public string Description { get; set; }

        /// <summary>
        /// Propriété représentant la masse de l'astre en valeur flottante (en masse terrestre s'il s'agit d'une planète, 
        /// ou en masse solaire s'il s'agit d'une étoile).
        /// </summary>
        [DataMember (EmitDefaultValue = false, Order = 3)]
        [Range(0, float.MaxValue, ErrorMessage = "La masse doit être positive.")]
        public float Masse { get; set; }

        /// <summary>
        /// Propriété représentant la température de l'astre en valeur entière (en Kelvin).
        /// </summary>
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public int Temperature { get; set; }

        /// <summary>
        /// Propriété pour savoir si l'astre est un favori de l'utilisateur ou non, représenté par un booléen.
        /// Une notification de changement est envoyé à la vue quand l'état du favori est modifié.
        /// </summary>
        [DataMember(EmitDefaultValue = false, Order = 6)]
        public bool Favori
        {
            get => favori;
            private set
            {
                if (favori == value) return;
                favori = value;
                OnPropertyChanged(nameof(Favori));
            }
        }

        /// <summary>
        /// Propriété permettant de savoir si l'astre est un astre personnalisé (= crée par l'utilisateur) ou non, représenté par un booléen.
        /// </summary>
        [DataMember(EmitDefaultValue = false, Order = 7)]
        public bool Personnalise { get; internal set; }

        /// <summary>
        /// Propriété permettant d'associer une image à l'astre (il s'agit d'une chaîne de caractères).
        /// </summary>
        [DataMember(EmitDefaultValue = false, Order = 5)]
        public string Image { get; set; }

        /// <summary>
        /// Permet de vérifier la cohérence des données lors d'entrées utilisateur avec les décorateurs placés sur les propriétées 
        /// précédentes. Peut éventuellement retourner des messages d'erreurs.
        /// </summary>
        /// <param name="columnName">Une colonne associée en réalité à la propriété dont on veut tester la cohérence des données.</param>
        /// <returns>Un message d'erreur sous forme d'une chaîne de caractères ou null si tout est correct.</returns>
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                    GetType().GetProperty(columnName).GetValue(this)
                    , new ValidationContext(this)
                    {
                        MemberName = columnName
                    }
                    , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        /// <summary>
        /// Propriété permettant de retourner l'erreur associée à l'invalidité des champs.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Constructeur vide, utilisé par les fabriques d'astres.
        /// </summary>
        public Astre() { }

        /// <summary>
        /// Constructeur d'astres.
        /// </summary>
        /// <param name="nom">Le nom de l'astre.</param>
        /// <param name="description">Une description courte de l'astre en question.</param>
        /// <param name="age">L'age de l'astre.</param>
        /// <param name="masse">La masse de l'astre (en masse terrestre ou solaire).</param>
        /// <param name="temperature">La température de l'astre (en kelvin).</param>
        /// <param name="personnalise">Un booléen indiquant si l'astre est personnalisé (créé par l'utilisateur) ou non.</param>
        /// <param name="image">Une image de l'astre sous forme de chemin en chaîne de caractères.</param>
        public Astre(string nom, string description, long age, float masse, int temperature, bool personnalise = false, string image = null)
        {
            Nom = nom;
            Description = description;
            Age = age;
            Masse = masse;
            Temperature = temperature;
            Personnalise = personnalise;
            Image = image;
        }

        //Méthode permettant de notifier la vue que des données ont été mises à jour.
        void OnPropertyChanged(string nomPropriete)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));

        /// <summary>
        /// Méthode permettant la modification de l'état de l'attribut favori. On change son état actuel.
        /// </summary>
        public void ModifierFavori()
        {
            Favori = !Favori;
        }

        /// <summary>
        /// Méthode abstraite ayant pour but de présenter l'astre, c'est à dire, d'afficher son type.
        /// </summary>
        public abstract void SePresenter();

        /// <summary>
        /// Permet d'afficher un Astre sous forme de différents champs de données, tels que son nom, sa masse, sa température,
        /// sa description, s'il est un astre personnalisé ou non, s'il est dans les favoris ou non...
        /// </summary>
        /// <returns>Retourne une chaîne de cracatère représentant l'Astre.</returns>
        public override string ToString()
        {
            StringBuilder chaine = new StringBuilder();

            chaine.AppendFormat("Description de {0} :\n\t{1}\n", Nom, Description);
            chaine.Append("Caractéristiques : \n");
            if (Favori)
            {
                chaine.Append("\tJe suis dans les favoris.\n");
            }
            else
            {
                chaine.Append("\tJe ne suis pas dans les favoris.\n");
            }
            if (Personnalise)
            {
                chaine.Append("\tJe suis un astre personnalisé.\n");
            }
            else
            {
                chaine.Append("\tJe ne suis pas un astre personnalisé.\n");
            }
            chaine.AppendFormat("\tAge : {0} a\n", Age);
            if (this is Etoile)
            {
                chaine.AppendFormat("\tMasse : {0} MS\n", Masse);
            }
            else
            {
                chaine.AppendFormat("\tMasse : {0} MT\n", Masse);
            }
                
            chaine.AppendFormat("\tTemperature : {0} K ({1}° C)\n", Temperature, ((float) Temperature).ToCelsius());

            return chaine.ToString();
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un astre passé en paramètre est égal à this, donc s'il possède le même nom, 
        /// même age, même masse, même température.
        /// </summary>
        /// <param name="autre">Un astre que l'on souhaite comparer à this.</param>
        /// <returns>Un booléen qui indique si l'astre passé en paramètre est le même que this ou non.</returns>
        public bool Equals([AllowNull] Astre autre)
        {
            return Nom.Equals(autre.Nom) 
                && Age == autre.Age 
                && Masse == autre.Masse 
                && Temperature == autre.Temperature;
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est un Astre.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si cet astre est égal à this, donc s'il possède le même 
        /// nom, même age, même masse, même température, en appellant la méthode Equals de cet astre et en le castant.
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'un astre (et s'il s'agit du même astre que this)</param>
        /// <returns>Un booléen qui indique si l'objet passé en paramètre est le même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Astre);
        }

        /// <summary>
        /// Permet la génération d'un HashCode, utilisé dans le cas des dictionnaires.
        /// Ce hascode est définit par le nom, l'âge, la température et la masse de l'astre.
        /// </summary>
        /// <returns>Un entier représentant le hashcode de cet astre.</returns>
        public override int GetHashCode()
        {
            return Nom.GetHashCode() + Age.GetHashCode() + Temperature.GetHashCode() + Masse.GetHashCode();
        }

        /// <summary>
        /// Permet la comparaison de deux objets. On vérifie que l'objet est un astre (sinon on lève une exception).
        /// La comparaison se base en fonction du nom de l'astre (ordre alphabétique). 
        /// </summary>
        /// <param name="obj">Un objet quelconque que l'on souhaite comparer à cet astre.</param>
        /// <returns>Un entier représentant la comparaison préalablement effectuée.</returns>
        public int CompareTo(object obj)
        {
            if(! (obj is Astre))
            {
                throw new ArgumentException("L'argument n'est pas un Astre", "obj");
            }
            Astre unAstre = obj as Astre;
            return this.CompareTo(unAstre);
        }

        /// <summary>
        /// Permet la comparaison de deux objets. La comparaison se base en fonction du nom de l'astre (ordre alphabétique). 
        /// </summary>
        /// <param name="autre">Un astre que l'on souhaite comparer avec this.</param>
        /// <returns>Un entier représentant la comparaison préalablement effectuée.</returns>
        public int CompareTo([AllowNull] Astre autre)
        {
            return Nom.CompareTo(autre.Nom);
        }
    }
}
