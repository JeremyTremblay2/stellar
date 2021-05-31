using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using Utilitaire;

namespace Espace
{
    /// <summary>
    /// Une étoile est une spécification d'un astre, il s'agit d'un objet plus précis qui se situe éventuellemnt dans une constellation, 
    /// possède un type et une luminosité.
    /// </summary>
    [DataContract]
    public class Etoile : Astre, IEquatable<Etoile>, IDataErrorInfo
    {

        /// <summary>
        /// Propriété représentant le type de l'étoile, contenu dans l'énumération TypeEtoile.
        /// </summary>
        [DataMember]
        public TypeEtoile Type { get; set; }

        /// <summary>
        /// Propriété représentant la constellation sous forme de châine de caractères, dans laquelle se trouve l'étoile.
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "La constellation doit être renseignée.")]
        [MaxLength(20, ErrorMessage = "Constellation trop longue.")]
        public string Constellation { get; set; }

        /// <summary>
        /// Propriété représentant la luminosité de l'étoile, sous forme d'une valeur flottante (en luminosité solaire Lo).
        /// </summary>
        [DataMember]
        [Range(0, float.MaxValue, ErrorMessage = "La luminositée doit être positive.")]
        public float Luminosite { get; set; }

        /// <summary>
        /// Permet de vérifier la cohérence des données lors d'entrées utilisateur avec les décorateurs placées sur les propriétées 
        /// précédentes. Peut éventuellement retourner des messages d'erreurs.
        /// </summary>
        /// <param name="columnName">Une colonne associée en réalité à la propriété dont on veut tester la cohérence des données.</param>
        /// <returns>Un message d'erreur sous forme d'une chaîne de caractères ou null si tout est correct.</returns>
        public new string this[string columnName]
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
        public new string Error { get; }

        /// <summary>
        /// Constructeur vide, utilisé par les fabriques d'étoiles.
        /// </summary>
        public Etoile() { }

        /// <summary>
        /// Constructeur d'étoiles. Il appele le constructeur de sa classe mère, Astre, afin qu'il soit initialisé.
        /// </summary>
        /// <param name="nom">Le nom de l'étoile.</param>
        /// <param name="description">Une courte description de l'étoile.</param>
        /// <param name="age">L'âge de l'étoile.</param>
        /// <param name="masse">La masse de l'étoile (en masse solaire).</param>
        /// <param name="temperature">La température de l'étoile (en Kelvin).</param>
        /// <param name="constellation">Le nom de la constellation auquelle appartient l'étoile.</param>
        /// <param name="luminosite">La luminosité de l'étoile (en luminosité solaire).</param>
        /// <param name="type">Le type de l'étoile (par défaut c'est une naine blanche).</param>
        /// <param name="personnalise">Un booléen indiquant si l'étoile est personnalisée (créée par l'utilisateur) ou non.</param>
        /// <param name="image">Une image de la planète sous forme de chemin en chaîne de caractères.</param>
        public Etoile(string nom, string description, long age, float masse, int temperature, string constellation, float luminosite, 
            TypeEtoile type = TypeEtoile.NaineBlanche, bool personnalise = false, string image = "étoile.jpg")
            : base(nom, description, age, masse, temperature, personnalise, image)
        {
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
