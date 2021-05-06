namespace Modele
{
    /// <summary>
    /// Constructeur permettant de créer des Astres.
    /// </summary>
    /// <typeparam name="T">Type de Builder</typeparam>
    /// <typeparam name="P">Type d'Astre (peut-être Etoile ou Planete)</typeparam>
    public class FabriqueDAstre<T,P> where T : FabriqueDAstre<T,P> where P : Astre, new()
    {
        protected P Astre { get; set; }

        public T Initialiser(bool personnalise)
        {
            Astre = new P();
            Astre.Personnalise = personnalise;
            return (T)this;
        }

        public T AvecNom(string nom)
        {
            Astre.Nom = nom;
            return (T)this;
        }

        public T AvecDescription(string description)
        {
            Astre.Description = description;
            return (T)this;
        }

        public T AvecAge(long age)
        {
            Astre.Age = age;
            return (T)this;
        }

        public T AvecMasse(float masse)
        {
            Astre.Masse = masse;
            return (T)this;
        }

        public T AvecTemperature(int temperature)
        {
            Astre.Temperature = temperature;
            return (T)this;
        }

        public P Construire()
        {
            return Astre;
        }
    }
}
