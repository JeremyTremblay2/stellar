using System;
using System.Globalization;

namespace Modele
{
    /// <summary>
    /// Un Astre est un objet abstrait, il s'agit d'un élément quelconque de l'Univers.
    /// </summary>
    public abstract class Astre
    {
        private const float constanteConversionTemperature = 273.15f;
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
        /// <param name="age">L'age de l'astre</param>
        /// <param name="masse">La masse de l'astre (en masse terrestre ou solaire)</param>
        /// <param name="temperature">La température de l'astre (en kelvin)</param>
        /// <param name="personnalise">Un booléen indiquant si l'astre est personnalisé (créé par l'utilisateur) ou non</param>
        /// 
        public Astre(string nom, string description, long age = 0, float masse = 1f, int temperature = 1000, bool personnalise = false)
        {
            Nom = nom;
            Description = description;
            Age = age;
            Masse = masse;
            Temperature = temperature;
            Personnalise = personnalise;
        }
        
        public string Nom
        {
            get => nom;
            set
            {
                nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            }
        }
        
        public long Age { get; set; }

        public string Description { get; set; }

        public float Masse { get; set; }

        public int Temperature { get; set; }

        public bool Favori { get; private set; }

        public bool Personnalise { get; set;  }

        public float GetTemperatureCelsius => (float) Math.Round(Temperature - constanteConversionTemperature, 2);

        /// <summary>
        /// Méthode permettant la modification de l'état de l'attribut favori. On change son état actuel.
        /// </summary>
        public void ModifierFavori()
        {
            if (Favori)
                Favori = false;
            else
                Favori = true;
        }

        public override string ToString()
        {
            string chaine = $"Description de {Nom} :\n\t{Description}\n";
            chaine += "Caractéristiques : \n";
            if (Favori)
            {
                chaine += "\tJe suis dans les favoris.\n";
            }
            else
            {
                chaine += "\tJe ne suis pas dans les favoris.\n";
            }
            if (Personnalise)
            {
                chaine += "\tJe suis un astre personnalisé.\n";
            }
            else
            {
                chaine += "\tJe ne suis pas un astre personnalisé.\n";
            }
            chaine += $"\tAge : {Age} a\n";
            if (this is Etoile)
            {
                chaine += $"\tMasse : {Masse} MS\n";
            }
            else
            {
                chaine += $"\tMasse : {Masse} MT\n";
            }
                
            chaine += $"\tTemperature : {Temperature} K ({GetTemperatureCelsius}° C)\n";

            return chaine;
        }
    }
}
