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
    /// Logique d'interaction pour AjouterPlanete.xaml
    /// </summary>
    public partial class AjouterPlanete : Window
    {
        private const int longueurMaxNom = 18;
        private const int longueurMaxSysteme = 20;
        private const int longueurMaxDescription = 200;
        public Manager LeManager => (App.Current as App).LeManager;

        public Planete LaPlanete { get; set; }
        public AjouterPlanete()
        {
            InitializeComponent();
            Vie.IsEnabled = false;
            var planete = new Planete("Ma Planète", "", 4500000000, 1, 1000, "", false, "Solaire", TypePlanete.Tellurique, true, "planète.jpg");
            LaPlanete = new Planete(planete.Nom, planete.Description, planete.Age, planete.Masse, planete.Temperature, planete.Vie,
                planete.EauPresente, planete.Systeme, planete.Type, planete.Personnalise , planete.Image);
            DataContext = this;
        }

        private void Fermer(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void Fermer(object sender, RoutedEventArgs e)
        {
            Fermer(null, null);
        }
        private void Valider(object sender, RoutedEventArgs e)
        {
            if (LeManager.RecupererAstre(LaPlanete.Nom) != null)
            {
                MessageBox.Show("Un astre avec ce nom existe déjà dans l'application, veuillez choisir un nom différent.",
                                "Attention, ce nom est déjà utilisé !",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LaPlanete.Nom.Length > longueurMaxNom)
            {
                MessageBox.Show($"Le nom de cette étoile est trop long, il doit faire au maximum {longueurMaxNom} caractères.",
                    "Nom trop long",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LaPlanete.Description.Length > longueurMaxDescription)
            {
                MessageBox.Show($"La description de cette étoile est trop longue, elle doit faire au maximum " +
                    $"{longueurMaxDescription} caractères.",
                    "Description trop longue",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LaPlanete.Systeme.Length > longueurMaxSysteme)
            {
                MessageBox.Show($"La constellation de cette étoile possède un nom trop long, elle doit faire au maximum " +
                    $"{longueurMaxSysteme} caractères.",
                    "Constellation trop longue",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace + "\nVérifiez les données puis réessayez.",
                                "Une erreur est survenue dans l'ajout",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Deplacer(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void CheckVie_Checked(object sender, RoutedEventArgs e)
        {
            Vie.IsEnabled = true;
            
        }
        private void CheckVie_Unchecked(object sender, RoutedEventArgs e)
        {
            Vie.IsEnabled = false;
        }
    }
  
}
