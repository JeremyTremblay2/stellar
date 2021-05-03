using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Enumeration des différents types d'étoiles existantes dans la galaxie.
    /// </summary>
    public enum TypeEtoile
    {
        [EnumMember(Value = "Naine Rouge")]
        NaineRouge,
        [EnumMember(Value = "Naine Jaune")]
        NaineJaune,
        [EnumMember(Value = "Naine Blanche")]
        NaineBlanche,
        [EnumMember(Value = "Naine Noire")]
        NaineNoire,
        [EnumMember(Value = "Géante Rouge")]
        GeanteRouge,
        [EnumMember(Value = "Géante Bleu")]
        GeanteBleu,
        [EnumMember(Value = "Supergéante Rouge")]
        SupergeanteRouge,
        [EnumMember(Value = "Etoile à Neutrons")]
        EtoileANeutrons,
        [EnumMember(Value = "Trou Noir")]
        TrouNoir
    }
}
