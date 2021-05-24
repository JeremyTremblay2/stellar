using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Appli.convertisseurs
{
    /// <summary>
    /// Classe permettant d'ajouter une valeur à un entier depuis le code XAML.
    /// </summary>
    public class ConvertisseurDeValeurs : IValueConverter
    {
        /// <summary>
        /// La méthode permettant la conversion. Il vient ajouter une valeur à l'objet s'il s'agit bien d'un entier, et retourne
        /// la nouvelle valeur.
        /// </summary>
        /// <param name="value">La valeur à incrémenter.</param>
        /// <param name="targetType">Le type de la valeur à incrémenter.</param>
        /// <param name="parameter">Le paramètre qui va venir incrémenter la valeur.</param>
        /// <param name="culture">Paramètre permettant de spécifier une langue.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = value;
            int parameterValue;

            if (value != null && targetType == typeof(double) && int.TryParse((string)parameter,
                NumberStyles.Integer, culture, out parameterValue))
            {
                result = (int)value + (int)parameterValue;
            }

            return result;
        }

        /// <summary>
        /// Convertisseur faisant l'opération inverse, non implémenté.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
