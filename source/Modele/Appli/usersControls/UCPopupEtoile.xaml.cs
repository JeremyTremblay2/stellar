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
using Modele;
using Espace;
using Geometrie;
using System.Text.RegularExpressions;

namespace Appli.usersControls
{
    /// <summary>
    /// Logique d'interaction pour UCPopupEtoile.xaml
    /// </summary>
    public partial class UCPopupEtoile : UserControl
    {
        public UCPopupEtoile()
        {
            InitializeComponent();
            (Application.Current.MainWindow as MainWindow).PopupEtoile.Visibility = Visibility.Hidden;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).PopupEtoile.Visibility = Visibility.Hidden;
        }

        private void buttonPopupClicCroix(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).PopupEtoile.Visibility = Visibility.Hidden;
        }

        private void buttonPopupDessusCroix(object sender, MouseEventArgs e)
        {
            QuitterPopup.Fill = Brushes.Gray;
        }

        private void buttonPopupHorsCroix(object sender, MouseEventArgs e)
        {
            QuitterPopup.Fill = Brushes.AliceBlue;
        }

        private void Valider(object sender, RoutedEventArgs e)
        {
            var main = (Application.Current.MainWindow as MainWindow);
            main.Manager.AjouterUnAstre(main.ClicCanvas, new FabriqueDEtoile().Initialiser(Nom.Text)
                                      .AvecDescription(Description.Text)
                                      .AvecAge(int.Parse(Age.Text))
                                      .AvecMasse(float.Parse(Masse.Text))
                                      .AvecTemperature(int.Parse(Temperature.Text))
                                      .AvecLuminosite(float.Parse(Luminosite.Text))
                                      .EstDansLaConstellation(Constellation.Text)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      //.AvecImage("sirius.jpeg")
                                      .Construire());
            main.PopupEtoile.Visibility = Visibility.Hidden;
        }

    }
}
