﻿namespace Modele
{
    /// <summary>
    /// Une étoile est un astre. Une étoile peut être dans une constellation, possède un type et une luminosité.
    /// </summary>
    public class Etoile : Astre
    {
        /// <summary>
        /// Constructeur vide, utilisé par les fabriques d'étoiles.
        /// </summary>
        public Etoile() { }

        /// <summary>
        /// Constructeur d'étoiles.
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
        public Etoile(string nom, string description, long age = 0, float masse = 0.0f, int temperature = 1000, bool personnalise = false, TypeEtoile type = TypeEtoile.NaineBlanche, string constellation = "Inconnue", float luminosite = 1000)
            : base(nom, description, age, masse, temperature, personnalise)
        {
            Type = type;
            Constellation = constellation;
            Luminosite = luminosite;
        }
        
        public TypeEtoile Type { get; set; }

        public string Constellation { get; set; }

        public float Luminosite { get; set; }

        public override string ToString()
        {
            string chaine = base.ToString();
            chaine += $"\tType d'étoile : {Type}\n";
            chaine += $"\tConstellation : {Constellation}\n";
            chaine += $"\tLuminosité : {Luminosite} Lo\n";
            return chaine;
        }
    }
}
