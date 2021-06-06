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
    /// Permet la création d'étoiles, et également leur modification.
    /// </summary>
    public partial class AjouterEtoile : Window
    {
        //L'image de l'étoile.
        private string etoileImg;

        public Manager LeManager => (App.Current as App).LeManager;

        // <summary>
        /// La copie de notre étoile.
        /// </summary>
        public Etoile LEtoile { get; internal set; }

        /// <summary>
        /// La version de notre étoile qui sera directement modifiée dans la vue et ici.
        /// </summary>
        public Etoile LEtoileEditable { get; internal set; }

        /// <summary>
        /// Lors de la modification de l'étoile, ce booléen est mit à faux pour indiquer que l'on fait une modification.
        /// </summary>
        public bool EstEnCoursDeCreation { get; internal set; } = true;

        /// <summary>
        /// Constructeur de notre fenêtre.
        /// Pose le DataContexte comme étant la fenêtre elle-même.
        /// Instancie deux propriétés : LEtoile qui est juste une "copie", et LEtoileEditable qui est la version qui sera modifiée 
        /// directement dans la fenêtre.
        /// </summary>
        public AjouterEtoile()
        {
            InitializeComponent();
            var etoile = new Etoile("", "", 0, 0, 0, "", 0, TypeEtoile.NaineBlanche, true, "étoile.jpg");
            LEtoile = new Etoile(etoile.Nom, etoile.Description, etoile.Age, etoile.Masse, etoile.Temperature, etoile.Constellation,
                etoile.Luminosite, etoile.Type, etoile.Personnalise, etoile.Image);
            LEtoileEditable = new Etoile(etoile.Nom, etoile.Description, etoile.Age, etoile.Masse, etoile.Temperature, etoile.Constellation,
                 etoile.Luminosite, etoile.Type, etoile.Personnalise, etoile.Image);
            DataContext = this;
        }

        /// <summary>
        /// Cette méthode est appelée lors de la validation.
        /// Elle permet de valider ou d'invalider la création ou la modification de l'étoile.
        /// Si le nom entrée existe déjà dans l'application, on affiche un message d'erreur et on empêche la validation.
        /// Sinon, on supprime les éventuels sauts de ligne de la description et on ferme cette fenêtre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider(object sender, RoutedEventArgs e)
        {
            if (LeManager.RecupererAstre(LEtoileEditable.Nom) != null && EstEnCoursDeCreation)
            {
                MessageBox.Show("Un astre avec ce nom existe déjà dans l'application, veuillez choisir un nom différent.",
                                "Attention, ce nom est déjà utilisé !",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LEtoileEditable.Description = LEtoileEditable.Description.Replace("\r\n", " ");
            Close();
        }

        /// <summary>
        /// Permet de n'autoriser que des nombres dans certaines entrées utilisateur. Evite ainsi des erreurs de liaison.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeulementNombre(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Ces méthodes servent à fermer la fenêtre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fermer(object sender, MouseButtonEventArgs e)
        {
            LEtoileEditable = null;
            Close();
        }

        private void Fermer(object sender, RoutedEventArgs e) => Fermer(null, null);

        /// <summary>
        /// Permet de déplacer la fenêtre popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deplacer(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        /// <summary>
        /// Quelques effets visuels lors du survol des boutons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SurvolBouton(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void FinSurvolBouton(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>
        /// Méthode permettant l'ouverture d'une image.
        /// Elle est appelée lors du clic de l'utilisateur sur l'image de l'étoile.
        /// Elle ouvre un explorateur qui permet à l'utilisateur de sélectionner une image.
        /// L'image est ensuite enregistrée et pourra donc être affichée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuvrirImage(object sender, RoutedEventArgs e)
        {
            //Ouverture de l'explorateur Windows.
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Les divers encodages en fonction de l'extension.
            Dictionary<string, BitmapEncoder> encoders = new Dictionary<string, BitmapEncoder>()
                                                    {
                                                        {".jpg", new JpegBitmapEncoder()},
                                                        {".bmp", new BmpBitmapEncoder()},
                                                        {".png", new PngBitmapEncoder()}
                                                    };
            //Les fomats autorisés. Par défaut, le dossier "Images" sera utilisé s'il existe.
            dlg.FileName = "Images";
            dlg.DefaultExt = ".jpg | .png | .gif"; 
            dlg.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            //Si une image a été sélectionnée.
            if (result == true)
            {
                string filename = dlg.FileName;
                
                etoileImg = dlg.SafeFileName.ToString();

                Image.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                FileInfo fi = new FileInfo(filename);

                //L'encodage doit exister dans le dictionnaire précédent.
                if (!encoders.ContainsKey(fi.Extension))
                {
                    return;
                }

                //On appelle la méthode de sauvegarde.
                SauvegardeImage(new BitmapImage(new Uri(filename, UriKind.Absolute)), etoileImg, encoders[fi.Extension]);

                //L'image de l'étoile est désormais le nom du fichier.
                LEtoileEditable.Image = etoileImg;
            }
        }

        /// <summary>
        /// Méthode permettant la sauvegarde d'une image sélectionnée par l'utilisateur.
        /// S'assure que l'image n'existe pas déjà puis l'enregistre dans le dossier des images correspondant.
        /// </summary>
        /// <param name="img">L'image à sauvegarder.</param>
        /// <param name="fileName">Le nom de l'image sous forme d'une chaîne de caractères.</param>
        /// <param name="encoder">Un type d'encodage.</param>
        void SauvegardeImage(BitmapImage img, string fileName, BitmapEncoder encoder)
        {
            if (!File.Exists(System.IO.Path.Combine(ConvertisseurDeTexteEnImage.cheminImages, fileName)))
            {
                FileInfo fi = new FileInfo(fileName);

                fileName = @".\images\astres\" + fileName;

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
