using System;

namespace StellarModele
{
    /// <summary>
    /// Un Astre est un objet abstrait, il s'agit d'un élément quelconque de l'Univers.
    /// </summary>
    public class Astre
    {
        private string nom;
        private int age;
        private int masse;
        private int temperature;
        private bool favoris;
        private bool personnalise;

        public Astre(string nom, int age, int masse, int temperature, bool favoris, bool personnalise)
        {
            Nom = nom;
            Age = age;
            Masse = masse;
            Temperature = temperature;
            Favoris = favoris;
            Personnalise = personnalise;
        }

        public string Nom { get; private set; }
        public int Age { get; private set; }
        public int Masse { get; private set; }
        public int Temperature { get; private set; }
        public bool Favoris { get; private set; }
        public bool Personnalise { get; private set; }

        public override string ToString()
        {
            string chaine = $"Caractéristiques :\n\tNom : {Nom}\n";
            if (age >= 0)
            {
                chaine += $"\tAge : {Age} a\n";
            }
            if (masse >= 0)
            {
                chaine += $"\tMasse : {Masse} M\n";
            }
            if (temperature >= 0)
            {
                chaine += $"\tTemperature : {Temperature} K\n";
            }
            if (favoris)
            {
                chaine += "Je suis dans les favoris.\n";
            }
            else
            {
                chaine += "Je ne suis pas dans les favoris.\n";
            }
            if (personnalise)
            {
                chaine += "Je suis un astre personnalisé.\n";
            }
            else
            {
                chaine += "Je ne suis pas un astre personnalisé.\n";
            }
            return chaine;
        }

    }
}
