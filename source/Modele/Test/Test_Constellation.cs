using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Espace;
using Geometrie;
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
                constel.Relier(new Point(42, 42), new Point(85, 36));
                constel.Relier(new Point(85, 36), new Point(20, 31));
                constel.Relier(new Point(20, 31), new Point(45, 69));

                constel.Relier(new Point(42, 42), new Point(95, 52));
                constel.Relier(new Point(95, 52), new Point(12, 89));
                constel.Relier(new Point(12, 89), new Point(22, 57));
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
        /// <summary>
        /// Test de la méthode DeplaceEtoile
        /// </summary>
        public void TestDeplaceEtoile()
        {
            Point pt = new Point(42, 42);
            Point nvPt = new Point(43, 43);
            constel.DeplacePoint(pt, nvPt);
            Console.WriteLine($"Déplacer le point {pt}:\n{constel.ToString()}");
        }
        /// <summary>
        /// Test de la méthode FusionnerAvec
        /// </summary>
        public void TestFusion()
        {
            Constellation constel2 = new Constellation(new Point(13, 13), new Point(25, 100));
            constel.FusionnerAvec(constel2, new Point(42,42), new Point(13, 13));
            Console.WriteLine($"fusion :\n{constel.ToString()}");
        }

        public void TestParcours()
        {
            Constellation constel3 = constel.DiviserConstellation();
            Console.WriteLine($"Parcours :\n{constel.ToString()}");
            if (constel3 != null)
            {
                Console.WriteLine($"ParcoursRejeton :\n{constel3.ToString()}");
            }
            
        }
    }
}
