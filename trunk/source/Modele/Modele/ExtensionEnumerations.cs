using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Modele
{
    /// <summary>
    /// Classe utilitaire sur les énumérations. Contient une méthode permettant de modifier l'affichage d'une énumération.
    /// </summary>
    public static class ExtensionEnumerations
    {
        /// <summary>
        /// Méthode permettant de modifier l'affichage d'une énumération, pour qu'elle apparaisse sous forme de chaîne de caractères.
        /// </summary>
        /// <param name="valeurEnumeration">L'énumération en question que l'on veut convertir en chaîne de caractères.</param>
        /// <returns>La chaîne de caractères créée.</returns>
        public static string RecupererValeurEnum(this Enum valeurEnumeration)
        {
            string valeur;

            //On récupère le type de l'énumération, le mebre correspondant et on le transforme en chaîne.
            valeur = valeurEnumeration.GetType()
                                      .GetMember(valeurEnumeration.ToString())
                                      .FirstOrDefault()
                                      .GetCustomAttribute<DisplayAttribute>()
                                      .Name;
            return valeur;
        }
    }
}
