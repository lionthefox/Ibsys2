using System.Collections;
using System.Collections.Generic;

namespace Ibsys2.Models.DispoEigenfertigung
{
    public class DispoEigenfertigungen
    {
        public IList<DispoEFPos> P1 { get; set; }
        public IList<DispoEFPos> P2 { get; set; }
        public IList<DispoEFPos> P3 { get; set; }
    }
}