using System;
using Espace;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace Appli.convertisseurs
{
    public class ConvertisseurDEnumerationsEnTexte : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var chaineFinale = new StringBuilder();

            if (value == null)
                return null;

            if (!(value is Enum))
                return value;

            var chaineOriginelle = value.ToString();

            foreach(char caractere in chaineOriginelle)
            {
                if (char.IsUpper(caractere))
                {
                    chaineFinale.Append(' ');
                }
                chaineFinale.Append(caractere);
            }

            chaineFinale.Remove(0, 1);
            return chaineFinale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
