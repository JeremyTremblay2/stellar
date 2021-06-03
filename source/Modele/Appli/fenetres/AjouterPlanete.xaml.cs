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
        private string planeteImg;

        public Manager LeManager => (App.Current as App).LeManager;

        public Planete LaPlanete { get; internal set; }

        public Planete LaPlaneteEditable { get; internal set; }

        public bool EstEnCoursDeCreation { get; internal set; } = true;

        public AjouterPlanete()
        {
            InitializeComponent();
            Vie.IsEnabled = false;
            var planete = new Planete("", "", 0, 0, 0, "dfgd", false, "", TypePlanete.Tellurique, true, "planète.jpg");
            LaPlanete = new Planete(planete.Nom, planete.Description, planete.Age, planete.Masse, planete.Temperature, planete.Vie,
                planete.EauPresente, planete.Systeme, planete.Type, planete.Personnalise, planete.Image);
            LaPlaneteEditable = new Planete(LaPlanete.Nom, LaPlanete.Description, LaPlanete.Age, LaPlanete.Masse, LaPlanete.Temperature, LaPlanete.Vie,
                LaPlanete.EauPresente, LaPlanete.Systeme, LaPlanete.Type, LaPlanete.Personnalise, LaPlanete.Image);
            DataContext = this;
        }

        private void Valider(object sender, RoutedEventArgs e)
        {
            if (LaPlaneteEditable.Nom != null && LeManager.RecupererAstre(LaPlaneteEditable.Nom) != null && EstEnCoursDeCreation)
            {
                MessageBox.Show("Un astre avec ce nom existe déjà dans l'application, veuillez choisir un nom différent.",
                                "Attention, ce nom est déjà utilisé !",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LaPlaneteEditable.Description = LaPlaneteEditable.Description.Replace("\r\n", " ");
            Close();
        }

        private void Fermer(object sender, MouseButtonEventArgs e)
        {
            LaPlaneteEditable = null;
            Close();
        }

        private void Fermer(object sender, RoutedEventArgs e) => Fermer(null, null);

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

                LaPlaneteEditable.Image = planeteImg;
            }
        }

        void SaveImage(BitmapImage img, string fileName, BitmapEncoder encoder)
        {
            if (!File.Exists(System.IO.Path.Combine(ConvertisseurDeTexteEnImage.cheminImages, fileName)))
            {
                FileInfo fi = new FileInfo(fileName);

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
