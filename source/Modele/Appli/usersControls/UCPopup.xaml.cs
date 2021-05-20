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
    /// Logique d'interaction pour UCPopup.xaml
    /// </summary>
    public partial class UCPopup : UserControl
    {
        public UCPopup()
        {
            InitializeComponent();
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        private void buttonPopupClicCroix(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        private void buttonPopupDessusCroix(object sender, MouseEventArgs e)
        {
            QuitterPopup.Fill = Brushes.Gray;
        }

        private void buttonPopupHorsCroix(object sender, MouseEventArgs e)
        {
            QuitterPopup.Fill = Brushes.AliceBlue;
        }
    }
}
