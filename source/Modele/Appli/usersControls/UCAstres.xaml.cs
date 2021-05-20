using Modele;
using System;
using System.Collections.Generic;
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

namespace Appli.usersControls
{
    /// <summary>
    /// Logique d'interaction pour UCAstres.xaml
    /// </summary>
    public partial class UCAstres : UserControl
    {

        public Manager Manager => (App.Current as App).LeManager;

        public UCAstres()
        {
            InitializeComponent();
            DataContext = Manager;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }
    }
}
