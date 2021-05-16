using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Utilitaire;

namespace Espace
{
    /// <summary>
    /// Une étoile est une spécification d'un astre, il s'agit d'un objet plus précis qui se situe éventuellemnt dans une constellation, 
    /// possède un type et une luminosité.
    /// </summary>
    public class Etoile : Astre, IEquatable<Etoile>
    {
        /// <summary>
        /// Propriété représentant le type de l'étoile, contenu dans l'énumération TypeEtoile.
        /// </summary>
        public TypeEtoile Type { get; internal set; }

        /// <summary>
        /// Propriété représentant la constellation sous forme de châine de caractères, dans laquelle se trouve l'étoile.
        /// </summary>
        public string Constellation { get; internal set; }

        /// <summary>
        /// Propriété représentant la luminosité de l'étoile, sous forme d'une valeur flottante (en luminosité solaire Lo).
        /// </summary>
        public float Luminosite { get; internal set; }

        /// <summary>
        /// Constructeur vide, utilisé par les fabriques d'étoiles.
        /// </summary>
        public Etoile() { }

        /// <summary>
        /// Constructeur d'étoiles. Il appele le constructeur de sa classe mère, Astre, afin qu'il soit initialisé.
        /// </summary>
        /// <param name="nom">Le nom de l'étoile</param>
        /// <param name="description">Une courte description de l'étoile</param>
        /// <param name="age">L'âge de l'étoile</param>
        /// <param name="masse">La masse de l'étoile (en masse solaire)</param>
        /// <param name="temperature">La température de l'étoile (en Kelvin)</param>
        /// <param name="personnalise">Un booléen indiquant si l'étoile est personnalisée (créée par l'utilisateur) ou non</param>
        /// <param name="type">Le type de l'étoile</param>
        /// <param name="constellation">Le nom de la constellation auquelle appartient l'étoile</param>
        /// <param name="luminosite">La luminosité de l'étoile (en luminosité solaire)</param>
        public Etoile(string nom, string description, long age, float masse, int temperature, string constellation, float luminosite, TypeEtoile type = TypeEtoile.NaineBlanche, bool personnalise = false)
            : base(nom, description, age, masse, temperature, personnalise)
        {
            if (luminosite < 0)
            {
                throw new ArgumentException("La luminosité d'un astre ne peut pas être négative.");
            }

            Type = type;
            Constellation = constellation;
            Luminosite = luminosite;
        }

        /// <summary>
        /// Méthode redéfinie depuis la classe-mère, et permettant d'afficher le type de cet Astre (une étoile donc).
        /// </summary>
        public override void SePresenter()
        {
            Debug.WriteLine($"Je suis une {nameof(Etoile)}");
        }

        /// <summary>
        /// Permet d'afficher une étoile sous forme de différents champs de données. On affiche les données de l'astre, puis les champs
        /// spécifiques à l'étoile (constellation, type, luminosité).
        /// </summary>
        /// <returns>Retourne une chaîne de caractères représentant l'étoile.</returns>
        public override string ToString()
        {
            string chaine = base.ToString();
            //On appelle notre extension qui va venir afficher l'énumération sous forme d'une chaîne de caractères.
            chaine += $"\tType d'étoile : {Type.RecupererValeurEnum()}\n";
            chaine += $"\tConstellation : {Constellation}\n";
            chaine += $"\tLuminosité : {Luminosite} Lo\n";
            return chaine;
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si une étoile passée en paramètre est égal à this, donc si elle possède les mêmes 
        /// champs que la classe mère, ainsi que le même type, la même constellation et la même luminosité.
        /// </summary>
        /// <param name="autre">Une étoile que l'on souhaite comparer à this.</param>
        /// <returns>Un booléen qui indique si l'étoile passée en paramètre est la même que this ou non.</returns>
        public bool Equals([AllowNull] Etoile autre)
        {
            return base.Equals(autre)
                && Constellation.Equals(autre.Constellation)
                && Type == autre.Type
                && Luminosite == autre.Luminosite;
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est une Etoile.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si cette étoile est égale à this, donc si elle possède 
        /// les mêmes champs de la classe mère, et même type, même constellation et même luminosité.
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'une étoile (et s'il s'agit de la même étoile que this)</param>
        /// <returns>Un booléen qui indique si l'objet en paramètre est la même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Etoile);
        }

        /// <summary>
        /// Méthode permettant la redéfinition du Hashcode de Etoile. Ce Hashcode est défini en fonction du hashcode de la classe 
        /// mère d'étoile (Astre), mais aussi du type de cette étoile, de sa constellation et de sa luminsoité.
        /// </summary>
        /// <returns>Un entier représentant le hashcode de la planète.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() + Type.GetHashCode() + Constellation.GetHashCode() + Luminosite.GetHashCode();
        }

    }
}
