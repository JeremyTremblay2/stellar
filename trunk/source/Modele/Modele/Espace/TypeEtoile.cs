using System.ComponentModel.DataAnnotations;

namespace Espace
{
    /// <summary>
    /// Enumeration des différents types d'étoiles existantes dans la galaxie.
    /// </summary>
    public enum TypeEtoile
    {
        [Display(Name = "Naine Blanche")]
        NaineBlanche,
        [Display(Name = "Naine Rouge")]
        NaineRouge,
        [Display(Name = "Naine Jaune")]
        NaineJaune,
        [Display(Name = "Naine Noire")]
        NaineNoire,
        [Display(Name = "Géante Rouge")]
        GeanteRouge,
        [Display(Name = "Géante Bleu")]
        GeanteBleu,
        [Display(Name = "Supergéante Rouge")]
        SupergeanteRouge,
        [Display(Name = "Etoile à Neutrons")]
        EtoileANeutrons,
        [Display(Name = "Trou Noir")]
        TrouNoir
    }
}
