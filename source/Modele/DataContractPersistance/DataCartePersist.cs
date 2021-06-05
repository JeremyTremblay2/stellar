using Espace;
using Geometrie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataContractPersistance
{
    /// <summary>
    /// Classe qui sera persistée lors de la sérialisation de la Carte.
    /// Contient des propriétés qui seront sauvegardées.
    /// </summary>
    [DataContract]
    public class DataCartePersist
    {
        /// <summary>
        /// Propriété concernant la liste de constellations de la carte.
        /// </summary>
        [DataMember]
        public List<Constellation> Constellations { get; set; } = new List<Constellation>();

        /// <summary>
        /// Propriété concernant le dictionnaire de points et d'astres de la Carte.
        /// </summary>
        [DataMember]
        public Dictionary<Point, Astre> Dico { get; set; } = new Dictionary<Point, Astre>();

    }
}
