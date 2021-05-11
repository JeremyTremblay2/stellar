using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Test
    {
        static void Main(string[] args)
        {
            Test_Astres test_Astres = new Test_Astres();
            test_Astres.Test();
            Test_Constellation test_Constellation = new Test_Constellation();
            test_Constellation.TestRelier();
            test_Constellation.TestDeplaceEtoile();
            test_Constellation.TestFusion();
            test_Constellation.TestSupprimer();
            Test_Carte test_carte = new Test_Carte();
            test_carte.Init();
            test_carte.TestDeplacerUnAstre();
            test_carte.TestRelierDeuxEtoiles();            
        }
    }
}
