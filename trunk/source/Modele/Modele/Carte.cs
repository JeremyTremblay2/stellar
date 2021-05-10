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
                       
        public void ajouterUnAstre(Point position, Astre astre)
        {
            if (position != null && astre != null)
                lesAstres[position] = astre;
        }

        public Astre supprimerUnAstre(Point position)
        {
            if (lesAstres.ContainsKey(position))
            {
                Astre astreARetourner = lesAstres[position];
                if (astreARetourner is Etoile)
                {
                    //traitement de l'éventuelle constellation.
                }

                lesAstres.Remove(position);
                return astreARetourner;
            }
            return null;
        }

        public void SupprimerTout()
        {
            lesAstres.Clear();
            lesConstellations.Clear();
        }

        public override string ToString()
        {
            string chaine = "Voici les astres sur la carte :\n";

            foreach(KeyValuePair<Point, Astre> kvp in lesAstres)
            {
                chaine += $"{kvp.Key} : {kvp.Value.Nom}\n";
            }

            chaine += "\nVoici les constellations sur la carte :\n";

            foreach (Constellation constellation in lesConstellations)
            {
                chaine += $"{constellation}\n";
            }

            return chaine;
        }
    }
}
