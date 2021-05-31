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
    public class DataContractPers : IPersistanceManager, IPersistanceCarte
    {

        public string CheminFichier { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "..//XML");

        public string NomFichier { get; set; } = "astres.xml";

        string PersChemin => Path.Combine(CheminFichier, NomFichier);

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

        public void SauvegardeDonnees(IEnumerable<Astre> astres)
        {
            var serializer = new DataContractSerializer(typeof(IEnumerable<Astre>));

            if (!Directory.Exists(CheminFichier))
            {
                Directory.CreateDirectory(CheminFichier);
            }

            var parametres = new XmlWriterSettings() { Indent = true };

            using(TextWriter tw = File.CreateText(PersChemin))
            {
                using(XmlWriter writer = XmlWriter.Create(tw, parametres))
                {
                    serializer.WriteObject(writer, astres);
                }
            }
        }

        public void SauvegardeDonneesCarte(Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations, string cheminFichier)
        {
            var serializer = new DataContractSerializer(typeof(DataCartePersist), 
                                            new DataContractSerializerSettings()
                                            {
                                                PreserveObjectReferences = true
                                            });

            DataCartePersist data = new DataCartePersist();
            data.Constellations.AddRange(constellations);
            foreach(KeyValuePair<Point, Astre> kvp in astres)
            {
                data.Dico.Add(kvp.Key, kvp.Value);
            }
            

            var parametres = new XmlWriterSettings() { Indent = true };

            using (TextWriter tw = File.CreateText(cheminFichier))
            {
                using (XmlWriter writer = XmlWriter.Create(tw, parametres))
                {
                    serializer.WriteObject(writer, data);
                }
            }
        }

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
                catch (Exception e)
                {
                    throw new SerializationException("Le fichier sélectionné semble innacessible ou corrompu.\n" +
                        "Veuillez rééssayer.");
                }
            }

            return (data.Dico, data.Constellations);
        }
    }
}
