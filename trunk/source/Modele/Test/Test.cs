using System;
using Tests_Fonctionnels;

namespace Test
{
    public class Test
    {
        public static void Main(string[] args)
        {
            Test_Astres.TestCreationAstres();
            Test_Geometrie.TestPoint();
			Test_Geometrie.TestSegment();
            Test_Constellation.TestRelier();
            Test_Constellation.TestSupprimer();
            Test_Constellation.TestParcours();
            /*test_Constellation.TestDeplaceEtoile();
            Test_Constellation.TestFusion();
			Test_Constellation.TestSupprimer();
            Test_Carte.Init();
            Test_Carte.TestRelierDeuxEtoiles();
            Test_Carte.TestDeplacerUnAstre();*/

            Test_Manager.TestCreationManager();
            Test_Manager.TestAjoutEtSuppressionAstre();
        }
    }
}
