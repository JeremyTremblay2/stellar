using Espace;
using Modele;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Appli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Manager Manager => (Application.Current as App).LeManager;
        public MainWindow()
        { 
            InitializeComponent();
            DataContext = Manager;            
        }

        private void popupClicMenu(object sender, MouseButtonEventArgs e)
        {
            Popup.Visibility = Visibility.Visible; //(Application.Current.MainWindow as MainWindow).
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.ChargeDonnees();
            Manager.AjouterUnAstre(new Geometrie.Point(12, 23), new FabriqueDePlanete().Initialiser("Terre")
                                       .AvecDescription("La Terre est la troisième planète par ordre d'éloignement au Soleil et la cinquième " +
                                       "plus grande du Système solaire aussi bien par la masse que le diamètre. Par ailleurs, elle est le seul " +
                                       "objet céleste connu pour abriter la vie.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(1)
                                       .AvecTemperature(288)
                                       .PresenceDeVie("Oui")
                                       .EstDansLeSysteme("Solaire")
                                       .EauEstPresente(true)
                                       .AvecType(TypePlanete.Tellurique)
                                       .AvecImage("terre.jpg")
                                       .Construire());
        }
    }
}
