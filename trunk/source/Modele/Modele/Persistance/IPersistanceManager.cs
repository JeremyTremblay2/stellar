using Espace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Interface permettant d'incorporer le patron Stratégie. Représente un type de persistance et contient des méthodes de chargement
    /// et de sauvegarde concernant les données du Manager.
    /// </summary>
    public interface IPersistanceManager
    {
        /// <summary>
        /// Méthode sans corps ayant pour but de retourner une collection d'astres, après chargement des données.
        /// </summary>
        /// <returns>La collection d'astres qui vient d'être chargée.</returns>
        IEnumerable<Astre> ChargeDonnees();

        /// <summary>
        /// Méthode sans corps utilisée pour la sauvegarde d'une collection d'astres.
        /// </summary>
        /// <param name="astres">La collection à sérialiser.</param>
        void SauvegardeDonnees(IEnumerable<Astre> astres);
    }
}
