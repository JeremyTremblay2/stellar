using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Modele
{
    public static class ExtensionEnumerations
    {
        public static string RecupererValeurEnum(this Enum valeurEnumeration)
        {
            string valeur;
            valeur = valeurEnumeration.GetType()
                                      .GetMember(valeurEnumeration.ToString())
                                      .FirstOrDefault()
                                      .GetCustomAttribute<DisplayAttribute>()
                                      .Name;
            return valeur;
        }
    }
}
