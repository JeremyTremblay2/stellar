using Modele;
using System;
using Geometrie;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Fonctionnels
{
    /// <summary>
    /// Classe permettant de tester la persistance en DataContract.
    /// </summary>
    public static class Test_DataContractPers
    {
        /// <summary>
        /// Méthode permettant de tester le chargement en Datacontract.
        /// </summary>
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
        }
    }
}
