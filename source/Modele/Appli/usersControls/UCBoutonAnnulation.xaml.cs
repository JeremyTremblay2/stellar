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
    /// Logique d'interaction pour UCBoutonAnnulation.xaml
    /// </summary>
    public partial class UCBoutonAnnulation : UserControl
    {
        public UCBoutonAnnulation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Quelques effets visuels au survol de la souris...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntreeSouris(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.6;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void SortieSouris(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
