using Espace;

namespace Modele
{
    /// <summary>
    /// Fabrique permettant de créer non pas des astres, mais des planètes. Il s'agit d'une spécification de la fabrique d'astres, qui 
    /// utilise la fabrique d'astres, mais cette fois sous la forme de fabrique de planète.
    /// </summary>
    public class FabriqueDePlanete : FabriqueDAstre<FabriqueDePlanete, Planete>
    {
        /// <summary>
        /// Méthode permettant d'ajouter une présence de vie à la planète. Appelle le mutateur de cet attribut pour modifier sa valeur.
        /// </summary>
        /// <param name="vie">Une présence de vie sous forme de chaîne de caractères, qui permet de préciser si de la vie existe sur la planète.</param>
        /// <returns>Une fabrique de planète.</returns>
        public FabriqueDePlanete PresenceDeVie(string vie)
        {
            Astre.Vie = vie;
            return this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter un type à la planète. Appelle le mutateur de cet attribut pour modifier sa valeur.
        /// </summary>
        /// <param name="type">Le type de la planète que l'on veut lui ajouter (défini dans l'énumération TypePlanete).</param>
        /// <returns>Une fabrique de planète.</returns>
        public FabriqueDePlanete AvecType(TypePlanete type)
        {
            Astre.Type = type;
            return this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter un système stellaire à la planète. Appelle le mutateur de cet attribut pour modifier sa 
        /// valeur.
        /// </summary>
        /// <param name="systeme">Le système stellaire de la planète que l'on veut lui ajouter, sous forme de chaîne de caractères.</param>
        /// <returns>Une fabrique de planète.</returns>
        public FabriqueDePlanete EstDansLeSysteme(string systeme)
        {
            Astre.Systeme = systeme;
            return this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter des informations sur la présence d'eau sur la planète. Appelle le mutateur de cet 
        /// attribut pour modifier sa valeur.
        /// </summary>
        /// <param name="eauPresente">Un booléen indiquant si de l'eau existe sur la planète.</param>
        /// <returns>Une fabrique de planète.</returns>
        public FabriqueDePlanete EauEstPresente(bool eauPresente)
        {
            Astre.EauPresente = eauPresente;
            return this;
        }
    }
}