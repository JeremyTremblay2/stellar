using Espace;
using Modele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace DataContractPersistance
{
    public class DataContractPers : IPersistanceManager
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
    }
}
