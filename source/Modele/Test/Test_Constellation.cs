using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele;

namespace Test
{
    /// <summary>
    /// Permets de tester la classe Constellation
    /// </summary>
    public class Test_Constellation
    {
        Constellation constel = new Constellation(new Point(42, 42), new Point(56, 56));
        /// <summary>
        /// Test de la méthode Relier
        /// </summary>
        public void TestRelier()
        {
           
            try
            {
                constel.Relier(new Point(85, 36), new Point(42, 42));
                constel.Relier(new Point(20, 31), new Point(85, 36));
                Console.WriteLine("Relier : \n" + constel.ToString());
            }
            catch (ArgumentException a)
            {
                Console.WriteLine("ERREUR :", a.Message);
            }
        }
        /// <summary>
        /// Test de la méthode Supprimer
        /// </summary>
        public void TestSupprimer()
        {
            Point pt = new Point(42, 42);
            constel.SupprimerLesLiens(pt);
            Console.WriteLine($"Suppression du point {pt}:\n{constel.ToString()}");
        }
    }
}
