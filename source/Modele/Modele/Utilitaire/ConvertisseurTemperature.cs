using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitaire
{
    /// <summary>
    /// Classe statique permettant la conversion de températures.
    /// </summary>
    static class ConvertisseurTemperature
    {
        /// <summary>
        /// Méthode permettant la convertion de degrés Fahrenheit en degrés Celsius.
        /// </summary>
        /// <param name="tempFahrenheit">La température en Fahrenheit à convertir en degrés Celsius.</param>
        /// <returns>Une nouvelle température, en degrés Celsius cette fois-ci.</returns>
        public static float ToCelsius(this float tempFahrenheit)
            => (float)Math.Round((tempFahrenheit - 32) * 5 / 9, 2);

        /// <summary>
        /// Méthode permettant la convertion de degrés Celsius en degrés Fahrenheit.
        /// </summary>
        /// <param name="tempCelsius">La température en Celsius à convertir en degrés Fahrenheit.</param>
        /// <returns>Une nouvelle température, en degrés Fahrenheit cette fois-ci.</returns>
        public static float ToFahrenheit(this float tempCelsius)
            => (float)Math.Round((tempCelsius * 9 / 5) + 32, 2);
    }
}
    