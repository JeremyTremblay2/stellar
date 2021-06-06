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
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Appli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// On instancie notre Manager.
        /// </summary>
        public Manager LeManager { get; private set; } = new Manager(new DataContractPers(), new DataContractPers());
        
        /// <summary>
        /// Constructeur même de notre application. Appelle la méthode de chargement des données du Manager.
        /// </summary>
        public App()
        {
            LeManager.ChargeDonnees();
        }
    }
}
