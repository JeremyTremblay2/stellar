using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Geometrie
{   
    /// <summary>
    /// Un point représente les coordonnées (x et y) d'un point sur la carte (l'emplacement des astres).
    /// </summary>
    public class Point : IEquatable<Point>, INotifyPropertyChanged
    {
        private string couleur;
        /// <summary>
        /// Propriété contenant la position en abscisses de x, sous forme d'une valeur entière.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Propriété contenant la position en ordonnées de y, sous forme d'une valeur entière.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Propriété concernant la couleur d'un point, sous forme de chaîne de caractères, en anglais, ou en hexadécimal.
        /// </summary>
        public string Couleur
        {
            get => couleur;
            set
            {
                if (couleur == value) return;
                couleur = value;
                OnPropertyChanged(nameof(Couleur));
            }
        }

        /// <summary>
        /// Constructeur de Point, permet d'initialiser un point aux coordonnées 0,0. La couleur par défaut est jaune.
        /// </summary>
        public Point() : this(0, 0) {   }

        /// <summary>
        /// Constructeur de Point, permet d'initialiser un point avec des coordonnées. La couleur par défaut est jaune.
        /// </summary>
        /// <param name="x">Coordonnées en abscisses.</param>
        /// <param name="y">Coordonnées en ordonnées.</param>
        public Point(int x, int y)
        {
            Deplacer(x, y);
            Couleur = "Yellow";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string nomPropriete)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));

        /// <summary>
        /// Permet de déplacer un point à de nouvelles coordonnées.
        /// </summary>
        /// <param name="x">Nouvelle coordonnée en abscisse qui sera affectée à x</param>
        /// <param name="y">Nouvelle coordonnée en ordonnée qui sera affectée à y</param>
        public void Deplacer(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Permet d'afficher un point sous forme d'une paire de coordonées (x; y).
        /// </summary>
        /// <returns>Retourne la chaîne de caractère représentant les coordonées du point.</returns>
        public override string ToString()
        {
            return $"({X}; {Y})";
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un point passé en paramètre est égal à this, donc s'il possède les mêmes
        /// coordonnées en abscisses et en ordonnées (x et y).
        /// </summary>
        /// <param name="autre">Un point que l'on souhaite comparer à this.</param>
        /// <returns>Un booléen qui indique si le point passé en paramètre est le même que this ou non.</returns>
        public bool Equals([AllowNull] Point autre)
        {
            return X.Equals(autre.X) && Y.Equals(autre.Y);
        }

        /// <summary>
        /// Protocole d'égalité permettant de savoir si un objet passé en paramètre est un Point.
        /// Si cette vérification est faite avec succès, alors on vérifie ensuite si ce point est égal à this, donc s'il possède les mêmes
        /// coordonnées en abscisses et en ordonnées (x et y).
        /// </summary>
        /// <param name="obj">Un objet quelconque, dont on veut déterminer s'il s'agit d'un point (et s'il s'agit du même point que this)</param>
        /// <returns>Un booléen qui indique si l'objet passé en paramètre est le même que this ou non.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;

            return Equals(obj as Point);
        }

        /// <summary>
        /// Permet la génération d'un HashCode, utilisé dans le cas des dictionnaires.
        /// Ce hascode est définit par les coordonnées du point, en abscisses (x) et en ordonnées (y).
        /// </summary>
        /// <returns>Un entier représentant le hashcode de ce point.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}
