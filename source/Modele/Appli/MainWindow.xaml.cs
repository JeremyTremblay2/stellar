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

        private void ModifierFavori(object sender, RoutedEventArgs e)
        {
            Button bouton = sender as Button;
            Astre astre = bouton.DataContext as Astre;
            astre.ModifierFavori();
        }
    }
}
