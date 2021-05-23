using Espace;
using Modele;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Appli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool triOrdreAlphabetique = false;
        private byte filtrePersonnalisation = 3;
        private Type filtreType = typeof(Astre);
        private bool filtreFavoris = false;
        private string filtreNom;


        private bool toggleSpec = false;

        public Manager Manager => (Application.Current as App).LeManager;
        public MainWindow()
        { 
            InitializeComponent();
            DataContext = Manager;
            SpecBouton.Path = "M505.918,236.117c-26.651-43.587-62.485-78.609-107.497-105.065c-45.015-26.457-92.549-39.687-142.608-39.687 c-50.059,0-97.595,13.225-142.61,39.687C68.187,157.508,32.355,192.53,5.708,236.117C1.903,242.778,0,249.345,0,255.818 c0,6.473,1.903,13.04,5.708,19.699c26.647,43.589,62.479,78.614,107.495,105.064c45.015,26.46,92.551,39.68,142.61,39.68 c50.06,0,97.594-13.176,142.608-39.536c45.012-26.361,80.852-61.432,107.497-105.208c3.806-6.659,5.708-13.223,5.708-19.699 C511.626,249.345,509.724,242.778,505.918,236.117z M194.568,158.03c17.034-17.034,37.447-25.554,61.242-25.554 c3.805,0,7.043,1.336,9.709,3.999c2.662,2.664,4,5.901,4,9.707c0,3.809-1.338,7.044-3.994,9.704 c-2.662,2.667-5.902,3.999-9.708,3.999c-16.368,0-30.362,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971 c0,3.811-1.336,7.044-3.999,9.71c-2.667,2.668-5.901,3.999-9.707,3.999c-3.809,0-7.044-1.334-9.71-3.999 c-2.667-2.666-3.999-5.903-3.999-9.71C169.015,195.482,177.535,175.065,194.568,158.03z M379.867,349.04 c-38.164,23.12-79.514,34.687-124.054,34.687c-44.539,0-85.889-11.56-124.051-34.687s-69.901-54.2-95.215-93.222 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.207-17.417,64.236c0,35.216,12.517,65.329,37.544,90.362 s55.151,37.544,90.362,37.544c35.214,0,65.329-12.518,90.362-37.544s37.545-55.146,37.545-90.362 c0-23.029-5.808-44.447-17.419-64.236c43.585,22.265,79.846,55.865,108.776,100.783C449.767,294.84,418.031,325.913,379.867,349.04 z";
        }

        private void popupClicMenu(object sender, MouseButtonEventArgs e)
        {
            Popup.Visibility = Visibility.Visible; //(Application.Current.MainWindow as MainWindow).
            
        }

        private void ModifierFavori(object sender, RoutedEventArgs e)
        {
            Button bouton = sender as Button;
            Astre astre = bouton.DataContext as Astre;
            astre.ModifierFavori();
        }

        /// <summary>
        /// Permet de cacher le master lorque le bouton spectateur est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecClic(object sender, MouseButtonEventArgs e)
        {
            if (toggleSpec)
            {
                SpecBouton.Path = "M505.918,236.117c-26.651-43.587-62.485-78.609-107.497-105.065c-45.015-26.457-92.549-39.687-142.608-39.687 c-50.059,0-97.595,13.225-142.61,39.687C68.187,157.508,32.355,192.53,5.708,236.117C1.903,242.778,0,249.345,0,255.818 c0,6.473,1.903,13.04,5.708,19.699c26.647,43.589,62.479,78.614,107.495,105.064c45.015,26.46,92.551,39.68,142.61,39.68 c50.06,0,97.594-13.176,142.608-39.536c45.012-26.361,80.852-61.432,107.497-105.208c3.806-6.659,5.708-13.223,5.708-19.699 C511.626,249.345,509.724,242.778,505.918,236.117z M194.568,158.03c17.034-17.034,37.447-25.554,61.242-25.554 c3.805,0,7.043,1.336,9.709,3.999c2.662,2.664,4,5.901,4,9.707c0,3.809-1.338,7.044-3.994,9.704 c-2.662,2.667-5.902,3.999-9.708,3.999c-16.368,0-30.362,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971 c0,3.811-1.336,7.044-3.999,9.71c-2.667,2.668-5.901,3.999-9.707,3.999c-3.809,0-7.044-1.334-9.71-3.999 c-2.667-2.666-3.999-5.903-3.999-9.71C169.015,195.482,177.535,175.065,194.568,158.03z M379.867,349.04 c-38.164,23.12-79.514,34.687-124.054,34.687c-44.539,0-85.889-11.56-124.051-34.687s-69.901-54.2-95.215-93.222 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.207-17.417,64.236c0,35.216,12.517,65.329,37.544,90.362 s55.151,37.544,90.362,37.544c35.214,0,65.329-12.518,90.362-37.544s37.545-55.146,37.545-90.362 c0-23.029-5.808-44.447-17.419-64.236c43.585,22.265,79.846,55.865,108.776,100.783C449.767,294.84,418.031,325.913,379.867,349.04 z";
                MasterGrid.Width = new GridLength(4, GridUnitType.Star);
                MasterGrid.MinWidth = 200;
                //SpecBouton.Height = 25;
                toggleSpec = false;
            } 
            else
            {

                SpecBouton.Path = "M372.872,94.221c0.191-0.378,0.28-1.235,0.28-2.568c0-3.237-1.522-5.802-4.571-7.715c-0.568-0.38-2.423-1.475-5.568-3.287 c-3.138-1.805-6.14-3.567-8.989-5.282c-2.854-1.713-5.989-3.472-9.422-5.28c-3.426-1.809-6.375-3.284-8.846-4.427 c-2.479-1.141-4.189-1.713-5.141-1.713c-3.426,0-6.092,1.525-7.994,4.569l-15.413,27.696c-17.316-3.234-34.451-4.854-51.391-4.854 c-51.201,0-98.404,12.946-141.613,38.831C70.998,156.08,34.836,191.385,5.711,236.114C1.903,242.019,0,248.586,0,255.819 c0,7.231,1.903,13.801,5.711,19.698c16.748,26.073,36.592,49.396,59.528,69.949c22.936,20.561,48.011,37.018,75.229,49.396 c-8.375,14.273-12.562,22.556-12.562,24.842c0,3.425,1.524,6.088,4.57,7.99c23.219,13.329,35.97,19.985,38.256,19.985 c3.422,0,6.089-1.529,7.992-4.575l13.99-25.406c20.177-35.967,50.248-89.931,90.222-161.878 C322.908,183.871,352.886,130.005,372.872,94.221z M158.456,362.885C108.97,340.616,68.33,304.93,36.547,255.822 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.206-17.417,64.237c0,20.365,4.661,39.68,13.99,57.955 c9.327,18.274,22.27,33.4,38.83,45.392L158.456,362.885z M265.525,155.887c-2.662,2.667-5.906,3.999-9.712,3.999 c-16.368,0-30.361,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971c0,3.811-1.336,7.044-3.999,9.71 c-2.668,2.667-5.902,3.999-9.707,3.999c-3.809,0-7.045-1.334-9.71-3.999c-2.667-2.666-3.999-5.903-3.999-9.71 c0-23.79,8.52-44.206,25.553-61.242c17.034-17.034,37.447-25.553,61.241-25.553c3.806,0,7.043,1.336,9.713,3.999 c2.662,2.664,3.996,5.901,3.996,9.707C269.515,149.992,268.181,153.228,265.525,155.887z M361.161,291.652c15.037-21.796,22.56-45.922,22.56-72.375c0-7.422-0.76-15.417-2.286-23.984l-79.938,143.321 C326.235,329.101,346.125,313.438,361.161,291.652z M505.916,236.114c-10.853-18.08-24.603-35.594-41.255-52.534c-16.646-16.939-34.022-31.496-52.105-43.68l-17.987,31.977 c31.785,21.888,58.625,49.87,80.51,83.939c-23.024,35.782-51.723,65-86.07,87.648c-34.358,22.661-71.712,35.693-112.065,39.115 l-21.129,37.688c42.257,0,82.18-9.038,119.769-27.121c37.59-18.076,70.668-43.488,99.216-76.225 c13.322-15.421,23.695-29.219,31.121-41.401c3.806-6.476,5.708-13.046,5.708-19.702 C511.626,249.157,509.724,242.59,505.916,236.114z";
                MasterGrid.Width = new GridLength(0);
                MasterGrid.MinWidth = 0;
                //SpecBouton.Height = 30;
                toggleSpec = true;
            }
        }

        private void PoubelleClic(object sender, MouseButtonEventArgs e)
        {
           Manager.SupprimerTout();
        }
        
        private void FaireLaRecherche()
        {
            Manager.Filtrage(filtreFavoris, filtrePersonnalisation, filtreType, triOrdreAlphabetique, filtreNom);
        }

        private void ClicSourisCanvas(object sender, MouseButtonEventArgs e)
        {
            var point = new Geometrie.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);
            Debug.WriteLine(point);
        }


        private void BoutonTriAlphabetique(object sender, RoutedEventArgs e)
        {
            if (triOrdreAlphabetique)
            {
                triOrdreAlphabetique = false;
                Bouton1.Source = new BitmapImage(new Uri("/images/icones/ordreAlphabetiqueCroissant.png", UriKind.Relative));
            }
            else
            {
                triOrdreAlphabetique = true;
                Bouton1.Source = new BitmapImage(new Uri("/images/icones/ordreAlphabetiqueDecroissant.png", UriKind.Relative));
            }
            FaireLaRecherche();
        }

        private void BoutonFiltreFavori(object sender, RoutedEventArgs e)
        {
            if (filtreFavoris)
            {
                filtreFavoris = false;
                Bouton2.Source = new BitmapImage(new Uri("/images/icones/coeurVide.png", UriKind.Relative));
            }
            else
            {
                filtreFavoris = true;
                Bouton2.Source = new BitmapImage(new Uri("/images/icones/coeurPlein.png", UriKind.Relative));

            }
            FaireLaRecherche();
        }

        private void BoutonFiltrePersonnalisation(object sender, RoutedEventArgs e)
        {
            if (filtrePersonnalisation == 1)
            {
                filtrePersonnalisation = 3;
                Bouton3.Text = "Tous les astres";
            }
            else if (filtrePersonnalisation == 2)
            {
                filtrePersonnalisation = 1;
                Bouton3.Text = "Mes créations uniquement";
            }
            else
            {
                filtrePersonnalisation = 2;
                Bouton3.Text = "Astres existants uniquement";
            }
            FaireLaRecherche();
        }

        private void BoutonFiltreType(object sender, RoutedEventArgs e)
        {
            if (filtreType == typeof(Etoile))
            {
                filtreType = typeof(Planete);
                Bouton4.Source = new BitmapImage(new Uri("/images/icones/planete.png", UriKind.Relative));
            }
            else if (filtreType == typeof(Planete))
            {
                filtreType = typeof(Astre);
                Bouton4.Source = new BitmapImage(new Uri("/images/icones/tousLesAstres.png", UriKind.Relative));
            }
            else
            {
                filtreType = typeof(Etoile);
                Bouton4.Source = new BitmapImage(new Uri("/images/icones/etoile.png", UriKind.Relative));
            }
            FaireLaRecherche();
        }
    }
}
