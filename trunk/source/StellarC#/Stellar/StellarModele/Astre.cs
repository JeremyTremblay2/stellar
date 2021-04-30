using System;

namespace StellarModele
{
    /// <summary>
    /// Un Astre est un objet abstrait, il s'agit d'un élément quelconque de l'Univers.
    /// </summary>
    public abstract class Astre
    {
        private string nom;
        private int age;
        private int masse;
        private int temperature;
        private bool favoris;
        private bool personnalise;
        private Point p;

        public Astre(string nom, int age = 0, int masse = 0, int temperature = 1000, bool favoris = false, bool personnalise = false)
        {
            Nom = nom;
            Age = age;
            Masse = masse;
            Temperature = temperature;
            this.favoris = favoris;
            Personnalise = personnalise;
        }

        public string Nom { get; private set; }
        public int Age { get; private set; }
        public int Masse { get; private set; }
        public int Temperature { get; private set; }

        public bool Favoris { get; }
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
