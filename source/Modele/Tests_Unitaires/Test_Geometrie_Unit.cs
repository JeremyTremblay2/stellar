using FluentAssertions;
using Geometrie;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_Unitaires
{
    /// <summary>
    /// Classe de tests permettant de vérifier le fonctionnement des élements de géométrie.
    /// </summary>
    [TestClass]
    public class Test_Geometrie_Unit
    {
        /// <summary>
        /// Méthode de test, permettant de vérifier les méthodes de la classe Point.
        /// </summary>
        [TestMethod]
        public void TestPoint()
        {
            //Déclaration de quelques points
            Point point1 = new Point();
            Point point2 = new Point(3, 5);
            Point point3 = new Point(2, 3);

            //Test du constructeur sans arguments, censé placer le point aux coordonnées (0,0).
            point1.X.Should().Be(0);
            point1.Y.Should().Be(0);

            point2.X.Should().Be(3);
            point2.Y.Should().Be(5);

            //Vérification de la méthode de déplacement.
            point1.Deplacer(2, 3);

            point1.X.Should().Be(2);
            point1.Y.Should().Be(3);

            //Normalement ici, on a point1(2, 3), point2(3,5) et point3(2, 3).
            point1.Equals(point3).Should().BeTrue();
            point1.Equals(point2).Should().BeFalse();
            point1.Deplacer(point1.X, point1.Y - 1);
            point1.Equals(point3).Should().BeFalse();

            point2.Deplacer(1, 1);
            point3.Deplacer(1, 1);

            //Vérification que les hashcode sont bien générés correctement.
            point2.GetHashCode().Should().Be(point3.GetHashCode());
            point2.GetHashCode().Should().NotBe(point1.GetHashCode());
        }

        /// <summary>
        /// Méthode de vérification de la classe Segment.
        /// </summary>
        [TestMethod]
        public void TestSegment()
        {
            //Déclaration de quelques segments
            Segment segment1 = new Segment(new Point(0, 1), new Point(4, 3));
            Segment segment2 = new Segment(new Point(2, 1), new Point(5, 1));
            Segment segment3 = new Segment(new Point(0, 1), new Point(4, 3));
            Segment segment4 = new Segment(new Point(4, 3), new Point(0, 1));

            //Vérification des coordonnées.
            segment1.Point1.Should().Be(new Point(0, 1));
            segment1.Point2.Should().Be(new Point(4, 3));

            segment2.Point1.Should().Be(new Point(2, 1));
            segment2.Point2.Should().Be(new Point(5, 1));

            //Vérification du système d'égalité.
            segment2.Equals(segment1).Should().BeFalse();
            segment2.Equals(segment3).Should().BeFalse();

            segment3.Equals(segment1).Should().BeTrue();
            segment3.Equals(segment4).Should().BeTrue();

            //Vérification du bon fonctionnement du hashcode.
            segment1.GetHashCode().Should().Be(segment3.GetHashCode());
            segment1.GetHashCode().Should().Be(segment4.GetHashCode());

            segment1.GetHashCode().Should().NotBe(segment2.GetHashCode());
            segment2.GetHashCode().Should().NotBe(segment4.GetHashCode());

            //Vérification de la méthode qui indique si un point est contenu dans un segment.
            segment1.PtEquals(new Point(0, 1)).Should().BeTrue();
            segment1.PtEquals(new Point(4, 3)).Should().BeTrue();
            segment1.PtEquals(new Point(2, 1)).Should().BeFalse();

            segment2.PtEquals(new Point(2, 1)).Should().BeTrue();
            segment2.PtEquals(new Point(5, 1)).Should().BeTrue();
            segment2.PtEquals(new Point(0, 1)).Should().BeFalse();

        }
    }
}
