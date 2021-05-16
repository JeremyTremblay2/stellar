using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Espace
{
    /// <summary>
    /// Une planète est une spécification d'un astre, il s'agit d'un objet plus précis qui se situe dans un système stellaire, 
    /// possède un type, contient éventuellement de l'eau, et de la vie.
    /// </summary>
    public class Planete : Astre, IEquatable<Planete>
    {
        /// <summary>
        /// Propriété représentant le type de la planète, contenu dans l'énumération TypePlanete.
        /// </summary>
        public TypePlanete Type { get; internal set; }

        /// <summary>
        /// Propriété permettant de représenter la présence de vie ou non de la planète, sous forme de chaîne de caractères.
        /// </summary>
        public string Vie { get; internal set; }

        /// <summary>
        /// Propriété représentant la présence d'eau sur la planète ou non, sous forme de booléen. 
        /// </summary>
        public bool EauPresente { get; internal set; }

        /// <summary>
        /// Propriété permettant de représenter le système stellaire de la planète, sous forme de chaîne de caractères.
        /// </summary>
        public string Systeme { get; internal set; }

        /// <summary>
        /// Constructeur de planète. Il appele le constructeur de sa classe mère, Astre, afin qu'il soit initialisé.
        /// </summary>
        /// <param name="nom">Le nom de la planète</param>
        /// <param name="description">Une courte description de la planète</param>
        /// <param name="age">L'âge de la planète</param>
        /// <param name="masse">La masse de la planète (en masse terrestre)</param>
        /// <param name="temperature">La température de la planète (en Kelvin)</param>
        /// <param name="personnalise">Un booléen indiquant si la planète est personnalisée (créée par l'utilisateur) ou non</param>
        /// <param name="type">Le type de planète</param>
        /// <param name="vie">Une chaîne de caractère donnant des indications sur une éventuelle vie sur la planète en question</param>
        /// <param name="eauPresente">un booléen indiquant si l'eau est présente ou non</param>
        /// <param name="systeme">Un chaîne de caractères indiquant dans quel système stellaire se trouve la planète</param>
        public Planete(string nom, string description, long age, float masse, int temperature, string vie, bool eauPresente, string systeme, TypePlanete type = TypePlanete.Naine, bool personnalise = false)
            : base(nom, description, age, masse, temperature, personnalise)
        {
            Type = type;
            Vie = vie;
            EauPresente = eauPresente;
            Systeme = systeme;
        }

        /// <summary>
        /// Constructeur vide, utilisé par les fabriques de planètes.
        /// </summary>
        public Planete() { }

        /// <summary>
        /// Méthode redéfinie depuis la classe-mère, et permettant d'afficher le type de cet Astre (une planète donc).
        /// </summary>
        public override void SePresenter()
        {
            Debug.WriteLine($"Je suis une {nameof(Planete)}");
        }

        /// <summary>
        /// Permet d'afficher une Planète. Pour cela, on affiche les données générale de l'astre, 
        /// puis ensuite les données spécifiques propres à la planète (vie, eau, système stellaire, type).
        /// </summary>
        /// <returns>Retourne la chaîne de caratères représentant la planète.</returns>
        public override string ToString()
        {
            string chaine = base.ToString();
            chaine += $"\tType de planète : {Type}\n";
            chaine += $"\tPrésence de vie : {Vie}\n";
            chaine += $"\tAppartient au système : {Systeme}\n";
            if (EauPresente)
                chaine += $"\tDe l'eau est visiblement présente sur cette planète.\n";
            else
                chaine += $"\tAucun signe d'eau découvert.\n";

            return chaine;
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si une planète passée en paramètre est égal à this, donc, si elle possède les mêmes 
        /// champs que la classe mère, ainsi que le même type, le même système et la même présence de vie.
        /// </summary>
        /// <param name="autre">Une planète que l'on souhaite comparer à this.</param>
        /// <returns>Un booléen qui indique si la planète passée en paramètre est la même que this ou non.</returns>
        public bool Equals([AllowNull] Planete autre)
        {
            return base.Equals(autre)
                && Vie.Equals(autre.Vie)
                && Type == autre.Type
                && Systeme.Equals(autre.Systeme);
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est une Planete.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si cette planète est égale à this, donc si elle possède 
        /// les mêmes champs de la classe mère, et même type, même présence de vie, même système stellaire.
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'une planète (et s'il s'agit de la même planète que this)</param>
        /// <returns>Un booléen qui indique si l'objet en paramètre est la même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Planete);
        }

        /// <summary>
        /// Méthode permettant la redéfinition du Hashcode de Planete. Ce Hashcode est défini en fonction du hashcode de la classe 
        /// mère de planète (Astre), mais aussi du type de planète, de la présence de vie, et du système stellaire.
        /// </summary>
        /// <returns>Un entier représentant le hashcode de la planète.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() + Type.GetHashCode() + Vie.GetHashCode() + Systeme.GetHashCode();
        }
    }
}
