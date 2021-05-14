using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Utilitaire;

namespace Espace
{
    /// <summary>
    /// Un Astre est un objet abstrait, il s'agit d'un élément quelconque de l'Univers.
    /// </summary>
    public abstract class Astre : IEquatable<Astre>, IComparable<Astre>, IComparable
    {
        private string nom;
        
        /// <summary>
        /// Constructeur vide, utilisé par les fabriques d'astres.
        /// </summary>
        public Astre() { }

        /// <summary>
        /// Constructeur d'astres.
        /// </summary>
        /// <param name="nom">Le nom de l'astre</param>
        /// <param name="description">Une description courte de l'astre en question</param>
        /// <param name="age">L'age de l'astre.</param>
        /// <param name="masse">La masse de l'astre (en masse terrestre ou solaire)</param>
        /// <param name="temperature">La température de l'astre (en kelvin)</param>
        /// <param name="personnalise">Un booléen indiquant si l'astre est personnalisé (créé par l'utilisateur) ou non</param>
        public Astre(string nom, string description, long age, float masse, int temperature, bool personnalise = false)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentNullException("Le nom d'un Astre ne peut pas être vide ou null.");
            }
            Nom = nom;
            Description = description;
            Age = age;
            Masse = masse;
            Temperature = temperature;
            Personnalise = personnalise;
        }
        
        /// <summary>
        /// Propriété représentant le nom de l'astre sous forme d'une chaîne de caractères.
        /// Le nom ne peut être qu'écrit sous format titre (Xxxx Xx Xxxx).
        /// </summary>
        public string Nom
        {
            get => nom;
            internal set
            {
                nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            }
        }
        
        /// <summary>
        /// Propriété représentant l'âge de l'astre en valeur entière.
        /// </summary>
        public long Age { get; internal set; }

        /// <summary>
        /// Propriété représentant une description quelconque de l'astre sous forme d'une chaîne de caractère.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Propriété représentant la masse de l'astre en valeur flottante (en masse terrestre s'il s'agit d'une planète, 
        /// ou en masse solaire s'il s'agit d'une étoile).
        /// </summary>
        public float Masse { get; internal set; }

        /// <summary>
        /// Propriété représentant la température de l'astre en valeur entière (en Kelvin).
        /// </summary>
        public int Temperature { get; internal set; }

        /// <summary>
        /// Propriété pour savoir si l'astre est un favori de l'utilisateur ou non, représenté par un booléen.
        /// </summary>
        public bool Favori { get; private set; }

        /// <summary>
        /// Propriété permettant de savoir si l'astre est un astre personnalisé (= crée par l'utilisateur) ou non, représente par un booléen.
        /// </summary>
        public bool Personnalise { get; internal set;  }

        /// <summary>
        /// Méthode permettant la modification de l'état de l'attribut favori. On change son état actuel.
        /// </summary>
        public void ModifierFavori()
        {
            Favori = Favori ? false : true;
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
                
            chaine.AppendFormat("\tTemperature : {0} K ({1}° C)\n", Temperature, ConvertisseurTemperature.ToCelsius(Temperature));

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
