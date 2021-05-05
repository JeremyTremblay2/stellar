using System;
using System.Collections.Generic;
using Modele;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(42, 89);
            Console.WriteLine(p);
            p.Deplacer(100, 200);
            Console.WriteLine(p);

            Segment s = new Segment(new Point(10, 12), new Point(96, 86));
            Console.WriteLine(s);

            List<Astre> lesAstres = new List<Astre>()
            {
                new Etoile("Sirius", 185620, 150, 1500, true, true, null, TypeEtoile.NaineBlanche, "Cassioppée", 3600),
                new Etoile("Bételgeuse", 203, 2, 300, false, true, new Point(45, 79), TypeEtoile.TrouNoir, "Dragon", 4500),
                new Etoile("Z0-x2V", 4, 78, 127, true, false, new Point(32,36), TypeEtoile.SupergeanteRouge, "Grande Ours", 1300),
                new Planete("Terre", 15000000, 1, 15, true, true, new Point(89, 112), TypePlanete.Tellurique, "Oui", true),
                new Planete("Saturne", 332131681, 89, 40, false, false, null, TypePlanete.Gazeuse, "Non", false),
                new Planete("Uranus", 541331819318, 2, -20, false, true, new Point(187, 263), TypePlanete.Naine, "Non", false),
                new Etoile()
                        .AvecNom("Sirius")
                        .AvecAge(45)
            };

            foreach(Astre astre in lesAstres)
            {
                Console.WriteLine(astre);
            }
        }
    }
}
