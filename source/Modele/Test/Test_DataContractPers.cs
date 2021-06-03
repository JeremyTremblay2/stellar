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

            //Création du manager et chargement du stub.
            Manager manager3 = new Manager(new Stub.Stub(), new Stub.Stub());
            manager3.ChargeDonnees();

            //Sauvegarde en datacontract des données.
            manager3.Persistance = new JSONPersistance.JSONPers();
            manager3.SauvegardeDonnees();

            //Chargement des données précédemment sauvegardées après instanciation d'un nouveau manager.
            Manager manager4 = new Manager(new JSONPersistance.JSONPers(), new DataContractPersistance.DataContractPers());
            manager4.ChargeDonnees();
            Console.WriteLine(manager4);
        }
    }
}
