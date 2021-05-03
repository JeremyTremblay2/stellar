using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Planete : Astre
    {
        public Planete(string nom, ulong age = 0, byte masse = 0, int temperature = 1000, bool favori = false, bool personnalise = false, Point positionAstre = null, TypePlanete type = TypePlanete.Naine, string vie = "Inconnu", bool eauPresente = false)
            : base(nom, age, masse, temperature, favori, personnalise, positionAstre)
        {
            Type = type;
            Vie = vie;
            EauPresente = eauPresente;
        }

        public TypePlanete Type { get; private set; }
        public string Vie { get; private set; }
        public bool EauPresente { get; private set; }

        public override string ToString()
        {
            string chaine = base.ToString();
            chaine += $"\tType de planète : {Type}\n";
            chaine += $"\tPrésence de vie : {Vie}\n";
            if (EauPresente)
                chaine += $"\tDe l'eau est visiblement présente sur cette planète.\n";
            else
                chaine += $"\tAucun signe d'eau découvert.\n";

            return chaine;
        }
    }
}
