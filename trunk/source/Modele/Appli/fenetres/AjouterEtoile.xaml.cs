using Espace;
using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

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
        private string etoileImg;

        public AjouterEtoile()
        {
            InitializeComponent();
            var etoile = new Etoile("Mon Etoile", "", 4500000000, 1, 1000, "", 1, TypeEtoile.TrouNoir, true, "étoile.jpg");
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
            if (LEtoile.Nom.Length > longueurMaxNom || string.IsNullOrWhiteSpace(LEtoile.Nom))
            {
                MessageBox.Show($"Le nom de cette étoile doit faire au maximum {longueurMaxNom} caractères, mais ne peut pas " +
                    $"non plus être vide.",
                    "Nom incorrect",
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
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace + "\nVérifiez les données puis réessayez.",
                                "Une erreur est survenue dans l'ajout",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SeulementNombre(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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

        static Dictionary<string, BitmapEncoder> encoders = new Dictionary<string, BitmapEncoder>()
                                                    {
                                                        {".jpg", new JpegBitmapEncoder()},
                                                        {".bmp", new BmpBitmapEncoder()},
                                                        {".png", new PngBitmapEncoder()}
                                                    };
        private void OuvrirImage(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.InitialDirectory = "C:\\Users\\Public\\Pictures\\Sample Pictures";
            //dlg.FileName = "Images"; // Default file name
            dlg.DefaultExt = ".jpg | .png | .gif"; // Default file extension
            dlg.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif"; // Filter files by extension 

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                
                etoileImg = dlg.SafeFileName.ToString();

                Image.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                FileInfo fi = new FileInfo(filename);
                if (!encoders.ContainsKey(fi.Extension))
                {
                    return;
                }
                SaveImage(new BitmapImage(new Uri(filename, UriKind.Absolute)), dlg.SafeFileName.ToString(), encoders[fi.Extension]);
                LEtoile.Image = etoileImg;
            }
        }

        void SaveImage(BitmapImage img, string fileName, BitmapEncoder encoder)
        {
            fileName = @"..\..\StellarBin\images\" + fileName; //..\\StellarBin\\images\\
            
            BitmapFrame frame = BitmapFrame.Create(img);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }
    }
}
