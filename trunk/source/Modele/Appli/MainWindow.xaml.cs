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
            Button bouton = sender as Button;
            Astre astre = bouton.DataContext as Astre;

            //Manager.ModifierFavori(astre);

            foreach(Astre astr in Manager.LesAstres)
            {
                Debug.WriteLine(astr);
            }

            DataContext = Manager;
        }
    }
}
