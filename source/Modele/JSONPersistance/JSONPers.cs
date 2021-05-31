using System;
using System.Collections.Generic;
using System.IO;
using Espace;
using Modele;
using Newtonsoft.Json;

namespace JSONPersistance
{
    public class JSONPers : IPersistanceManager
    {
        public string CheminFichier { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "..//JSON");

        public string NomFichier { get; set; } = "astres.json";

        string PersChemin => Path.Combine(CheminFichier, NomFichier);

        public IEnumerable<Astre> ChargeDonnees()
        {
            if (!File.Exists(PersChemin))
            {
                throw new FileNotFoundException("Le fichier de données de l'application est manquant ou innaccessible.");
            }

            var json = File.ReadAllText(PersChemin);
            return JsonConvert.DeserializeObject<IEnumerable<Astre>>(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public void SauvegardeDonnees(IEnumerable<Astre> astres)
        {
            if (!Directory.Exists(CheminFichier))
            {
                Directory.CreateDirectory(CheminFichier);
            }

            string json = JsonConvert.SerializeObject(astres, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(PersChemin, json);
        }
    }
}
