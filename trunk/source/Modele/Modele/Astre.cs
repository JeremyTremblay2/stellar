using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Un Astre est un objet abstrait, il s'agit d'un élément quelconque de l'Univers.
    /// </summary>
    public abstract class Astre
    {
        public Astre()
        {
        }

        public Astre(string nom, ulong age = 0, byte masse = 0, int temperature = 1000, bool favori = false, bool personnalise = false, Point positionAstre = null)
        {
            Nom = nom;
            Age = age;
            Masse = masse;
            Temperature = temperature;
            Favori = favori;
            Personnalise = personnalise;
            PositionAstre = positionAstre;
        }

        public string Nom { get; private set; }
        public ulong Age { get; private set; }
        public byte Masse { get; private set; }
        public int Temperature { get; private set; }
        public bool Favori { get; private set; }
        public bool Personnalise { get; private set; }
        public Point PositionAstre { get; set; }

        public Astre AvecNom(string nom)
        {
            Nom = nom;
            return this;
        }
        public Astre AvecAge(ulong age)
        {
            Age = age;
            return this;
        }
        public void ModifierFavori()
        {
            if (Favori)
                Favori = false;
            else
                Favori = true;
        }

        public override string ToString()
        {
            string chaine = $"Caractéristiques de {Nom} :\n";
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
            if (PositionAstre != null)
            {
                chaine += $"\tJe suis placé aux coordonnées {PositionAstre}\n";
            }
            chaine += $"\tAge : {Age} a\n";
            chaine += $"\tMasse : {Masse} M\n";
            chaine += $"\tTemperature : {Temperature} K\n";
            
            return chaine;
        }
    }
}
