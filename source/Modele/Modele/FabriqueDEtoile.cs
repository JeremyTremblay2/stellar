namespace Modele
{
    public class FabriqueDEtoile : FabriqueDAstre<FabriqueDEtoile,Etoile>
    {
        public FabriqueDEtoile AvecLuminosite(float luminosite)
        {
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
