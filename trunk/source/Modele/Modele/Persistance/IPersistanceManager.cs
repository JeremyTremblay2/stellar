using Espace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public interface IPersistanceManager
    {
        IEnumerable<Astre> ChargeDonnees();

        void SauvegardeDonnees(IEnumerable<Astre> astres);
    }
}
