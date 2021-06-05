using System;
using System.Collections.Generic;
using System.IO;
using Espace;
using Modele;
using Newtonsoft.Json;

namespace JSONPersistance
{
    /// <summary>
    /// Classe permettant la sérialisation du Manager, en format de fichier JSON. Cette classe implémente notre interface de persistance.
    /// Utilise le Nuget Newtonsoft.
    /// </summary>
    public class JSONPers : IPersistanceManager
    {
        /// <summary>
        /// Le chemin vers le fichier.
        /// </summary>
        public string CheminFichier { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "..//JSON");

        /// <summary>
        /// Propriété concernant le nom de notre fichier.
        /// </summary>
        public string NomFichier { get; set; } = "astres.json";

        /// <summary>
        /// Propriété comme étant la combinaison du chemin et du fichier.
        /// </summary>
        string PersChemin => Path.Combine(CheminFichier, NomFichier);

        /// <summary>
        /// Méthode de chargement des données du Manager.
        /// Vérifie que le fichier de données existe (sinon lève une exception), puis charge les données depuis ce fichier.
        /// </summary>
        /// <returns>La collection d'astres qui vient d'être chargée du fichier.</returns>
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

        /// <summary>
        /// Méthode permettant la sauvegardes des données du Manager en JSON.
        /// Crée éventuellement le dossier destination, puis enregistre la collection passée en paramètre à l'intérieur.
        /// </summary>
        /// <param name="astres">La collection à sérialiser en JSON.</param>
        public void SauvegardeDonnees(IEnumerable<Astre> astres)
        {
            if (!Directory.Exists(CheminFichier))
            {
                Directory.CreateDirectory(CheminFichier);
            }

            string json = JsonConvert.SerializeObject(astres, new JsonSerializerSettings()
            {
                //Pour l'indentation.
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(PersChemin, json);
        }
    }
}
