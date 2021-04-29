using StellarModele;
using System;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Astre astre = new Astre("UnAstre", 200, 2, 3600, false, true);
            Console.WriteLine(astre);
        }
    }
}
