using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Test
    {
        static void Main(string[] args)
        {
            Test_Astres test_Astres = new Test_Astres();
            test_Astres.Test();
            Test_Constellation test_Constellation = new Test_Constellation();
            test_Constellation.TestRelier();
            test_Constellation.TestSupprimer();
            
        }
    }
}
