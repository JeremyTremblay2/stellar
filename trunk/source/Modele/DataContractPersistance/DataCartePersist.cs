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
    [DataContract]
    public class DataCartePersist
    {
        [DataMember]
        public List<Constellation> Constellations { get; set; } = new List<Constellation>();

        [DataMember]
        public Dictionary<Point, Astre> Dico { get; set; } = new Dictionary<Point, Astre>();

    }
}
