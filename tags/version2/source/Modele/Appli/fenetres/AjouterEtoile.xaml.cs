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
using Appli.convertisseurs;

namespace Appli.fenetres
{
    /// <summary>
    /// Logique d'interaction pour AjouterEtoile.xaml
    /// </summary>
    public partial class AjouterEtoile : Window
    {
        private string etoileImg;

        public Manager LeManager => (App.Current as App).LeManager;

        public Etoile LEtoile { get; internal set; }

        public bool Modification { get; internal set; }

        public AjouterEtoile()
        {
            InitializeComponent();
            var etoile = new Etoile("", "", 0, 0, 0, "", 0, TypeEtoile.NaineBlanche, true, "étoile.jpg");
            LEtoile = new Etoile(etoile.Nom, etoile.Description, etoile.Age, etoile.Masse, etoile.Temperature, etoile.Constellation,
                etoile.Luminosite, etoile.Type, etoile.Personnalise, etoile.Image);
            DataContext = this;
        }

        private void Valider(object sender, RoutedEventArgs e)
        {
            if (LeManager.RecupererAstre(LEtoile.Nom) != null && !Modification)
            {
                MessageBox.Show("Un astre avec ce nom existe déjà dans l'application, veuillez choisir un nom différent.",
                                "Attention, ce nom est déjà utilisé !",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LEtoile.Description = LEtoile.Description.Replace("\r\n", "");
            Close();
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

        private void Fermer(object sender, RoutedEventArgs e) => Fermer(null, null);

        private void Deplacer(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
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
            if(!File.Exists(System.IO.Path.Combine(ConvertisseurDeTexteEnImage.cheminImages, fileName)))
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
