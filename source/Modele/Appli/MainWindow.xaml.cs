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


        private bool modeSpectateurActive = false;
        private Dictionary<string, bool> boutonsCarteActifs = new Dictionary<string, bool>()
        {
            ["ajouterEtoile"] = false,
            ["ajouterPlanete"] = false,
            ["relier"] = false,
            ["effacer"] = false,
            ["deplacer"] = false,
        };
        private List<Geometrie.Point> lesPointsCliques = new List<Geometrie.Point>();


        public Manager Manager => (Application.Current as App).LeManager;

        public MainWindow()
        { 
            InitializeComponent();
            DataContext = Manager;
            modeSpectateur.Path = "M505.918,236.117c-26.651-43.587-62.485-78.609-107.497-105.065c-45.015-26.457-92.549-39.687-142.608-39.687 c-50.059,0-97.595,13.225-142.61,39.687C68.187,157.508,32.355,192.53,5.708,236.117C1.903,242.778,0,249.345,0,255.818 c0,6.473,1.903,13.04,5.708,19.699c26.647,43.589,62.479,78.614,107.495,105.064c45.015,26.46,92.551,39.68,142.61,39.68 c50.06,0,97.594-13.176,142.608-39.536c45.012-26.361,80.852-61.432,107.497-105.208c3.806-6.659,5.708-13.223,5.708-19.699 C511.626,249.345,509.724,242.778,505.918,236.117z M194.568,158.03c17.034-17.034,37.447-25.554,61.242-25.554 c3.805,0,7.043,1.336,9.709,3.999c2.662,2.664,4,5.901,4,9.707c0,3.809-1.338,7.044-3.994,9.704 c-2.662,2.667-5.902,3.999-9.708,3.999c-16.368,0-30.362,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971 c0,3.811-1.336,7.044-3.999,9.71c-2.667,2.668-5.901,3.999-9.707,3.999c-3.809,0-7.044-1.334-9.71-3.999 c-2.667-2.666-3.999-5.903-3.999-9.71C169.015,195.482,177.535,175.065,194.568,158.03z M379.867,349.04 c-38.164,23.12-79.514,34.687-124.054,34.687c-44.539,0-85.889-11.56-124.051-34.687s-69.901-54.2-95.215-93.222 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.207-17.417,64.236c0,35.216,12.517,65.329,37.544,90.362 s55.151,37.544,90.362,37.544c35.214,0,65.329-12.518,90.362-37.544s37.545-55.146,37.545-90.362 c0-23.029-5.808-44.447-17.419-64.236c43.585,22.265,79.846,55.865,108.776,100.783C449.767,294.84,418.031,325.913,379.867,349.04 z";
        }

        /// <summary>
        /// Permet de cacher le master lorque le bouton spectateur est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpectateurClic(object sender, MouseButtonEventArgs e)
        {
            if (modeSpectateurActive)
            {
                modeSpectateur.Path = "M505.918,236.117c-26.651-43.587-62.485-78.609-107.497-105.065c-45.015-26.457-92.549-39.687-142.608-39.687 c-50.059,0-97.595,13.225-142.61,39.687C68.187,157.508,32.355,192.53,5.708,236.117C1.903,242.778,0,249.345,0,255.818 c0,6.473,1.903,13.04,5.708,19.699c26.647,43.589,62.479,78.614,107.495,105.064c45.015,26.46,92.551,39.68,142.61,39.68 c50.06,0,97.594-13.176,142.608-39.536c45.012-26.361,80.852-61.432,107.497-105.208c3.806-6.659,5.708-13.223,5.708-19.699 C511.626,249.345,509.724,242.778,505.918,236.117z M194.568,158.03c17.034-17.034,37.447-25.554,61.242-25.554 c3.805,0,7.043,1.336,9.709,3.999c2.662,2.664,4,5.901,4,9.707c0,3.809-1.338,7.044-3.994,9.704 c-2.662,2.667-5.902,3.999-9.708,3.999c-16.368,0-30.362,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971 c0,3.811-1.336,7.044-3.999,9.71c-2.667,2.668-5.901,3.999-9.707,3.999c-3.809,0-7.044-1.334-9.71-3.999 c-2.667-2.666-3.999-5.903-3.999-9.71C169.015,195.482,177.535,175.065,194.568,158.03z M379.867,349.04 c-38.164,23.12-79.514,34.687-124.054,34.687c-44.539,0-85.889-11.56-124.051-34.687s-69.901-54.2-95.215-93.222 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.207-17.417,64.236c0,35.216,12.517,65.329,37.544,90.362 s55.151,37.544,90.362,37.544c35.214,0,65.329-12.518,90.362-37.544s37.545-55.146,37.545-90.362 c0-23.029-5.808-44.447-17.419-64.236c43.585,22.265,79.846,55.865,108.776,100.783C449.767,294.84,418.031,325.913,379.867,349.04 z";
                MasterGrid.Width = new GridLength(350);
                modeSpectateurActive = false;
            } 
            else
            {
                modeSpectateur.Path = "M372.872,94.221c0.191-0.378,0.28-1.235,0.28-2.568c0-3.237-1.522-5.802-4.571-7.715c-0.568-0.38-2.423-1.475-5.568-3.287 c-3.138-1.805-6.14-3.567-8.989-5.282c-2.854-1.713-5.989-3.472-9.422-5.28c-3.426-1.809-6.375-3.284-8.846-4.427 c-2.479-1.141-4.189-1.713-5.141-1.713c-3.426,0-6.092,1.525-7.994,4.569l-15.413,27.696c-17.316-3.234-34.451-4.854-51.391-4.854 c-51.201,0-98.404,12.946-141.613,38.831C70.998,156.08,34.836,191.385,5.711,236.114C1.903,242.019,0,248.586,0,255.819 c0,7.231,1.903,13.801,5.711,19.698c16.748,26.073,36.592,49.396,59.528,69.949c22.936,20.561,48.011,37.018,75.229,49.396 c-8.375,14.273-12.562,22.556-12.562,24.842c0,3.425,1.524,6.088,4.57,7.99c23.219,13.329,35.97,19.985,38.256,19.985 c3.422,0,6.089-1.529,7.992-4.575l13.99-25.406c20.177-35.967,50.248-89.931,90.222-161.878 C322.908,183.871,352.886,130.005,372.872,94.221z M158.456,362.885C108.97,340.616,68.33,304.93,36.547,255.822 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.206-17.417,64.237c0,20.365,4.661,39.68,13.99,57.955 c9.327,18.274,22.27,33.4,38.83,45.392L158.456,362.885z M265.525,155.887c-2.662,2.667-5.906,3.999-9.712,3.999 c-16.368,0-30.361,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971c0,3.811-1.336,7.044-3.999,9.71 c-2.668,2.667-5.902,3.999-9.707,3.999c-3.809,0-7.045-1.334-9.71-3.999c-2.667-2.666-3.999-5.903-3.999-9.71 c0-23.79,8.52-44.206,25.553-61.242c17.034-17.034,37.447-25.553,61.241-25.553c3.806,0,7.043,1.336,9.713,3.999 c2.662,2.664,3.996,5.901,3.996,9.707C269.515,149.992,268.181,153.228,265.525,155.887z M361.161,291.652c15.037-21.796,22.56-45.922,22.56-72.375c0-7.422-0.76-15.417-2.286-23.984l-79.938,143.321 C326.235,329.101,346.125,313.438,361.161,291.652z M505.916,236.114c-10.853-18.08-24.603-35.594-41.255-52.534c-16.646-16.939-34.022-31.496-52.105-43.68l-17.987,31.977 c31.785,21.888,58.625,49.87,80.51,83.939c-23.024,35.782-51.723,65-86.07,87.648c-34.358,22.661-71.712,35.693-112.065,39.115 l-21.129,37.688c42.257,0,82.18-9.038,119.769-27.121c37.59-18.076,70.668-43.488,99.216-76.225 c13.322-15.421,23.695-29.219,31.121-41.401c3.806-6.476,5.708-13.046,5.708-19.702 C511.626,249.157,509.724,242.59,505.916,236.114z";
                MasterGrid.Width = new GridLength(0);
                modeSpectateurActive = true;
            }
        }

        private void EffacerDonneesCliquees()
        {
            boutonsCarteActifs = boutonsCarteActifs.ToDictionary(p => p.Key, p => false);
            lesPointsCliques.Clear();
            ajouterEtoile.Fill = "AliceBlue";
            ajouterPlanete.Fill = "AliceBlue";
            effacer.Fill = "AliceBlue";
            relier.Fill = "AliceBlue";
            deplacer.Fill = "AliceBlue";
        }

        private void EtoileClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["ajouterEtoile"])
            {
                boutonsCarteActifs["ajouterEtoile"] = false;
                ajouterEtoile.Fill = "AliceBlue";
            }
            else
            {
                EffacerDonneesCliquees();
                boutonsCarteActifs["ajouterEtoile"] = true;
                ajouterEtoile.Fill = "Red";
            }
        }

        private void PlaneteClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["ajouterPlanete"])
            {
                boutonsCarteActifs["ajouterPlanete"] = false;
                ajouterPlanete.Fill = "AliceBlue";
            }
            else
            {
                EffacerDonneesCliquees();
                boutonsCarteActifs["ajouterPlanete"] = true;
                ajouterPlanete.Fill = "Red";
            }
        }

        private void RelierClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["relier"])
            {
                boutonsCarteActifs["relier"] = false;
                relier.Fill = "AliceBlue";
            }
            else
            {
                EffacerDonneesCliquees();
                boutonsCarteActifs["relier"] = true;
                relier.Fill = "Red";
            }
        }

        private void EffacerClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["effacer"])
            {
                boutonsCarteActifs["effacer"] = false;
                effacer.Fill = "AliceBlue";
            }
            else
            {
                EffacerDonneesCliquees();
                boutonsCarteActifs["effacer"] = true;
                effacer.Fill = "Red";
            }
        }

        private void DeplacerClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["deplacer"])
            {
                boutonsCarteActifs["deplacer"] = false;
                deplacer.Fill = "AliceBlue";
            }
            else
            {
                EffacerDonneesCliquees();
                boutonsCarteActifs["deplacer"] = true;
                deplacer.Fill = "Red";
            }
        }

        private void PoubelleClic(object sender, MouseButtonEventArgs e)
        {
           Manager.SupprimerTout();
        }

        private void PointClique(object sender, MouseButtonEventArgs e)
        {
            var point = new Geometrie.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);
            Geometrie.Point pointSurCarte = RecupererPointClique(point);

            //Debug.WriteLine($"Le point le plus proche de l'endroit cliqué est à {pointSurCarte}");

            if (pointSurCarte != null)
            {
                //Debug.WriteLine($"L'astre correspondant est {Manager.Carte.LesAstres[pointSurCarte]}");

                if (boutonsCarteActifs["effacer"])
                {
                    Manager.SupprimerUnAstre(pointSurCarte);
                }

                else if (boutonsCarteActifs["relier"])
                {
                    if (lesPointsCliques.Count == 0)
                    {
                        if (Manager.Carte.LesAstres[pointSurCarte] is Etoile)
                        {
                            lesPointsCliques.Add(pointSurCarte);
                        }
                        else
                        {
                            messageErreur.Text = "Seules des étoiles peuvent être reliées entre elles.";
                        }
                    }

                    else if (!lesPointsCliques[0].Equals(pointSurCarte))
                    {
                        if (Manager.Carte.LesAstres[pointSurCarte] is Etoile)
                        {
                            Manager.RelierDeuxEtoiles(lesPointsCliques[0], pointSurCarte);
                            lesPointsCliques.Clear();
                        }
                        else
                        {
                            messageErreur.Text = "Seules des étoiles peuvent être reliées entre elles.";
                        }
                    }
                }
                else if (boutonsCarteActifs["deplacer"])
                {
                    if (lesPointsCliques.Count == 0)
                    {
                        lesPointsCliques.Add(pointSurCarte);
                    }
                }
                else
                {
                    Popup.Visibility = Visibility.Visible;

                    var astreClique = new Binding("Nom")
                    {
                        Source = Manager.Carte.LesAstres[pointSurCarte]
                    };

                    //Debug.WriteLine(astreClique);
                    //astreClique.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    //Popup.SetBinding(TextBlock.TextProperty, astreClique);
                }
            }
        }

        private void CanevaClic(object sender, MouseButtonEventArgs e)
        {
            messageErreur.Text = "";

            if (boutonsCarteActifs["deplacer"] && lesPointsCliques.Count == 1)
            {
                var point = new Geometrie.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);
                if (!lesPointsCliques[0].Equals(point) && !RecherchePointAProximite(point))
                {
                    point.Deplacer(point.X - 15, point.Y);
                    Manager.DeplacerUnAstre(lesPointsCliques[0], point);
                }
            }
        }

        private bool RecherchePointAProximite(Geometrie.Point ptSelect)
        {
            foreach (Geometrie.Point pt in Manager.Carte.LesAstres.Keys)
            {
                if (ptSelect.X + 350 - 25 >= pt.X && ptSelect.Y + 50 - 25 >= pt.Y && ptSelect.X <= pt.X + 350 + 25 && ptSelect.Y <= pt.Y + 50 + 25)
                {
                    return true;
                }
            }
            return false;
        }

        private Geometrie.Point RecupererPointClique(Geometrie.Point ptSelect)
        {
            foreach (Geometrie.Point pt in Manager.Carte.LesAstres.Keys)
            {
                if (ptSelect.X + 350 >= pt.X && ptSelect.Y + 50 >= pt.Y && ptSelect.X <= pt.X + 350 + 15 && ptSelect.Y <= pt.Y + 50 + 15)
                {
                    return pt;
                }
            }
            return null;
        }

        /**********************************************************************************************/

        private void PopupClicMenu(object sender, MouseButtonEventArgs e)
        {
            Popup.Visibility = Visibility.Visible; //(Application.Current.MainWindow as MainWindow).
        }

        private void ModifierFavori(object sender, RoutedEventArgs e)
        {
            Button bouton = sender as Button;
            Astre astre = bouton.DataContext as Astre;
            astre.ModifierFavori();
        }

        private void FaireLaRecherche()
        {
            Manager.Filtrage(filtreFavoris, filtrePersonnalisation, filtreType, triOrdreAlphabetique, filtreNom);
            Popup.Visibility = Visibility.Hidden; 
        }

        private void BoutonTriAlphabetique(object sender, MouseButtonEventArgs e)
        {
            if (triOrdreAlphabetique)
            {
                triOrdreAlphabetique = false;
                Bouton1.Path = "M286.935,69.377c-3.614-3.617-7.898-5.424-12.848-5.424H18.274c-4.952,0-9.233,1.807-12.85,5.424 C1.807,72.998,0,77.279,0,82.228c0,4.948,1.807,9.229,5.424,12.847l127.907,127.907c3.621,3.617,7.902,5.428,12.85,5.428  s9.233-1.811,12.847-5.428L286.935,95.074c3.613-3.617,5.427-7.898,5.427-12.847C292.362,77.279,290.548,72.998,286.935,69.377z";
            }
            else
            {
                triOrdreAlphabetique = true;
                Bouton1.Path = "M286.935,197.287L159.028,69.381c-3.613-3.617-7.895-5.424-12.847-5.424s-9.233,1.807-12.85,5.424L5.424,197.287 C1.807,200.904,0,205.186,0,210.134s1.807,9.233,5.424,12.847c3.621,3.617,7.902,5.425,12.85,5.425h255.813 c4.949,0,9.233-1.808,12.848-5.425c3.613-3.613,5.427-7.898,5.427-12.847S290.548,200.904,286.935,197.287z";
            }
            FaireLaRecherche();
        }

        private void BoutonFiltreFavori(object sender, MouseButtonEventArgs e)
        {
            if (filtreFavoris)
            {
                filtreFavoris = false;
                Bouton2.Path = "m256 455.515625c-7.289062 0-14.316406-2.640625-19.792969-7.4375-20.683593-18.085937-40.625-35.082031-58.21875-50.074219l-.089843-.078125c-51.582032-43.957031-96.125-81.917969-127.117188-119.3125-34.644531-41.804687-50.78125-81.441406-50.78125-124.742187 0-42.070313 14.425781-80.882813 40.617188-109.292969 26.503906-28.746094 62.871093-44.578125 102.414062-44.578125 29.554688 0 56.621094 9.34375 80.445312 27.769531 12.023438 9.300781 22.921876 20.683594 32.523438 33.960938 9.605469-13.277344 20.5-24.660157 32.527344-33.960938 23.824218-18.425781 50.890625-27.769531 80.445312-27.769531 39.539063 0 75.910156 15.832031 102.414063 44.578125 26.191406 28.410156 40.613281 67.222656 40.613281 109.292969 0 43.300781-16.132812 82.9375-50.777344 124.738281-30.992187 37.398437-75.53125 75.355469-127.105468 119.308594-17.625 15.015625-37.597657 32.039062-58.328126 50.167969-5.472656 4.789062-12.503906 7.429687-19.789062 7.429687zm-112.96875-425.523437c-31.066406 0-59.605469 12.398437-80.367188 34.914062-21.070312 22.855469-32.675781 54.449219-32.675781 88.964844 0 36.417968 13.535157 68.988281 43.882813 105.605468 29.332031 35.394532 72.960937 72.574219 123.476562 115.625l.09375.078126c17.660156 15.050781 37.679688 32.113281 58.515625 50.332031 20.960938-18.253907 41.011719-35.34375 58.707031-50.417969 50.511719-43.050781 94.136719-80.222656 123.46875-115.617188 30.34375-36.617187 43.878907-69.1875 43.878907-105.605468 0-34.515625-11.605469-66.109375-32.675781-88.964844-20.757813-22.515625-49.300782-34.914062-80.363282-34.914062-22.757812 0-43.652344 7.234374-62.101562 21.5-16.441406 12.71875-27.894532 28.796874-34.609375 40.046874-3.453125 5.785157-9.53125 9.238282-16.261719 9.238282s-12.808594-3.453125-16.261719-9.238282c-6.710937-11.25-18.164062-27.328124-34.609375-40.046874-18.449218-14.265626-39.34375-21.5-62.097656-21.5zm0 0";

            }
            else
            {
                filtreFavoris = true;
                Bouton2.Path = "m471.382812 44.578125c-26.503906-28.746094-62.871093-44.578125-102.410156-44.578125-29.554687 0-56.621094 9.34375-80.449218 27.769531-12.023438 9.300781-22.917969 20.679688-32.523438 33.960938-9.601562-13.277344-20.5-24.660157-32.527344-33.960938-23.824218-18.425781-50.890625-27.769531-80.445312-27.769531-39.539063 0-75.910156 15.832031-102.414063 44.578125-26.1875 28.410156-40.613281 67.222656-40.613281 109.292969 0 43.300781 16.136719 82.9375 50.78125 124.742187 30.992188 37.394531 75.535156 75.355469 127.117188 119.3125 17.613281 15.011719 37.578124 32.027344 58.308593 50.152344 5.476563 4.796875 12.503907 7.4375 19.792969 7.4375 7.285156 0 14.316406-2.640625 19.785156-7.429687 20.730469-18.128907 40.707032-35.152344 58.328125-50.171876 51.574219-43.949218 96.117188-81.90625 127.109375-119.304687 34.644532-41.800781 50.777344-81.4375 50.777344-124.742187 0-42.066407-14.425781-80.878907-40.617188-109.289063zm0 0";
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

        private void barreRecherche(object sender, TextChangedEventArgs e)
        {
            filtreNom = BarreRecherche.Text;
            FaireLaRecherche();
        }

        private void SurvolBouton(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void FinSurvolBouton(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
