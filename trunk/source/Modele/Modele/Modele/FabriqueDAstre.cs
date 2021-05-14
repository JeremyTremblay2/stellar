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
        protected P Astre { get; private set; }

        public T Initialiser(string nom, bool personnalise = false)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Le nom doit être renseigné et ne peut pas être vide ou null");
            }
            Astre = new P();
            Astre.Nom = nom;
            Astre.Personnalise = personnalise;
            return (T)this;
        }

        public T AvecDescription(string description)
        {
            Astre.Description = description;
            return (T)this;
        }

        public T AvecAge(long age)
        {
            if (age < 0)
            {
                throw new ArgumentException("L'âge de l'astre ne peut pas être négatif.");
            }
            Astre.Age = age;
            return (T)this;
        }

        public T AvecMasse(float masse)
        {
            if (masse < 0)
            {
                throw new ArgumentException("La masse de l'astre ne peut pas être négative.");
            }
            Astre.Masse = masse;
            return (T)this;
        }

        public T AvecTemperature(int temperature)
        {
            if (temperature < 0)
            {
                throw new ArgumentException("La température de l'astre ne peut pas être négative.");
            }
            Astre.Temperature = temperature;
            return (T)this;
        }

        public P Construire()
        {
            return Astre;
        }
    }
}
