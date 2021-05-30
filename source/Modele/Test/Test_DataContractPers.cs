using Modele;
using System;
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
            Manager manager = new Manager(new Stub.Stub());
            manager.ChargeDonnees();
            Console.WriteLine(manager);
            manager.Persistance = new DataContractPersistance.DataContractPers();
            manager.SauvegardeDonnees();
        }
    }
}
