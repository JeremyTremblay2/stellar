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
        private const int longueurMaxNom = 18;
        private const int longueurMaxConstellation = 20;
        private const int longueurMaxDescription = 200;

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

        private void Valider(object sender, RoutedEventArgs e)
        {
            if (LeManager.RecupererAstre(LEtoile.Nom) != null)
            {
                MessageBox.Show("Un astre avec ce nom existe déjà dans l'application, veuillez choisir un nom différent.",
                                "Attention, ce nom est déjà utilisé !",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LEtoile.Nom.Length > longueurMaxNom)
            {
                MessageBox.Show($"Le nom de cette étoile est trop long, il doit faire au maximum {longueurMaxNom} caractères.",
                    "Nom trop long",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LEtoile.Description.Length > longueurMaxDescription)
            {
                MessageBox.Show($"La description de cette étoile est trop longue, elle doit faire au maximum " +
                    $"{longueurMaxDescription} caractères.",
                    "Description trop longue",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LEtoile.Constellation.Length > longueurMaxConstellation)
            {
                MessageBox.Show($"La constellation de cette étoile possède un nom trop long, elle doit faire au maximum " +
                    $"{longueurMaxConstellation} caractères.",
                    "Constellation trop longue",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                /*LeManager.AjouterUnAstre(ClicCanvas, new FabriqueDEtoile().Initialiser(Nom)
                                      .AvecDescription(Description)
                                      .AvecAge(int.Parse(Age))
                                      .AvecMasse(float.Parse(Masse))
                                      .AvecTemperature(int.Parse(Temperature))
                                      .AvecLuminosite(float.Parse(Luminosite))
                                      .EstDansLaConstellation(Constellation)
                                      .AvecType(TypeEtoile.NaineJaune)
                                      //.AvecImage("sirius.jpeg")
                                      .Construire());*/
                Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace + "\nVérifiez les données puis réessayez.",
                                "Une erreur est survenue dans l'ajout",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fermer(object sender, MouseButtonEventArgs e)
        {
            LEtoile = null;
            Close();
        }

        private void Fermer(object sender, RoutedEventArgs e)
        {
            Fermer(null, null);
        }

        private void Deplacer(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
