using Geometrie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Fonctionnels
{
    /// <summary>
    /// Classe de tests fonctionnels portant sur l'aspect géométrie.
    /// </summary>
    public static class Test_Geometrie
    {
        /// <summary>
        /// Méthode de tests fonctionnels sur les points.
        /// </summary>
        public static void TestPoint()
        {
            //Création de quelques points et affichage.
            var lesPoints = new List<Point>()
            {
                new Point(12, 17),
                new Point(36, 23),
                new Point(14, 3),
                new Point(),
                new Point(),
            };

            foreach (Point point in lesPoints)
            {
                Console.WriteLine(point);
            }

            //Déplacement de deux unités en abscisses pour chaque point, puis affcihage.
            for (int i = 0; i < lesPoints.Count(); i++)
            {
                lesPoints[i].Deplacer(lesPoints[i].X + 2, lesPoints[i].Y);
            }

            Console.WriteLine("Après déplacement des points, voici leur valeur :");

            foreach (Point point in lesPoints)
            {
                Console.WriteLine(point);
            }

            //Vérification des protocoles d'égalité.

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"{lesPoints[0]} equals {lesPoints[1]} ?  =>  {lesPoints[0].Equals(lesPoints[1])}");
            Console.WriteLine($"{lesPoints[1]} equals {lesPoints[2]} ?  =>  {lesPoints[1].Equals(lesPoints[2])}");
            Console.WriteLine($"{lesPoints[2]} equals {lesPoints[3]} ?  =>  {lesPoints[2].Equals(lesPoints[3])}");
            Console.WriteLine($"{lesPoints[3]} equals {lesPoints[4]} ?  =>  {lesPoints[3].Equals(lesPoints[4])}");
            Console.WriteLine($"{lesPoints[0]} equals {lesPoints[0]} ?  =>  {lesPoints[0].Equals(lesPoints[0])}");
            Console.WriteLine($"{lesPoints[1]} equals {lesPoints[1]} ?  =>  {lesPoints[1].Equals(lesPoints[1])}");
            Console.WriteLine($"{lesPoints[2]} equals {lesPoints[2]} ?  =>  {lesPoints[2].Equals(lesPoints[2])}");
            Console.WriteLine($"{lesPoints[3]} equals {lesPoints[3]} ?  =>  {lesPoints[3].Equals(lesPoints[3])}");
            Console.WriteLine($"{lesPoints[4]} equals {lesPoints[4]} ?  =>  {lesPoints[4].Equals(lesPoints[4])}");

            Console.WriteLine("-----------------------------------------");

            Console.WriteLine($"{lesPoints[0]} hashcode similaire à {lesPoints[1]} ?  =>  {lesPoints[0].GetHashCode() == (lesPoints[1].GetHashCode())}");
            Console.WriteLine($"{lesPoints[1]} hashcode similaire à {lesPoints[2]} ?  =>  {lesPoints[1].GetHashCode() == (lesPoints[2].GetHashCode())}");
            Console.WriteLine($"{lesPoints[2]} hashcode similaire à {lesPoints[3]} ?  =>  {lesPoints[2].GetHashCode() == (lesPoints[3].GetHashCode())}");
            Console.WriteLine($"{lesPoints[3]} hashcode similaire à {lesPoints[4]} ?  =>  {lesPoints[3].GetHashCode() == (lesPoints[4].GetHashCode())}");
            Console.WriteLine($"{lesPoints[0]} hashcode similaire à {lesPoints[0]} ?  =>  {lesPoints[0].GetHashCode() == (lesPoints[0].GetHashCode())}");
            Console.WriteLine($"{lesPoints[1]} hashcode similaire à {lesPoints[1]} ?  =>  {lesPoints[1].GetHashCode() == (lesPoints[1].GetHashCode())}");
            Console.WriteLine($"{lesPoints[2]} hashcode similaire à {lesPoints[2]} ?  =>  {lesPoints[2].GetHashCode() == (lesPoints[2].GetHashCode())}");
            Console.WriteLine($"{lesPoints[3]} hashcode similaire à {lesPoints[3]} ?  =>  {lesPoints[3].GetHashCode() == (lesPoints[3].GetHashCode())}");
            Console.WriteLine($"{lesPoints[4]} hashcode similaire à {lesPoints[4]} ?  =>  {lesPoints[4].GetHashCode() == (lesPoints[4].GetHashCode())}");
        }

        /// <summary>
        /// Méthode de tests fonctionnels sur les segments.
        /// </summary>
        public static void TestSegment()
        {

        }
    }
}
