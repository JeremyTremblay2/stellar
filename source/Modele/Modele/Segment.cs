namespace Modele
{
    /// <summary>
    /// Un segment représente les coordonnées de deux points reliés entre eux (liaisons entre les étoiles sur la carte)
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Constructeur de Segment
        /// </summary>
        /// <param name="point1">Coordonnées du premier point du segment</param>
        /// <param name="point2">Coordonnées du deuxième point du segment</param>
        public Segment(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        public override string ToString()
        {
            return $"Segment entre les points {Point1} et {Point2}";
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            if (ReferenceEquals(obj, null) || !GetType().Equals(obj.GetType())) return false;
            Segment seg = (Segment) obj;
            return seg.Point1.Equals(this.Point1) && seg.Point2.Equals(this.Point2);
        }
    }
}
