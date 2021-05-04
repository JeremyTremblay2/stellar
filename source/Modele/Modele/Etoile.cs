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
        /// <summary>
        /// Étoile est un astre pouvant être reliée à d'étoiles par des segments. Étoile peut être dans une constellation et possède une luminosité
        /// </summary>
        public Etoile(string nom, ulong age = 0, byte masse = 0, int temperature = 1000, bool favori = false, bool personnalise = false, Point positionAstre = null, TypeEtoile type = TypeEtoile.NaineBlanche, string constellation = "Inconnue", int luminosite = 1000)
            : base(nom, age, masse, temperature, favori, personnalise, positionAstre)
        {
            Type = type;
            Constellation = constellation;
            Luminosite = luminosite;
        }
        /// <summary>
        /// Constructeur de Etoile
        /// </summary>
        /// Variables pour les informations sur l'étoile
        /// <param name="constellation">Nom de la constellation dans laquelle se trouve l'étoile</param>
        /// <param name="luminosite">Luminosité de l'étoile</param>
        /// <param name="nom">Nom de l'étoile</param>
        /// <param name="age">Age de l'étoile</param>
        /// <param name="masse">Masse de l'étoile</param>
        /// <param name="temperature">Temperature de l'étoile</param>
        /// Variables filtres
        /// <param name="favoris">Etoile dans les favoris de l'utilisateur</param>
        /// <param name="personnalise">Etoile personnalisée</param>
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
