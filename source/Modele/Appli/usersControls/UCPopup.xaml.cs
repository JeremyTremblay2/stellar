using Modele;
using Espace;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Appli.usersControls
{
    /// <summary>
    /// Logique d'interaction pour UCPopup.xaml.
    /// Il s'agit de la partie Détail de l'application, dans laquelle on retrouve la plupart des informations.
    /// </summary>
    public partial class UCPopup : UserControl
    {
        //Ne vaut pas null lorsque l'utilisateur décide d'ajouter l'astre à la carte.
        private Astre astreAAjouter;

        public Manager LeManager => (Application.Current as App).LeManager;

        /// <summary>
        /// Constructeur de notre fenêtre popup. Par défaut la fenêtre est cachée.
        /// </summary>
        public UCPopup()
        {
            InitializeComponent();
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Evenement permettant de notifier le programmer principal que l'utilisateur souhaite ajouter l'astre sur la carte.
        /// </summary>
        public static readonly RoutedEvent AjouterAstreSurCarteEvent = EventManager.RegisterRoutedEvent("AjouterAstreSurCarte", 
            RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(UCPopup));

        /// <summary>
        /// Permet d'abonner ou de désabonner l'évènement.
        /// </summary>
        public event RoutedEventHandler AjouterAstreSurCarte
        {
            add { AddHandler(AjouterAstreSurCarteEvent, value); }
            remove { RemoveHandler(AjouterAstreSurCarteEvent, value); }
        }

        /// <summary>
        /// Lorsqu'on clique à l'extérieur de la popup, on la fait disparaître.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Lorsqu'on clique sur la croix, on fait disparaître la popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPopupClicCroix(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Méthode appellée lorsque l'utilisateur clique sur le bouton pour ajouter l'astre à la carte.
        /// On récupère alors l'astre sélectionné.
        /// On vérifie ensuite que cet astre précisémment ne se trouve aps sur la carte. Pour cela, on vient parcourir les données de la
        /// Carte, et on passe un booléen à True si l'astre est déjà présent. 
        /// Enfin, s'il ne s'agit aps d'un astre personnalisé et qu'il ne se trouve pas déjà sur la carte, alors on envoie un évènement
        /// à la fenêtre principale qui pourra gérer l'ajout sur la carte.
        /// Sinon on affiche une messageBox indiquant à l'utilisateur que l'action n'est pas possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterAstre(object sender, MouseButtonEventArgs e)
        {
            bool astreExistantSurCarte = false;
            astreAAjouter = LeManager.AstreSelectionne;

            if (astreAAjouter == null) return;

            if (!astreAAjouter.Personnalise)
            {
                foreach (KeyValuePair<Geometrie.Point, Astre> kvp in LeManager.Carte.LesAstres)
                {
                    if (kvp.Value.Equals(astreAAjouter))
                    {
                        astreExistantSurCarte = true;
                    }
                }
                if (!astreExistantSurCarte)
                {
                    (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
                    RaiseEvent(new RoutedEventArgs(AjouterAstreSurCarteEvent));
                    return;
                }
            }
            MessageBox.Show("Impossible d'ajouter l'astre sélectionné sur la carte, il est déjà présent.",
                            "L'astre ne peut être ajouté sur la carte",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        /****************************************************************************************************/

        //Ce sont ici des dependency property sur la plupart des binding qui seront réalisé à l'intérieur même de ce usercontrol.

        public string ImageAstre
        {
            get { return (string)GetValue(ImageAstrePropriete); }
            set { SetValue(ImageAstrePropriete, value); }
        }

        public static readonly DependencyProperty ImageAstrePropriete =
            DependencyProperty.Register("ImageAstre", typeof(string), typeof(UCPopup), new PropertyMetadata("astre.jpg"));

        public string NomAstre
        {
            get { return (string)GetValue(NomAstrePropriete); }
            set { SetValue(NomAstrePropriete, value); }
        }

        public static readonly DependencyProperty NomAstrePropriete =
            DependencyProperty.Register("NomAstre", typeof(string), typeof(UCPopup), new PropertyMetadata("Astre"));

        public string DescriptionAstre
        {
            get { return (string)GetValue(DescriptionAstrePropriete); }
            set { SetValue(DescriptionAstrePropriete, value); }
        }

        public static readonly DependencyProperty DescriptionAstrePropriete =
            DependencyProperty.Register("DescriptionAstre", typeof(string), typeof(UCPopup), new PropertyMetadata("Aucune description pour cet astre."));

        public long AgeAstre
        {
            get { return (long)GetValue(AgeAstrePropriete); }
            set { SetValue(AgeAstrePropriete, value); }
        }

        public static readonly DependencyProperty AgeAstrePropriete =
            DependencyProperty.Register("AgeAstre", typeof(long), typeof(UCPopup), new PropertyMetadata(4500000000));

        public float MasseAstre
        {
            get { return (float)GetValue(MasseAstrePropriete); }
            set { SetValue(MasseAstrePropriete, value); }
        }

        public static readonly DependencyProperty MasseAstrePropriete =
            DependencyProperty.Register("MasseAstre", typeof(float), typeof(UCPopup), new PropertyMetadata(1.0f));

        public int TemperatureAstre
        {
            get { return (int)GetValue(TemperatureAstrePropriete); }
            set { SetValue(TemperatureAstrePropriete, value); }
        }

        public static readonly DependencyProperty TemperatureAstrePropriete =
            DependencyProperty.Register("TemperatureAstre", typeof(int), typeof(UCPopup), new PropertyMetadata(273));

        public string ConstellationEtoile
        {
            get { return (string)GetValue(ConstellationEtoilePropriete); }
            set { SetValue(ConstellationEtoilePropriete, value); }
        }

        public static readonly DependencyProperty ConstellationEtoilePropriete =
            DependencyProperty.Register("ConstellationEtoile", typeof(string), typeof(UCPopup), new PropertyMetadata("Aucune"));

        public TypeEtoile TypeEtoile
        {
            get { return (TypeEtoile)GetValue(TypeEtoilePropriete); }
            set { SetValue(TypeEtoilePropriete, value); }
        }

        public static readonly DependencyProperty TypeEtoilePropriete =
            DependencyProperty.Register("TypeEtoile", typeof(TypeEtoile), typeof(UCPopup), new PropertyMetadata(TypeEtoile.NaineBlanche));

        public float LuminositeEtoile
        {
            get { return (float)GetValue(LuminositeEtoilePropriete); }
            set { SetValue(LuminositeEtoilePropriete, value); }
        }

        public static readonly DependencyProperty LuminositeEtoilePropriete =
            DependencyProperty.Register("LuminositeEtoile", typeof(float), typeof(UCPopup), new PropertyMetadata(1.0f));

        public string SystemePlanete
        {
            get { return (string)GetValue(SystemePlanetePropriete); }
            set { SetValue(SystemePlanetePropriete, value); }
        }

        public static readonly DependencyProperty SystemePlanetePropriete =
            DependencyProperty.Register("SystemePlanete", typeof(string), typeof(UCPopup), new PropertyMetadata("Inconnu"));

        public TypePlanete TypePlanete
        {
            get { return (TypePlanete)GetValue(TypePlanetePropriete); }
            set { SetValue(TypePlanetePropriete, value); }
        }

        public static readonly DependencyProperty TypePlanetePropriete =
            DependencyProperty.Register("TypePlanete", typeof(TypePlanete), typeof(UCPopup), new PropertyMetadata(TypePlanete.Tellurique));

        public bool EauPresentePlanete
        {
            get { return (bool)GetValue(EauPresentePlanetePropriete); }
            set { SetValue(EauPresentePlanetePropriete, value); }
        }

        public static readonly DependencyProperty EauPresentePlanetePropriete =
            DependencyProperty.Register("EauPresentePlanete", typeof(bool), typeof(UCPopup), new PropertyMetadata(false));

        public string PresenceDeViePlanete
        {
            get { return (string)GetValue(PresenceDeViePlanetePropriete); }
            set { SetValue(PresenceDeViePlanetePropriete, value); }
        }

        public static readonly DependencyProperty PresenceDeViePlanetePropriete =
            DependencyProperty.Register("PresenceDeViePlanete", typeof(string), typeof(UCPopup), new PropertyMetadata("Inconnu"));
    }
}
