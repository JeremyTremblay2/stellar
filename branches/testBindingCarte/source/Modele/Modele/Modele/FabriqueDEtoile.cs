using Espace;
using System;

namespace Modele
{
    /// <summary>
    /// Fabrique permettant de créer non pas des astres, mais des étoiles. Il s'agit d'une spécification de la fabrique d'astres, qui 
    /// utilise la fabrique d'astres, mais cette fois sous la forme de fabrique d'étoiles.
    /// </summary>
    public class FabriqueDEtoile : FabriqueDAstre<FabriqueDEtoile,Etoile>
    {
        /// <summary>
        /// Méthode permettant d'ajouter une luminosité à l'étoile. Appelle le mutateur de luminosité afin de modifier sa valeur.
        /// </summary>
        /// <param name="luminosite">Un flottant représentant la luminosité de l'étoile.</param>
        /// <returns>Une fabrique d'étoile.</returns>
        public FabriqueDEtoile AvecLuminosite(float luminosite)
        {
            Astre.Luminosite = luminosite;
            return this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter un type à l'étoile. Appelle le mutateur de type afin de modifier sa valeur.
        /// </summary>
        /// <param name="type">Un type d'étoile comme défini dans l'énumération TypeEtoile.</param>
        /// <returns>Une fabrique d'étoile.</returns>
        public FabriqueDEtoile AvecType(TypeEtoile type)
        {
            Astre.Type = type;
            return this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une constellation à l'étoile. Appelle le mutateur de constellation afin de modifier sa valeur.
        /// </summary>
        /// <param name="constellation">Une chaîne de caractères indiquant dans quelle constellation se trouve l'étoile.</param>
        /// <returns>Une fabrique d'étoile.</returns>
        public FabriqueDEtoile EstDansLaConstellation(string constellation)
        {
            Astre.Constellation = constellation;
            return this;
        }
    }
}
