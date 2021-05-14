using Espace;
using System;

namespace Modele
{
    public class FabriqueDEtoile : FabriqueDAstre<FabriqueDEtoile,Etoile>
    {
        public FabriqueDEtoile AvecLuminosite(float luminosite)
        {
            if (luminosite < 0)
            {
                throw new ArgumentException("La luminosité d'un astre ne peut pas être négative.");
            }
            Astre.Luminosite = luminosite;
            return this;
        }

        public FabriqueDEtoile AvecType(TypeEtoile type)
        {
            Astre.Type = type;
            return this;
        }

        public FabriqueDEtoile EstDansLaConstellation(string constellation)
        {
            Astre.Constellation = constellation;
            return this;
        }
    }
}
