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
    /// Logique d'interaction pour UCPopup.xaml
    /// </summary>
    public partial class UCPopup : UserControl
    {
        public Manager LeManager => (Application.Current as App).LeManager;

        public UCPopup()
        {
            InitializeComponent();
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

        private void buttonPopupClicCroix(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Popup.Visibility = Visibility.Hidden;
        }

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
