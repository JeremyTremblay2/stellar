using Modele;
using System;
using Geometrie;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Fonctionnels
{
    public static class Test_DataContractPers
    {
        public static void TestChargement()
        {
            //Création du manager et chargement du stub.
            Manager manager = new Manager(new Stub.Stub(), new Stub.Stub());
            manager.ChargeDonnees();
            Console.WriteLine(manager);

            //Sauvegarde en datacontract des données.
            manager.Persistance = new DataContractPersistance.DataContractPers();
            manager.SauvegardeDonnees();

            //Chargement des données précédemment sauvegardées après instanciation d'un nouveau manager.
            Manager manager2 = new Manager(new DataContractPersistance.DataContractPers(), new DataContractPersistance.DataContractPers());
            manager2.ChargeDonnees();
            Console.WriteLine(manager2);

            manager2.AjouterUnAstre(new Point(425, 13), manager2.LesAstres[0]);
            manager2.AjouterUnAstre(new Point(200, 320), manager2.LesAstres[1]);
            manager2.RelierDeuxEtoiles(new Point(200, 320), new Point(425, 13)); 

            //Sauvegarde des données de la carte.
            manager2.SauvegardeDonneesCarte(@"test.xml");
            Console.WriteLine(manager2);

            //Chargement des données de la carte précédemment sauvegardée.
            manager2.ChargeDonneesCarte(@"test.xml");
            Console.WriteLine(manager2);
        }
    }
}
