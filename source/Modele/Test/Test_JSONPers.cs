using Modele;
using JSONPersistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tests_Fonctionnels
{
    public static class Test_JSONPers
    {
        public static void TestChargement()
        {
            //Création du manager et chargement du stub.
            Manager manager = new Manager(new Stub.Stub(), new Stub.Stub());
            manager.ChargeDonnees();
            Console.WriteLine(manager);

            //Sauvegarde en JSON des données.
            manager.Persistance = new JSONPers();
            manager.SauvegardeDonnees();

            //Chargement des données précédemment sauvegardées après instanciation d'un nouveau manager.
            Manager manager2 = new Manager(new JSONPers(), new DataContractPersistance.DataContractPers());
            manager2.ChargeDonnees();
            Console.WriteLine(manager2);
        }
    }
}
