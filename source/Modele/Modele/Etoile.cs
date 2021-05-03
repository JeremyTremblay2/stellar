using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Etoile : Astre
    {
        public Etoile(string nom, ulong age = 0, byte masse = 0, int temperature = 1000, bool favori = false, bool personnalise = false, Point positionAstre = null, TypeEtoile type = TypeEtoile.NaineBlanche, string constellation = "Inconnue", int luminosite = 1000)
            : base(nom, age, masse, temperature, favori, personnalise, positionAstre)
        {
            Type = type;
            Constellation = constellation;
            Luminosite = luminosite;
        }

        public TypeEtoile Type { get; private set; }
        public string Constellation { get; private set; }
        public int Luminosite { get; private set; }

        public override string ToString()
        {
            string chaine = base.ToString();
            chaine += $"\tType d'étoile : {Type}\n";
            chaine += $"\tConstellation : {Constellation}\n";
            chaine += $"\tLuminosité : {Luminosite} L\n";
            return chaine;
        }
    }
}
