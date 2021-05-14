using Espace;

namespace Modele
{
    public class FabriqueDePlanete : FabriqueDAstre<FabriqueDePlanete, Planete>
    {
        public FabriqueDePlanete PresenceDeVie(string vie)
        {
            Astre.Vie = vie;
            return this;
        }

        public FabriqueDePlanete AvecType(TypePlanete type)
        {
            Astre.Type = type;
            return this;
        }

        public FabriqueDePlanete EstDansLeSysteme(string systeme)
        {
            Astre.Systeme = systeme;
            return this;
        }
        public FabriqueDePlanete EauEstPresente(bool eauPresente)
        {
            Astre.EauPresente = eauPresente;
            return this;
        }
    }
}