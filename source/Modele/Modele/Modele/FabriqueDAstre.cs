using Espace;
using System;

namespace Modele
{
    /// <summary>
    /// Fabrique permettant de créer des Astres.
    /// </summary>
    /// <typeparam name="T">Type de Fabrique (fabrique d'étoiles ou fabrique de planètes)</typeparam>
    /// <typeparam name="P">Type d'Astre (peut-être Etoile ou Planete)</typeparam>
    public class FabriqueDAstre<T,P> where T : FabriqueDAstre<T,P> where P : Astre, new()
    {
        /// <summary>
        /// Propriété concernant l'astre le type d'astre qui va être crée par la fabrique (ici P). 
        /// </summary>
        protected P Astre { get; private set; }

        /// <summary>
        /// Méthode permettant d'initialiser et de lancer la construction de l'astre.
        /// Pour cela, il faut fournir le nom de l'astre en paramètre, et éventuellement s'il est personnalisé ou non (par défaut c'est
        /// faux). On instancie ensuite un P (qui correspond au type de l'astre crée).
        /// </summary>
        /// <param name="nom">Le nom de l'astre qui va être crée.</param>
        /// <param name="personnalise">Un booléen permettant de dire si on veut que l'astre soit personnalisé ou non.</param>
        /// <returns>Une fabrique d'astres de type T.</returns>
        public T Initialiser(string nom, bool personnalise = false)
        {
            Astre = new P();
            Astre.Nom = nom;
            Astre.Personnalise = personnalise;
            return (T)this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une description à un astre. Appelle le mutateur de la description de l'astre pour modifier sa valeur.
        /// </summary>
        /// <param name="description">Une chaîne de caractères donnant une description de l'astre.</param>
        /// <returns>Une fabrique d'astre de type T.</returns>
        public T AvecDescription(string description)
        {
            Astre.Description = description;
            return (T)this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter un age à un astre. Appelle le mutateur de l'âge de l'astre pour modifier sa valeur.
        /// </summary>
        /// <param name="age">Un entier long représentant l'âge de l'astre.</param>
        /// <returns>Une fabrique d'astre de type T.</returns>
        public T AvecAge(long age)
        {
            Astre.Age = age;
            return (T)this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une masse à un astre. Appelle le mutateur de la masse de l'astre pour modifier sa valeur.
        /// </summary>
        /// <param name="masse">Un flottant représentant en masse terrestre ou masse solaire la masse de l'astre.</param>
        /// <returns>Une fabrique d'astre de type T.</returns>
        public T AvecMasse(float masse)
        {
            Astre.Masse = masse;
            return (T)this;
        }

        /// <summary>
        /// Méthode permettant d'ajouter une température à un astre. Appelle le mutateur de la température de l'astre pour modifier 
        /// sa valeur.
        /// </summary>
        /// <param name="temperature">Un entier représentant la température de l'astre (en kelvin).</param>
        /// <returns>Une fabrique d'astre de type T.</returns>
        public T AvecTemperature(int temperature)
        {
            Astre.Temperature = temperature;
            return (T)this;
        }

        /// <summary>
        /// Méthode permettant de construire un astre, et donc de retourner cette fois-ci non pas une fabrique de type T, mais un astre P.
        /// </summary>
        /// <returns>Un astre P correspondant au type générique que l'on a décidé de construire jusqu'à maintenant (Etoile ou Planete).</returns>
        public P Construire()
        {
            return Astre;
        }
    }
}
