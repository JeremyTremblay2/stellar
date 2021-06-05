using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Interface permettant d'incorporer le patron Stratégie. Représente un type de persistance et contient des méthodes de chargement
    /// et de sauvegarde concernant les données de la Carte.
    /// </summary>
    public interface IPersistanceCarte
    {
        /// <summary>
        /// Méthode sans corps ayant pour but de retourner un dictionnaire de points et d'astres, ainsi qu'une liste de constellation, après
        /// chargement des données.
        /// </summary>
        /// <param name="cheminFichier">Le chemin du fichier à charger.</param>
        /// <returns>Deux collections chargées dans le fichier.</returns>
        (Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations) ChargeDonneesCarte(string cheminFichier);

        /// <summary>
        /// Méthode sans corps ayant pour but de sauvegarder un dictionnaire de points et d'astres, ainsi qu'une liste de constellations.
        /// </summary>
        /// <param name="astres">Le dictionnaire à sauvegarder.</param>
        /// <param name="constellations">La liste de constellations à sauvegarder.</param>
        /// <param name="cheminFichier">Le chemin vers le fichier où les données seront sérialisées.</param>
        void SauvegardeDonneesCarte(Dictionary<Point, Astre> astres, IEnumerable<Constellation> constellations, string cheminFichier);
    }
}
