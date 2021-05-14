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
    public static class ConvertisseurTemperature
    {
        private const float constanteKelvin = 273.15f;
        /// <summary>
        /// Méthode permettant la convertion de Kelvin en degrés Celsius.
        /// </summary>
        /// <param name="tempKelvin">La température en Kelvin à convertir en degrés Celsius.</param>
        /// <returns>Une nouvelle température, en degrés Celsius cette fois-ci.</returns>
        public static float ToCelsius(this float tempKelvin)
            => (float)Math.Round(tempKelvin - constanteKelvin);

        /// <summary>
        /// Méthode permettant la convertion de degrés Celsius en Kelvin.
        /// </summary>
        /// <param name="tempCelsius">La température en Celsius à convertir en Kelvin.</param>
        /// <returns>Une nouvelle température, en Kelvin cette fois-ci.</returns>
        public static float ToKelvin(this float tempCelsius)
            => (float)Math.Round(tempCelsius + constanteKelvin);
    }
}
    