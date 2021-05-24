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
    /// Logique d'interaction pour UCImageAstre.xaml
    /// </summary>
    public partial class UCImageAstre : UserControl
    {
        public UCImageAstre()
        {
            InitializeComponent();
        }

        public int Largeur
        {
            set
            {
                Dimensions.Width = value;
            }
        }

        public int Hauteur
        {
            set
            {
                Dimensions.Height = value;
            }
        }

        public string ImageAstre
        {
            get { return (string)GetValue(ImageAstrePropriete); }
            set { SetValue(ImageAstrePropriete, value); }
        }

        public static readonly DependencyProperty ImageAstrePropriete =
            DependencyProperty.Register("ImageAstre", typeof(string), typeof(UCImageAstre), new PropertyMetadata("astre.jpg"));
    }
}
