using System;
using System.Diagnostics.CodeAnalysis;

namespace Modele
{
    public class Planete : Astre, IEquatable<Planete>
    {
        public Planete() { }
        /// <summary>
        /// Constructeur dde planètes.
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
        public Planete(string nom, string description, long age = 0, float masse = 0, int temperature = 100, bool personnalise = false, TypePlanete type = TypePlanete.Naine, string vie = "Inconnu", bool eauPresente = false, string systeme = "Inconnu")
            : base(nom, description, age, masse, temperature, personnalise)
        {
            Type = type;
            Vie = vie;
            EauPresente = eauPresente;
            Systeme = systeme;
        }

        public TypePlanete Type { get; set; }

        public string Vie { get; set; }

        public bool EauPresente { get; set; }

        public string Systeme { get; set; }

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

        public bool Equals([AllowNull] Planete autre)
        {
            return base.Equals(autre)
                && Vie.Equals(autre.Vie)
                && Type == autre.Type
                && Systeme.Equals(autre.Systeme);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Planete);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + Type.GetHashCode() + Vie.GetHashCode() + Systeme.GetHashCode();
        }
    }
}
