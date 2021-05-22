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
    /// Logique d'interaction pour UCCarte.xaml
    /// </summary>
    public partial class UCCarte : UserControl
    {
        public UCCarte()
        {
            InitializeComponent();
            SpectateurOn.Visibility = Visibility.Hidden;
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
            }
            else
            {
                SpectateurOff.Visibility = Visibility.Visible;
                SpectateurOn.Visibility = Visibility.Hidden;
            }
        }
    }
}
