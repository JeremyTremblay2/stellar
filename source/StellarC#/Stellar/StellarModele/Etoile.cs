using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarModele
{
    /// <summary>
    /// Étoile est un astre pouvant être reliée à d'étoiles par des segments. Étoile peut être dans une constellation et possède une luminosité
    /// </summary>
    public class Etoile : Astre
    {
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
        public Etoile(string constellation, int luminosite, string nom, int age = 0, int masse = 0, int temperature = 1000, bool favoris = false, bool personnalise = false) 
            : base(nom, age, masse, temperature, favoris, personnalise)
        {
            this.constellation = constellation;
            this.luminosite = luminosite;
        }

        public string constellation { get; private set; }
        public int luminosite { get; private set; }

        public override string ToString()
        {
            string chaine = base.ToString() + $"Constellation : {constellation}\n";
            if (constellation != null)
            {
                chaine += $"Constellation : {constellation}\n";
            }
            else if(luminosite > 0)
            {
                chaine += $"luminosité : {luminosite}\n";
            }
                
            return chaine;
        }
    }
}
