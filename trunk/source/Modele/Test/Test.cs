﻿using System;
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
			Test_Constellation test_Constellation = new Test_Constellation();            test_Constellation.TestRelier();
            test_Constellation.TestSupprimer();
            test_Constellation.TestParcours();
            /*test_Constellation.TestDeplaceEtoile();
            test_Constellation.TestFusion();
			test_Constellation.TestSupprimer();
            Test_Carte test_carte = new Test_Carte();            test_carte.Init();
            test_carte.TestRelierDeuxEtoiles();
            test_carte.TestDeplacerUnAstre();*/

            Test_Manager.TestCreationManager();
            Test_Manager.TestAjoutEtSuppressionAstre();
        }
    }
}
