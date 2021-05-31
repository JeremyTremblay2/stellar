using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public interface IPersistanceCarte
    {
        (Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations) ChargeDonneesCarte(string cheminFichier);

        void SauvegardeDonneesCarte(Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations, string cheminFichier);
    }
}
