using Appli.convertisseurs;
using Espace;
using Modele;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool Modification { get; internal set; }

        private string planeteImg;
        public AjouterPlanete()
        {
            InitializeComponent();
            Vie.IsEnabled = false;
            var planete = new Planete("Ma Planète", "", 4500000000, 1, 1000, "", false, "Solaire", TypePlanete.Tellurique, true, "planète.jpg");
            LaPlanete = new Planete(planete.Nom, planete.Description, planete.Age, planete.Masse, planete.Temperature, planete.Vie,
                planete.EauPresente, planete.Systeme, planete.Type, planete.Personnalise , planeteImg);
            DataContext = this;
        }

        private void Fermer(object sender, MouseButtonEventArgs e)
        {
            LaPlanete = null;
            Close();
        }
        private void Fermer(object sender, RoutedEventArgs e) => Fermer(null, null);

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
        private void SeulementNombre(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Deplacer(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void CheckVie_Checked(object sender, RoutedEventArgs e)
        {
            Vie.IsEnabled = true;
            Vie.Background = Brushes.White;
        }
        private void CheckVie_Unchecked(object sender, RoutedEventArgs e)
        {
            Vie.IsEnabled = false;
            Vie.Background = Brushes.DarkGray;
        }
        private void OuvrirImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Dictionary<string, BitmapEncoder> encoders = new Dictionary<string, BitmapEncoder>()
                                                    {
                                                        {".jpg", new JpegBitmapEncoder()},
                                                        {".bmp", new BmpBitmapEncoder()},
                                                        {".png", new PngBitmapEncoder()}
                                                    };

            dlg.FileName = "Images";
            dlg.DefaultExt = ".jpg | .png | .gif";
            dlg.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                planeteImg = dlg.SafeFileName.ToString();

                Image.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                FileInfo fi = new FileInfo(filename);
                if (!encoders.ContainsKey(fi.Extension))
                {
                    return;
                }



                SaveImage(new BitmapImage(new Uri(filename, UriKind.Absolute)), planeteImg, encoders[fi.Extension]);

                LaPlanete.Image = planeteImg;

            }
        }

        void SaveImage(BitmapImage img, string fileName, BitmapEncoder encoder)
        {
            if (!File.Exists(System.IO.Path.Combine(ConvertisseurDeTexteEnImage.cheminImages, fileName)))
            {
                FileInfo fi = new FileInfo(fileName);

                /*int i = 0;

                while (File.Exists(System.IO.Path.Combine(ConvertisseurDeTexteEnImage.cheminImages, fileName)))
                {
                    fileName = $"{fi.Name.Remove(fi.Name.LastIndexOf('.'))}_{i}{fi.Extension}";
                    i++;
                }*/

                fileName = @"..\..\StellarBin\images\" + fileName;

                BitmapFrame frame = BitmapFrame.Create(img);
                encoder.Frames.Add(frame);

                using (var stream = File.Create(fileName))
                {
                    encoder.Save(stream);
                }
            }
        }
    }
}
