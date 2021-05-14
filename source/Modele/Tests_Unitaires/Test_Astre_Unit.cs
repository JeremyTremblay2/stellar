using Espace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests_Unitaires
{
    [TestClass]
    public class Test_Astre_Unit
    {
        [TestMethod]
        public void TestCreationAstres()
        {
            var lesEtoiles = new List<Etoile>()
            {
                 new Etoile("Sirius", 
                            "Sirius, également appelée Alpha Canis Majoris (α Canis Majoris/α CMa) par la " +
                            "désignation de Bayer, est l'étoile principale de la constellation du Grand Chien. Vue de la " +
                            "Terre, Sirius est l'étoile la plus brillante du ciel après le Soleil.",
                            250000000, 
                            1.03f,
                            9900,
                            false,
                            TypeEtoile.NaineBlanche,
                            "Grand Chien",
                            26.1f),

                 new Etoile("Soleil",
                            "Le Soleil est l’étoile du Système solaire. Dans la classification astronomique, " +
                            "c’est une étoile de type naine jaune d'une masse d'environ 1,989 1 × 1030 kg, composée d’hydrogène et d’hélium.",
                            460000000,
                            1f,
                            5773,
                            false,
                            TypeEtoile.NaineJaune,
                            "Aucune",
                            1),

                 new Etoile("Bételgeuse",
                            "Bételgeuse (α Orionis) est une étoile variable semi-régulière de type supergéante " +
                            "rouge, dans la constellation d’Orion, située à une distance très difficile à établir.",
                            8000000,
                            15f,
                            3600,
                            false,
                            TypeEtoile.SupergeanteRouge,
                            "Orion",
                            17),
            };

            foreach(var etoile in lesEtoiles)
            {
                etoile.SePresenter();
            }
        }
    }
}
