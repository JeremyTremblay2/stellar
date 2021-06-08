using Espace;
using Modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Appli.fenetres;
using Appli.usersControls;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization;

namespace Appli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Constantes qui correspondent au début des coordonnées du caneva, donc aux décalages horizontaux et verticaux.
        private const int decalageHorizontalCanvas = 350;
        private const int decalageVerticalCanvas = 50;

        //Les attributs permettant d'effectuer les tris. Ils sont modifiés lors des clics sur les boutons.
        private bool triOrdreAlphabetique = false;
        private byte filtrePersonnalisation = 3;
        private Type filtreType = typeof(Astre);
        private bool filtreFavoris = false;
        private string filtreNom;

        //Permet de savoir quand est pressée la touche "Controle"
        private bool CtrlOk = false;

        //Permet de savoir quels sont les boutons activés sur la carte, de manière à ce qu'un seul puisse être activé à la fois.
        private bool modeSpectateurActive = false;
        private Dictionary<string, bool> boutonsCarteActifs = new Dictionary<string, bool>()
        {
            ["ajouterEtoile"] = false,
            ["ajouterPlanete"] = false,
            ["relier"] = false,
            ["effacer"] = false,
            ["deplacer"] = false,
            ["modifier"] = false,
        };

        //Le point cliqué par l'utilisateur.
        private Geometrie.Point pointClique;

        //L'astre à ajouter sur la carte (clic effectué depuis le popup, dans la partie détail).
        private Astre astreAAjouter;

        //Le manager.
        public Manager Manager => (Application.Current as App).LeManager;

        /// <summary>
        /// Le constructeur de la fenêtre principale.
        /// On précise le DataContexte qui est ici le Manager.
        /// Puis on vient spécifier l'image du mode spectateur.
        /// </summary>
        public MainWindow()
        { 
            InitializeComponent();
            DataContext = Manager;
            modeSpectateur.Path = "M505.918,236.117c-26.651-43.587-62.485-78.609-107.497-105.065c-45.015-26.457-92.549-39.687-142.608-39.687 c-50.059,0-97.595,13.225-142.61,39.687C68.187,157.508,32.355,192.53,5.708,236.117C1.903,242.778,0,249.345,0,255.818 c0,6.473,1.903,13.04,5.708,19.699c26.647,43.589,62.479,78.614,107.495,105.064c45.015,26.46,92.551,39.68,142.61,39.68 c50.06,0,97.594-13.176,142.608-39.536c45.012-26.361,80.852-61.432,107.497-105.208c3.806-6.659,5.708-13.223,5.708-19.699 C511.626,249.345,509.724,242.778,505.918,236.117z M194.568,158.03c17.034-17.034,37.447-25.554,61.242-25.554 c3.805,0,7.043,1.336,9.709,3.999c2.662,2.664,4,5.901,4,9.707c0,3.809-1.338,7.044-3.994,9.704 c-2.662,2.667-5.902,3.999-9.708,3.999c-16.368,0-30.362,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971 c0,3.811-1.336,7.044-3.999,9.71c-2.667,2.668-5.901,3.999-9.707,3.999c-3.809,0-7.044-1.334-9.71-3.999 c-2.667-2.666-3.999-5.903-3.999-9.71C169.015,195.482,177.535,175.065,194.568,158.03z M379.867,349.04 c-38.164,23.12-79.514,34.687-124.054,34.687c-44.539,0-85.889-11.56-124.051-34.687s-69.901-54.2-95.215-93.222 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.207-17.417,64.236c0,35.216,12.517,65.329,37.544,90.362 s55.151,37.544,90.362,37.544c35.214,0,65.329-12.518,90.362-37.544s37.545-55.146,37.545-90.362 c0-23.029-5.808-44.447-17.419-64.236c43.585,22.265,79.846,55.865,108.776,100.783C449.767,294.84,418.031,325.913,379.867,349.04 z";
            UCPopup Popup = new UCPopup();
            FaireLaRecherche();
        }

        //Partie "Carte" ou éditeur.
        //Contient les diverses méthodes permettant de réaliser des actions sur la carte.

        /// <summary>
        /// Permet de cacher le master lorque le bouton spectateur est cliqué.
        /// On vient modifier l'image du bouton spectateur, puis cacher la plupart des autes boutons.
        /// Si l'action inverse est effectuée, alors on effectue l'inverse également.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpectateurClic(object sender, MouseButtonEventArgs e)
        {
            if (modeSpectateurActive)
            {
                modeSpectateur.Path = "M505.918,236.117c-26.651-43.587-62.485-78.609-107.497-105.065c-45.015-26.457-92.549-39.687-142.608-39.687 c-50.059,0-97.595,13.225-142.61,39.687C68.187,157.508,32.355,192.53,5.708,236.117C1.903,242.778,0,249.345,0,255.818 c0,6.473,1.903,13.04,5.708,19.699c26.647,43.589,62.479,78.614,107.495,105.064c45.015,26.46,92.551,39.68,142.61,39.68 c50.06,0,97.594-13.176,142.608-39.536c45.012-26.361,80.852-61.432,107.497-105.208c3.806-6.659,5.708-13.223,5.708-19.699 C511.626,249.345,509.724,242.778,505.918,236.117z M194.568,158.03c17.034-17.034,37.447-25.554,61.242-25.554 c3.805,0,7.043,1.336,9.709,3.999c2.662,2.664,4,5.901,4,9.707c0,3.809-1.338,7.044-3.994,9.704 c-2.662,2.667-5.902,3.999-9.708,3.999c-16.368,0-30.362,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971 c0,3.811-1.336,7.044-3.999,9.71c-2.667,2.668-5.901,3.999-9.707,3.999c-3.809,0-7.044-1.334-9.71-3.999 c-2.667-2.666-3.999-5.903-3.999-9.71C169.015,195.482,177.535,175.065,194.568,158.03z M379.867,349.04 c-38.164,23.12-79.514,34.687-124.054,34.687c-44.539,0-85.889-11.56-124.051-34.687s-69.901-54.2-95.215-93.222 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.207-17.417,64.236c0,35.216,12.517,65.329,37.544,90.362 s55.151,37.544,90.362,37.544c35.214,0,65.329-12.518,90.362-37.544s37.545-55.146,37.545-90.362 c0-23.029-5.808-44.447-17.419-64.236c43.585,22.265,79.846,55.865,108.776,100.783C449.767,294.84,418.031,325.913,379.867,349.04 z";
                MasterGrid.Width = new GridLength(decalageHorizontalCanvas);
                modeSpectateurActive = false;
                ajouterEtoile.Visibility = Visibility.Visible;
                ajouterPlanete.Visibility = Visibility.Visible;
                effacer.Visibility = Visibility.Visible;
                relier.Visibility = Visibility.Visible;
                deplacer.Visibility = Visibility.Visible;
                modifier.Visibility = Visibility.Visible;
            }
            else
            {
                modeSpectateur.Path = "M372.872,94.221c0.191-0.378,0.28-1.235,0.28-2.568c0-3.237-1.522-5.802-4.571-7.715c-0.568-0.38-2.423-1.475-5.568-3.287 c-3.138-1.805-6.14-3.567-8.989-5.282c-2.854-1.713-5.989-3.472-9.422-5.28c-3.426-1.809-6.375-3.284-8.846-4.427 c-2.479-1.141-4.189-1.713-5.141-1.713c-3.426,0-6.092,1.525-7.994,4.569l-15.413,27.696c-17.316-3.234-34.451-4.854-51.391-4.854 c-51.201,0-98.404,12.946-141.613,38.831C70.998,156.08,34.836,191.385,5.711,236.114C1.903,242.019,0,248.586,0,255.819 c0,7.231,1.903,13.801,5.711,19.698c16.748,26.073,36.592,49.396,59.528,69.949c22.936,20.561,48.011,37.018,75.229,49.396 c-8.375,14.273-12.562,22.556-12.562,24.842c0,3.425,1.524,6.088,4.57,7.99c23.219,13.329,35.97,19.985,38.256,19.985 c3.422,0,6.089-1.529,7.992-4.575l13.99-25.406c20.177-35.967,50.248-89.931,90.222-161.878 C322.908,183.871,352.886,130.005,372.872,94.221z M158.456,362.885C108.97,340.616,68.33,304.93,36.547,255.822 c28.931-44.921,65.19-78.518,108.777-100.783c-11.61,19.792-17.417,41.206-17.417,64.237c0,20.365,4.661,39.68,13.99,57.955 c9.327,18.274,22.27,33.4,38.83,45.392L158.456,362.885z M265.525,155.887c-2.662,2.667-5.906,3.999-9.712,3.999 c-16.368,0-30.361,5.808-41.971,17.416c-11.613,11.615-17.416,25.603-17.416,41.971c0,3.811-1.336,7.044-3.999,9.71 c-2.668,2.667-5.902,3.999-9.707,3.999c-3.809,0-7.045-1.334-9.71-3.999c-2.667-2.666-3.999-5.903-3.999-9.71 c0-23.79,8.52-44.206,25.553-61.242c17.034-17.034,37.447-25.553,61.241-25.553c3.806,0,7.043,1.336,9.713,3.999 c2.662,2.664,3.996,5.901,3.996,9.707C269.515,149.992,268.181,153.228,265.525,155.887z M361.161,291.652c15.037-21.796,22.56-45.922,22.56-72.375c0-7.422-0.76-15.417-2.286-23.984l-79.938,143.321 C326.235,329.101,346.125,313.438,361.161,291.652z M505.916,236.114c-10.853-18.08-24.603-35.594-41.255-52.534c-16.646-16.939-34.022-31.496-52.105-43.68l-17.987,31.977 c31.785,21.888,58.625,49.87,80.51,83.939c-23.024,35.782-51.723,65-86.07,87.648c-34.358,22.661-71.712,35.693-112.065,39.115 l-21.129,37.688c42.257,0,82.18-9.038,119.769-27.121c37.59-18.076,70.668-43.488,99.216-76.225 c13.322-15.421,23.695-29.219,31.121-41.401c3.806-6.476,5.708-13.046,5.708-19.702 C511.626,249.157,509.724,242.59,505.916,236.114z";
                MasterGrid.Width = new GridLength(0);
                modeSpectateurActive = true;
                EffacerDonneesCliquees();
                ajouterEtoile.Visibility = Visibility.Hidden;
                ajouterPlanete.Visibility = Visibility.Hidden;
                effacer.Visibility = Visibility.Hidden;
                relier.Visibility = Visibility.Hidden;
                deplacer.Visibility = Visibility.Hidden;
                modifier.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet de réinitialiser l'affichage de la carte.
        /// Désactive tous les boutons qui pouvaient être cliqués.
        /// Rétablit les couleurs des points de la carte, car ils pouvaient éventuellement avoir été modifiés.
        /// </summary>
        private void EffacerDonneesCliquees()
        {
            boutonsCarteActifs = boutonsCarteActifs.ToDictionary(p => p.Key, p => false);
            pointClique = null;
            astreAAjouter = null;

            ajouterEtoile.Fill = "AliceBlue";
            ajouterPlanete.Fill = "AliceBlue";
            effacer.Fill = "AliceBlue";
            relier.Fill = "AliceBlue";
            deplacer.Fill = "AliceBlue";
            modifier.Fill = "AliceBlue";

            foreach (KeyValuePair<Geometrie.Point, Astre> kvp in Manager.Carte.LesAstres)
            {
                if (kvp.Value is Etoile)
                {
                    kvp.Key.Couleur = "Yellow";
                }
                else
                {
                    kvp.Key.Couleur = "Green";
                }
            }
        }

        /// <summary>
        /// Événement lié au clic du bouton Modifier, on vient changer sa couleur.
        /// Il permet de modifier un astre personnalisé.
        /// Si le bouton est cliqué alors qu'il était déjà actif, alors on inverse son état.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["modifier"])
            {
                EffacerDonneesCliquees();
            }
            else
            {
                EffacerDonneesCliquees();
                boutonsCarteActifs["modifier"] = true;
                modifier.Fill = "Red";

                //On passe tous les astres modifiables en Cyan sur la carte, de manière à ce que l'utilisateur puisse se rendre compte
                //facilement des astres modifiables.
                foreach (KeyValuePair<Geometrie.Point, Astre> kvp in Manager.Carte.LesAstres)
                {
                    if (kvp.Value.Personnalise)
                    {
                        kvp.Key.Couleur = "Cyan";
                    }
                }
            }
        }
        /// <summary>
        /// Événement lié au clic du bouton Etoile, on vient changer sa couleur.
        /// Il permet de créer une étoile. Cet évènement se déclenchera au prochain clic sur le caneva.
        /// Si le bouton est cliqué alors qu'il était déjà actif, alors on inverse son état.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Événement lié au clic du bouton Planète, on vient changer sa couleur.
        /// Il permet de créer une planète. Cet évènement se déclenchera au prochain clic sur le caneva.
        /// Si le bouton est cliqué alors qu'il était déjà actif, alors on inverse son état.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Événement lié au clic du bouton Relier, on vient changer sa couleur.
        /// Il permet de relier deux étoiles entre elles. Cet évènement se déclenchera lorsque deux étoiles seront cliquées sur la carte.
        /// Si le bouton est cliqué alors qu'il était déjà actif, alors on inverse son état.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Événement lié au clic du bouton Effacer, on vient changer sa couleur.
        /// Il permet d'effacer un astre de la carte et ses constellations. Cet évènement se déclenchera au prochaine clic d'un astre
        /// sur la carte.
        /// Si le bouton est cliqué alors qu'il était déjà actif, alors on inverse son état.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Événement lié au clic du bouton Deplacer, on vient changer sa couleur.
        /// Il permet de déplacer un astre à une autre position sur la carte. Cet évènement se déclenchera lors du prochain clic d'un astre,
        /// puis lorsque que la carte sera cliquée à un autre endroit.
        /// Si le bouton est cliqué alors qu'il était déjà actif, alors on inverse son état.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeplacerClic(object sender, MouseButtonEventArgs e)
        {
            if (boutonsCarteActifs["deplacer"])
            {
                astreAAjouter = null;
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

        /// <summary>
        /// Événement lié au clic du bouton Ouvrir.
        /// Cette méthode permet d'ouvrir une fenêtre de l'explorateur windows qui permet de charger le fichier de données d'une carte.
        /// Si l'utilisateur choisi un fichier valide, alors il sera ouvert puis les données chargées.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargementClic(object sender, MouseButtonEventArgs e)
        {
            bool carteVide = false;
            MessageBoxResult resultat = MessageBoxResult.No;

            //Ouverture de l'explorateur. On n'accepte que les fichiers XML.
            OpenFileDialog chargementFichier = new OpenFileDialog();
            chargementFichier.Filter = "XML files (*.xml)|*.xml";

            //Si l'utilisateur a choisi un fichier.
            if (chargementFichier.ShowDialog() == true)
            {
                Debug.WriteLine(chargementFichier.FileName);

                //Si la carte est vide, on vient passer cette variable à vrai, ce qui fait que l'on ne demande pas confirmation pour le chargement.
                if (!Manager.Carte.LesAstres.Any())
                {
                    carteVide = true;
                }
                //Sinon on demande confirmation pour le chargement de la carte.
                else
                {
                    resultat = MessageBox.Show("Ouvrir un nouveau projet effacera la carte actuelle, êtes-vous sûr de vouloir ouvrir" +
                                                " un nouveau document ?",
                                                "Ouverture d'une carte",
                                                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }

                //Si l'utilisateur accepte, on charge le fichier.
                if (resultat == MessageBoxResult.Yes || carteVide)
                {
                    try
                    {
                        Debug.WriteLine(chargementFichier.FileName);
                        Manager.ChargeDonneesCarte(chargementFichier.FileName);
                        FaireLaRecherche();
                    }
                    catch (Exception erreur)
                    {
                        MessageBox.Show(erreur.Message,
                                        "Un problème est survenu",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Événement lié au clic du bouton sauvegarder. 
        /// Cette méthode ouvre un explorateur de fichier windows dans lequel l'utilisateur est invité à sélectionner un nom de fichier 
        /// dans lequel sa carte sera sauvegardée. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SauvegardeClic(object sender, MouseButtonEventArgs e)
        {
            //Ouverture de l'explorateur.
            SaveFileDialog sauvegardeFichier = new SaveFileDialog();
            sauvegardeFichier.Filter = "XML file (*.xml)|*.xml";

            //Si l'utilisateur a choisi un nom de fichier et a sauvegardé, alors on appelle la méthode de sauvegarde avec le chemin d'où
            //se trouve le fichier.
            if (sauvegardeFichier.ShowDialog() == true)
            {
                Debug.WriteLine(sauvegardeFichier.FileName);
                Manager.SauvegardeDonneesCarte(sauvegardeFichier.FileName);
            }
        }

        /// <summary>
        /// Événement lié au clic du bouton poubelle. 
        /// Il permet de tout supprimer sur la carte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PoubelleClic(object sender, MouseButtonEventArgs e)
        {
            //Avertissement et confirmation de la part de l'utilisateur.
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment effacer toute la carte ?",
                                        "Supprimer",
                                        MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (resultat)
            {
                case MessageBoxResult.Yes:
                    Manager.SupprimerTout();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        /// <summary>
        /// Événement lié aux clics sur les astres. 
        /// Permet de réaliser diverses actions en fonction des boutons qui ont pu être actionnés.
        /// Appelle les méthodes dans le Manager necessaire selon les boutons d'outils cliqués.
        /// Pour cela on récupère les coordonnées de l'endroit où le clic a eu lieu, puis on appelle une méthode qui nous donne le point
        /// de coordonnées de l'astre le plus proche de la zone cliquée.
        /// On traite ensuite les divers cas en fonction des boutons qui peuvent être appuyés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointClique(object sender, MouseButtonEventArgs e)
        {
            //On récupère les coordonnées du clic, puis on récupère le point le plus proche correspondant à ces coordonnées.
            var point = new Geometrie.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);
            Geometrie.Point pointSurCarte = RecupererPointClique(point);

            //Le mode spectateur doit être désactivé.
            if (pointSurCarte != null && !modeSpectateurActive)
            {
                //On efface l'astre correspondant si le bouton était actif.
                if (boutonsCarteActifs["effacer"])
                {
                    Manager.SupprimerUnAstre(pointSurCarte);
                }

                //Si le bouton our relier était actif, on vient vérifier si un point avait déjà été cliqué, s'il s'agit bien d'une étoile...
                else if (boutonsCarteActifs["relier"])
                {
                    if (pointClique == null)
                    {
                        if (Manager.Carte.LesAstres[pointSurCarte] is Etoile)
                        {
                            pointClique = pointSurCarte;
                        }
                        else
                        {
                            MessageBox.Show("Il est impossible de relier des planètes, seules des étoiles peuvent former une constellation.",
                                "Impossible de relier des planètes",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }

                    //Une fois le deuxième point cliqué de cette manière, on vient appeller la méthode pour relier les deux points ensembles.
                    else if (!pointClique.Equals(pointSurCarte))
                    {
                        if (Manager.Carte.LesAstres[pointSurCarte] is Etoile)
                        {
                            Manager.RelierDeuxEtoiles(pointClique, pointSurCarte);
                            pointClique = null;
                        }
                        else
                        {
                            MessageBox.Show("Il est impossible de relier des planètes, seules des étoiles peuvent former une constellation.",
                                "Impossible de relier des planètes",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }

                //Si le bouton de déplacement était actif, on vient sauvegarder le point qui a été cliqué dans une variable.
                else if (boutonsCarteActifs["deplacer"])
                {
                    if (pointClique == null)
                    {
                        pointClique = pointSurCarte;
                    }
                    else
                    {
                        pointClique = null;
                        pointClique = pointSurCarte;
                    }
                }

                //Si c'était le bouton de modification qui était actif, alors on vient récupérer l'astre correspondant et vérifier que c'est
                //un astre personnalisé.
                else if (boutonsCarteActifs["modifier"])
                {
                    var astre = Manager.Carte.LesAstres[pointSurCarte];

                    if (!astre.Personnalise)
                    {
                        MessageBox.Show("Il est impossible de modifier les données d'un astre déjà existant dans l'application.",
                                "Impossible de modifier cet astre",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    //Etoile ou planète, les actions sont similaires.
                    if (astre is Etoile)
                    {
                        Manager.AstreSelectionne = null;

                        //On vient créer une nouvelle fenêtre d'ajout d'étoile, et on vient setter l'astre que l'on a récupérer.

                        var nouvelleEtoile = new AjouterEtoile();
                        nouvelleEtoile.Owner = this;
                        nouvelleEtoile.LEtoile = (Etoile)astre;

                        //On crée une copie de notre étoile, et on vient le setter à la version qui sera modifiée dans la fenêtre.
                        nouvelleEtoile.LEtoileEditable = new Etoile(nouvelleEtoile.LEtoile.Nom, nouvelleEtoile.LEtoile.Description,
                            nouvelleEtoile.LEtoile.Age, nouvelleEtoile.LEtoile.Masse, nouvelleEtoile.LEtoile.Temperature,
                            nouvelleEtoile.LEtoile.Constellation, nouvelleEtoile.LEtoile.Luminosite, nouvelleEtoile.LEtoile.Type,
                            nouvelleEtoile.LEtoile.Personnalise, nouvelleEtoile.LEtoile.Image);

                        nouvelleEtoile.EstEnCoursDeCreation = false;
                        nouvelleEtoile.ShowDialog();

                        //Si l'utilisateur valide, alors on appelle la méthode de modification qui viendra mettre à jour les données.
                        if (nouvelleEtoile.LEtoileEditable != null)
                        {
                            Manager.ModifierUnAstre(nouvelleEtoile.LEtoile, nouvelleEtoile.LEtoileEditable);
                            EffacerDonneesCliquees();
                            FaireLaRecherche();
                        }
                    }
                    else
                    {
                        Manager.AstreSelectionne = null;

                        var nouvellePlanete = new AjouterPlanete();
                        nouvellePlanete.Owner = this;
                        nouvellePlanete.LaPlanete = (Planete)astre;

                        nouvellePlanete.LaPlaneteEditable = new Planete(nouvellePlanete.LaPlanete.Nom, nouvellePlanete.LaPlanete.Description,
                            nouvellePlanete.LaPlanete.Age, nouvellePlanete.LaPlanete.Masse, nouvellePlanete.LaPlanete.Temperature,
                            nouvellePlanete.LaPlanete.Vie, nouvellePlanete.LaPlanete.EauPresente, nouvellePlanete.LaPlanete.Systeme,
                            nouvellePlanete.LaPlanete.Type, nouvellePlanete.LaPlanete.Personnalise, nouvellePlanete.LaPlanete.Image);

                        nouvellePlanete.EstEnCoursDeCreation = false;
                        nouvellePlanete.ShowDialog();

                        if (nouvellePlanete.LaPlaneteEditable != null)
                        {
                            Manager.ModifierUnAstre(nouvellePlanete.LaPlanete, nouvellePlanete.LaPlaneteEditable);
                            EffacerDonneesCliquees();
                            FaireLaRecherche();
                        }
                    }
                }
                //Sinon on affiche juste le popup de l'astre correspondant.
                else
                {
                    Manager.AstreSelectionne = Manager.Carte.LesAstres[pointSurCarte];
                    PopupClicMenu(null, null);
                }
            }
        }

        /// <summary>
        /// Événement lié aux clics sur le Caneva. Transforme un clic en Point de coordonnées.
        /// Réalise ensuite diverses actions en fonction des outils cliqués.
        /// La plupart concernent des ajouts de points sur la carte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanevaClic(object sender, MouseButtonEventArgs e)
        {
            var point = new Geometrie.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);

            //On vérifie toujours que le clic ne se fait pas à côté d'un point, et que l'on ne clique pas sur le côté du menu.
            if (!RecherchePointAProximite(point) && point.X > 16 + decalageHorizontalCanvas)
            {
                //Si le bouton déplacé était activé et que l'utilisateur avait déjà sélectionné un point, alors on vient le déplacer.
                if (boutonsCarteActifs["deplacer"] && pointClique != null)
                {
                    if (!point.Equals(pointClique))
                    {
                        point.Deplacer(point.X - 15 - decalageHorizontalCanvas, point.Y - decalageVerticalCanvas);
                        Manager.DeplacerUnAstre(pointClique, point);
                        pointClique = null;
                    }
                }

                //Si l'utilisateur avait appuyé sur le bouton d'ajout (étoile ou planète), alors on crée une nouvelle fenêtre d'ajout.
                else if (boutonsCarteActifs["ajouterEtoile"])
                {
                    var nouvelleEtoile = new AjouterEtoile();
                    nouvelleEtoile.Owner = this;
                    nouvelleEtoile.ShowDialog();

                    //S'il valide, alors on vient ajouter l'astre correspondant.
                    if (nouvelleEtoile.LEtoileEditable != null)
                    {
                        EffacerDonneesCliquees();
                        point.Deplacer(point.X - 15 - decalageHorizontalCanvas, point.Y - decalageVerticalCanvas);
                        Manager.AjouterUnAstre(point, nouvelleEtoile.LEtoileEditable);
                        FaireLaRecherche();
                    }
                }

                //La méthode est similaire.
                else if (boutonsCarteActifs["ajouterPlanete"])
                {
                    var nouvellePlanete = new AjouterPlanete();
                    nouvellePlanete.Owner = this;
                    nouvellePlanete.ShowDialog();

                    if (nouvellePlanete.LaPlaneteEditable != null)
                    {
                        EffacerDonneesCliquees();
                        point.Deplacer(point.X - 15 - decalageHorizontalCanvas, point.Y - decalageVerticalCanvas);
                        Manager.AjouterUnAstre(point, nouvellePlanete.LaPlaneteEditable);
                        FaireLaRecherche();
                    }
                }

                //S'il avait cliqué sur "ajouter à la carte" depuis la popup, alors on vient ajouter l'astre correspodant à la carte.
                else if (astreAAjouter != null)
                {
                    point.Deplacer(point.X - decalageHorizontalCanvas, point.Y - decalageVerticalCanvas);
                    Manager.AjouterUnAstre(point, astreAAjouter);
                    astreAAjouter = null;
                }
            }
        }

        /// <summary>
        /// Permet de rechercher si un point est à proximité d'un clic sur le caneva.
        /// Permet d'éviter que des points se retrouvent surperposés.
        /// Parcours les points de la carte et retourne vrai si un se trouve assez proche du point passé en paramètre.
        /// </summary>
        /// <param name="ptSelect">Point sélectionné lors d'un clic sur le caneva</param>
        /// <returns>Un booléen, donc vrai si un point (Astre) est à proximité du point passé en paramètre et faux si rien n'est trouvé</returns>
        private bool RecherchePointAProximite(Geometrie.Point ptSelect)
        {
            foreach (Geometrie.Point pt in Manager.Carte.LesAstres.Keys)
            {
                if (ptSelect.X - decalageHorizontalCanvas + 15 >= pt.X && ptSelect.Y - decalageVerticalCanvas + 25 >= pt.Y 
                    && ptSelect.X - decalageHorizontalCanvas <= pt.X + 40 && ptSelect.Y - decalageVerticalCanvas <= pt.Y + 25)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permet de récupérer le point (l'astre) qui a été cliqué à partir d'un point passé en paramètre.
        /// Cette méthode parcourt les points de la carte et retourne le premier point le plus proche du point cliqué.
        /// Retourne null sinon.
        /// </summary>
        /// <param name="ptSelect">Point cliqué sur le caneva</param>
        /// <returns>Le point (Astre) cliqué.</returns>
        private Geometrie.Point RecupererPointClique(Geometrie.Point ptSelect)
        {
            foreach (Geometrie.Point pt in Manager.Carte.LesAstres.Keys)
            {
                //Décalage de +/- 15 pixels à gauche et en haut du point cliqué.
                if (ptSelect.X - decalageHorizontalCanvas >= pt.X && ptSelect.Y - decalageVerticalCanvas >= pt.Y 
                    && ptSelect.X - decalageHorizontalCanvas <= pt.X + 15 && ptSelect.Y - decalageVerticalCanvas <= pt.Y + 15)
                {
                    return pt;
                }
            }
            return null;
        }

        /**********************************************************************************************/

        //Partie "Master" (menu latéral).
        //Corresond aux recherches, modifications des favoris.

        /// <summary>
        /// Cette méthode est appelée lorsque l'utilisateur clique sur le bouton pour ajouter un astre dans le popup, depuis la partie détail.
        /// On récupère ainsi le datacontexte de la popup, ce qui nous permet de récupérer l'astre que l'utilisateur va ajouter, au prochain
        /// clic sur la carte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPopup_AjouterAstre(object sender, RoutedEventArgs e)
        {
            EffacerDonneesCliquees();
            UserControl popup = sender as UserControl;
            astreAAjouter = popup.DataContext as Astre;
        }

        /// <summary>
        /// Ouvre le popup de détail lorsque un élément de la liste du menu est cliqué.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupClicMenu(object sender, MouseButtonEventArgs e)
        {
            astreAAjouter = null;
            Popup.Visibility = Visibility.Visible; 
        }

        /// <summary>
        /// Modifie le favori d'un astre lorsque le bouton est cliqué.
        /// Appelle la méthode de sauvegarde du Manager afin que les données soient enregistrées.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierFavori(object sender, RoutedEventArgs e)
        {
            Button bouton = sender as Button;
            Astre astre = bouton.DataContext as Astre;
            astre.ModifierFavori();
            Manager.SauvegardeDonnees();
        }

        /// <summary>
        /// Permet d'éffectuer la recherche demandée, en fonction des différents paramètres activés.
        /// </summary>
        private void FaireLaRecherche()
        {
            Manager.Filtrage(filtreFavoris, filtrePersonnalisation, filtreType, triOrdreAlphabetique, filtreNom);
            Popup.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Événement lié au bouton de tri alphabetique. Mets à jour la recherche lorsque le bouton est cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonTriAlphabetique(object sender, MouseButtonEventArgs e)
        {
            //On modifie le "Path" qui correspond à l'image de la flèche de tri, en fonction de l'état du bouton.
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

        /// <summary>
        /// Événement lié au bouton des favoris. 
        /// Mets à jour la recherche lorsque le bouton est cliqué, et relance une recherche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonFiltreFavori(object sender, MouseButtonEventArgs e)
        {
            //On modifie le "Path" qui correspond à l'image du coeur vide ou plein, en fonction de l'état du bouton.
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

        /// <summary>
        /// Événement lié au bouton de tri par astre personnalisé. 
        /// Mets à jour la recherche lorsque le bouton est cliqué, et relance une recherche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonFiltrePersonnalisation(object sender, RoutedEventArgs e)
        {
            //En fonction de l'état du bouton, on vient modifier son texte et sa valeur. La recherche est modifiée en conséquence.
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
        /// <summary>
        /// Événement lié au bouton de tri par type d'astre. 
        /// Mets à jour la recherche lorsque le bouton est cliqué, et relance une recherche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonFiltreType(object sender, RoutedEventArgs e)
        {
            //En fonction de l'état du bouton, on vient modifier son image et sa valeur. La recherche est modifiée en conséquence.
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

        /// <summary>
        /// Événement lié à la barre de recherche. Mets à jour la recherche selon les caractères entrés dans la boîte de dialogue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barreRecherche(object sender, TextChangedEventArgs e)
        {
            filtreNom = BarreRecherche.Text;
            FaireLaRecherche();
        }

        /// <summary>
        /// Effets visuels lors du survol de la souris des boutons.
        /// Permet de montrer à l'utilisateur les endroits auxquels il est possible d'interagir.
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
        /// Raccourcis clavier, permettant d'améliorer l'ergonomie de l'application, et facilitant l'usage des outils de la partie Carte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RaccourcisClavier(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                CtrlOk = true;

            Debug.WriteLine(e.Key.ToString());
            
            if (CtrlOk && e.Key == Key.D)
            {
                DeplacerClic(null, null);
            } 
            else if (CtrlOk && e.Key == Key.G)
            {
                EffacerClic(null, null);
            } 
            else if (CtrlOk && e.Key == Key.O)
            {
                CtrlOk = false;
                ChargementClic(null, null);
            }
            else if(CtrlOk && e.Key == Key.S)
            {
                CtrlOk = false;
                SauvegardeClic(null, null);
            }
            else if (CtrlOk && e.Key == Key.E)
            {
                EtoileClic(null, null);
            }
            else if (CtrlOk && e.Key == Key.P)
            {
                PlaneteClic(null, null);
            }
            else if (CtrlOk && e.Key == Key.J)
            {
                RelierClic(null, null);
            }
            else if (CtrlOk && e.Key == Key.M)
            {
                ModifierClic(null, null);
            }
            else if (CtrlOk && e.Key == Key.U)
            {
                CtrlOk = false;
                PoubelleClic(null, null);
            }
            else if (e.Key == Key.F12)
            {
                SpectateurClic(null, null);
            }
        }

        /// <summary>
        /// Mets à false CtrlUp lorsque le bouton LeftCtrl est relaché. 
        /// Permet concrètement de pouvoir effectuer plusieurs raccourcis clavier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtrlUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                CtrlOk = false;
        }
    }
}
