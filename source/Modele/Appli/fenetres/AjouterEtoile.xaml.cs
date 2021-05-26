using Espace;
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
using System.Windows.Shapes;

namespace Appli.fenetres
{
    /// <summary>
    /// Logique d'interaction pour AjouterEtoile.xaml
    /// </summary>
    public partial class AjouterEtoile : Window
    {

        public Manager LeManager => (App.Current as App).LeManager;

        public Etoile LEtoile { get; set; }

        public AjouterEtoile()
        {
            InitializeComponent();
            var etoile = new Etoile("Mon Etoile", "", 4500000000, 1, 1000, "", 1, TypeEtoile.NaineBlanche, true, "étoile.jpg");
            LEtoile = new Etoile(etoile.Nom, etoile.Description, etoile.Age, etoile.Masse, etoile.Temperature, etoile.Constellation,
                etoile.Luminosite, etoile.Type, etoile.Personnalise, etoile.Image);
            DataContext = this;
        }

        /*private void buttonPopupClicCroix(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).PopupEtoile.Visibility = Visibility.Hidden;
        }*/

        private void Valider(object sender, RoutedEventArgs e)
        {
            if (LeManager.RecupererAstre(LEtoile.Nom) != null)
            {
                MessageBox.Show("Un astre avec ce nom existe déjà dans l'application, veuillez choisir un nom différent.",
                                "Attention, ce nom est déjà utilisé !",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }

            /*main.Manager.AjouterUnAstre(main.ClicCanvas, new FabriqueDEtoile().Initialiser(Nom.Text)
                                      .AvecDescription(Description.Text)
                                      .AvecAge(int.Parse(Age.Text))
                                      .AvecMasse(float.Parse(Masse.Text))
                                      .AvecTemperature(int.Parse(Temperature.Text))
                                      .AvecLuminosite(float.Parse(Luminosite.Text))
                                      .EstDansLaConstellation(Constellation.Text)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      //.AvecImage("sirius.jpeg")
                                      .Construire());*/

            LeManager.AjouterUnAstre(LEtoile);
            //PopupEtoile.Visibility = Visibility.Hidden;
            Close();
        }

        private void Fermer(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Deplacer(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
