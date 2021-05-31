using DataContractPersistance;
using Modele;
using JSONPersistance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Appli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Manager LeManager { get; private set; } = new Manager(new JSONPers(), new DataContractPers());

        public App()
        {
            LeManager.ChargeDonnees();
        }
    }
}
