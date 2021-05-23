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
    /// Logique d'interaction pour UCPathButton.xaml
    /// </summary>
    public partial class UCPathButton : UserControl
    {
        public UCPathButton()
        {
            InitializeComponent();
        }
        private void SpecEnter(object sender, MouseEventArgs e)
        {
            PathButton.Fill = Brushes.Gray;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void SpecLeave(object sender, MouseEventArgs e)
        {
            PathButton.Fill = Brushes.AliceBlue;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public string Path
        {
            set
            {
                PathButton.Data = Geometry.Parse(value);
            }
        }

        public string Fill
        {
            set
            {
                PathButton.Fill = new SolidColorBrush((Color) ColorConverter.ConvertFromString(value));
            }
        }

    }
}
