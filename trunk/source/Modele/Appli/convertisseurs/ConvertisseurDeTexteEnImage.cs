using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Appli.convertisseurs
{
    public class ConvertisseurDeTexteEnImage : IValueConverter
    {
        public static string cheminImages;
        static ConvertisseurDeTexteEnImage()
        {
            cheminImages = Path.Combine(Directory.GetCurrentDirectory(), @"..\images\");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string nomImage = value as string;

            if (string.IsNullOrWhiteSpace(nomImage)) return null;

            string cheminImage = Path.Combine(cheminImages, nomImage);

            return new Uri(cheminImage, UriKind.RelativeOrAbsolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
