using Espace;
using Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Fonctionnels
{
    /// <summary>
    /// Classe de tests fonctionnels portant sur le manager.
    /// </summary>
    public static class Test_Manager
    {
        //Instanciation d'un manager.
        static Manager manager = new Manager();

        /// <summary>
        /// Méthode de test dans laquelle on vérifie si le manager est bien vide après sa création.
        /// </summary>
        public static void TestCreationManager()
        {
            Console.WriteLine("Le manager après sa création :");
            Console.WriteLine(manager);
            Console.WriteLine("-----------------------------------------------");
        }
        
        /// <summary>
        /// Méthode de test dans laquelle on vient ajouter des astres au manager, les supprimer, et regarder le comportement de celui-ci.
        /// </summary>
        public static void TestAjoutEtSuppressionAstre()
        {
            manager.AjouterUnAstre(new Planete("Terre",
                                    "La Terre est la troisième planète par ordre d'éloignement au Soleil et la cinquième " +
                                    "plus grande du Système solaire aussi bien par la masse que le diamètre. Par ailleurs, elle est le seul " +
                                    "objet céleste connu pour abriter la vie.",
                                    4500000000,
                                    1,
                                    288,
                                    "Oui",
                                    true,
                                    "Solaire",
                                    TypePlanete.Tellurique,
                                    true));

            Console.WriteLine("Le manager après ajout d'un astre :");
            Console.WriteLine(manager);
            Console.WriteLine("-----------------------------------------------");

            manager.AjouterUnAstre(new FabriqueDePlanete().Initialiser("Pluton")
                                       .AvecDescription("Pluton, officiellement désigné par (134340) Pluton, est une planète naine, la plus volumineuse " +
                                       "connue dans le Système solaire, et la deuxième en ce qui concerne sa masse. Après sa découverte par l'astronome " +
                                       "américain Clyde Tombaugh en 1930, Pluton était considérée comme la neuvième planète du Système solaire.")
                                       .AvecAge(4500000000)
                                       .AvecMasse(0.002f)
                                       .AvecTemperature(48)
                                       .PresenceDeVie("Non")
                                       .EstDansLeSysteme("Solaire externe")
                                       .EauEstPresente(false)
                                       .AvecType(TypePlanete.Naine)
                                       .Construire());

            Console.WriteLine("Le manager après ajout d'un second astre :");
            Console.WriteLine(manager);
            Console.WriteLine("-----------------------------------------------");

            manager.SupprimerTout();
            Console.WriteLine("Le manager après supression de tous les astres :");
            Console.WriteLine(manager);
            Console.WriteLine("-----------------------------------------------");
        }

    }
}
