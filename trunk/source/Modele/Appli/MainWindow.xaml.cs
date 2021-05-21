using Modele;
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
            SpectateurOn.Visibility = Visibility.Hidden;
            
        }

        private void popupClicMenu(object sender, MouseButtonEventArgs e)
        {
            Popup.Visibility = Visibility.Visible; //(Application.Current.MainWindow as MainWindow).
            
        }

        private void SpecEnter(object sender, MouseEventArgs e)
        {
            SpectateurOn.Fill = Brushes.Gray;
            SpectateurOff.Fill = Brushes.Gray;
        }

        private void SpecLeave(object sender, MouseEventArgs e)
        {
            SpectateurOn.Fill = Brushes.AliceBlue;
            SpectateurOff.Fill = Brushes.AliceBlue;
        }

        private void SpecDown(object sender, MouseButtonEventArgs e)
        {
            if (SpectateurOff.Visibility == Visibility.Visible)
            {
                SpectateurOn.Visibility = Visibility.Visible;
                SpectateurOff.Visibility = Visibility.Hidden;
            } else
            {
                SpectateurOff.Visibility = Visibility.Visible;
                SpectateurOn.Visibility = Visibility.Hidden;
            }
        }
    }
}
