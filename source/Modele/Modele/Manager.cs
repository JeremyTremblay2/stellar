using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Manager
    {
        List<Astre> lesAstres;
        Carte carte;

        public Manager()
        {
            lesAstres = new List<Astre>();
            carte = new Carte(true);
        }

        public void AjouterUnAstre(Point position, Astre astre)
        {
            if (astre != null)
            {
                lesAstres.Add(astre);
                carte.AjouterUnAstre(position, astre);
            }
        }

        public void SupprimerUnAstre(Point position)
        {
            Astre astreASupprimer = carte.SupprimerUnAstre(position);
            lesAstres.Remove(astreASupprimer);
        }

        public void SupprimerTout()
        {
            lesAstres.Clear();
            carte.SupprimerTout();
        }

        public void DeplacerUnAstre(Point anciennePosition, Point nouvellPosition)
            => carte.DeplacerUnAstre(anciennePosition, nouvellPosition);

        public void RelierDeuxEtoiles(Point point1, Point point2)
            => carte.DeplacerUnAstre(point1, point2);

        public void FaireUneRecherche()
        {

        }

        public override string ToString()
        {
            string chaine = "Les Astres contenus dans l'application sont :\n";

            if (lesAstres.Count() == 0)
            {
                chaine += "\tAucun astre.\n";
            }
            else
            {
                chaine += $"{lesAstres.Count()} Astre(s) au total :\n";
            }

            foreach (Astre astre in lesAstres)
            {
                chaine += $"\t{astre.Nom}\n";
            }

            return chaine;
        }
    }
}
