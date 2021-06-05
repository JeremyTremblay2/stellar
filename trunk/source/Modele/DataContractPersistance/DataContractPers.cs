using Espace;
using Geometrie;
using Modele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace DataContractPersistance
{
    /// <summary>
    /// Classe permettant la persistance en DataContract To XML de la carte et du Manager.
    /// </summary>
    public class DataContractPers : IPersistanceManager, IPersistanceCarte
    {
        /// <summary>
        /// Propriété concernant le chemin du fichier dans lequel seront chargées ou sauvegardées les données.
        /// </summary>
        public string CheminFichier { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "..//XML");

        /// <summary>
        /// Propriété concernant le nom du fichier.
        /// </summary>
        public string NomFichier { get; set; } = "astres.xml";

        /// <summary>
        /// Propriété comme étant la combinaison du nom du fichier et de son chemin.
        /// </summary>
        string PersChemin => Path.Combine(CheminFichier, NomFichier);

        /// <summary>
        /// Méthode de Chargement des données du Manager.
        /// Lève une exception si le fichier est inaccesible, puis charge la collection et la retourne.
        /// </summary>
        /// <returns>La collection d'astres qui vient d'être chargée.</returns>
        public IEnumerable<Astre> ChargeDonnees()
        {
            if (!File.Exists(PersChemin))
            {
                throw new FileNotFoundException("Le fichier de données de l'application est manquant ou innaccessible.");
            }

            var serializer = new DataContractSerializer(typeof(IEnumerable<Astre>));

            IEnumerable<Astre> astres;

            using(Stream s = File.OpenRead(PersChemin))
            {
                astres = serializer.ReadObject(s) as IEnumerable<Astre>;
            }

            return astres;
        }

        /// <summary>
        /// Méthode de sauvegarder des données du Manager. 
        /// Crée éventuellement le dossier destinaton, puis sérialise la collection d'astres passée en paramètre.
        /// </summary>
        /// <param name="astres">La collection d'astres à sérialiser.</param>
        public void SauvegardeDonnees(IEnumerable<Astre> astres)
        {
            var serializer = new DataContractSerializer(typeof(IEnumerable<Astre>));

            if (!Directory.Exists(CheminFichier))
            {
                Directory.CreateDirectory(CheminFichier);
            }

            //Pour l'indentation du fichier.
            var parametres = new XmlWriterSettings() { Indent = true };

            using(TextWriter tw = File.CreateText(PersChemin))
            {
                using(XmlWriter writer = XmlWriter.Create(tw, parametres))
                {
                    serializer.WriteObject(writer, astres);
                }
            }
        }

        /// <summary>
        /// Méthode permettant la sauvegarde des données de la Carte.
        /// Enregistre le dictionnaire et la collection de constellation dans le fichier XML.
        /// Pour cela on vient utiliser un DataCartePersist qui va faciliter la sauvegarde des données.
        /// </summary>
        /// <param name="astres">Le dictionnaire de points et d'astres à sérialiser.</param>
        /// <param name="constellations">La collection de constellations à enregistrer.</param>
        /// <param name="cheminFichier">Le chemin du fichier vers lequel sauvegarder ces données.</param>
        public void SauvegardeDonneesCarte(Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations, string cheminFichier)
        {
            //Pour que les références soient conservées et éviter des problèmes lors du chargement.
            var serializer = new DataContractSerializer(typeof(DataCartePersist), 
                                            new DataContractSerializerSettings()
                                            {
                                                PreserveObjectReferences = true
                                            });

            //Utilisation de notre DataCartePersist et de ses deux propriétés.
            DataCartePersist data = new DataCartePersist();
            data.Constellations.AddRange(constellations);
            foreach(KeyValuePair<Point, Astre> kvp in astres)
            {
                data.Dico.Add(kvp.Key, kvp.Value);
            }
            
            //Pour l'indentation.
            var parametres = new XmlWriterSettings() { Indent = true };

            using (TextWriter tw = File.CreateText(cheminFichier))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, parametres))
                {
                    serializer.WriteObject(writer, data);
                }
            }
        }

        /// <summary>
        /// Méthode de chargement des données de la Carte. Lève une exception si le fichier passé en paramètre n'existe pas ou est corrompu.
        /// Permet de charger les données d'un fichier passé en paramètre puis les retourne.
        /// </summary>
        /// <param name="cheminFichier">Le nom du fichier dans lequel les données seront chargées.</param>
        /// <returns>Un dictionnaire de points et d'astres, ainsi qu'une collection de constellations qui viennent d'être chargées.</returns>
        public (Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations) ChargeDonneesCarte(string cheminFichier) 
        {
            if (!File.Exists(cheminFichier))
            {
                throw new FileNotFoundException("Le fichier de données de la carte est manquant ou innaccessible.");
            }

            var serializer = new DataContractSerializer(typeof(DataCartePersist));

            DataCartePersist data;

            using (Stream s = File.OpenRead(cheminFichier))
            {
                try
                {
                    data = serializer.ReadObject(s) as DataCartePersist;
                }
                catch (Exception)
                {
                    throw new SerializationException("Le fichier sélectionné semble innacessible ou corrompu.\n" +
                        "Veuillez rééssayer.");
                }
            }

            return (data.Dico, data.Constellations);
        }
    }
}
