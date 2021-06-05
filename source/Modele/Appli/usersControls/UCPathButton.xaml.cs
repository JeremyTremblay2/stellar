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

        /// <summary>
        /// Quelques effets visuels au survol de la souris...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecEnter(object sender, MouseEventArgs e)
        {
            PathButton.Opacity = 0.6;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void SpecLeave(object sender, MouseEventArgs e)
        {
            PathButton.Opacity = 1;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>
        /// Mutateur sur le contenu même du "chemin" qui va pouvoir représenter une image.
        /// </summary>
        public string Path
        {
            set
            {
                PathButton.Data = Geometry.Parse(value);
            }
        }

        /// <summary>
        /// Mutateur sur la couleur de l'image. PErmet de mieux faire ressortir les boutons.
        /// </summary>
        public string Fill
        {
            set
            {
                PathButton.Fill = new SolidColorBrush((Color) ColorConverter.ConvertFromString(value));
            }
        }

    }
}
