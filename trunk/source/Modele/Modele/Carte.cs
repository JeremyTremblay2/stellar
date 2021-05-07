using System;
using System.Collections.Generic;
using System.Linq;

namespace Modele
{
    public class Carte
    {
        private Dictionary<Point, Astre> lesAstres;
        private List<Constellation> lesConstellations;

        public Carte(bool avecCreation)
        {
            lesAstres = new Dictionary<Point, Astre>();
            lesConstellations = new List<Constellation>();

            if (avecCreation)
            {
                //Ajouter des astres et constellations.
            }
        }
        
        public void ajouterUneEtoile(Point position)
        {
            lesAstres[position] = null;
        }

        public void supprimerUneConstellation(Point position)
        {
            
        }
    }
}
