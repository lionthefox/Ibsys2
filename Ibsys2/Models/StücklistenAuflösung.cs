using System.Collections.Generic;

namespace Ibsys2.Models
{
    public class StücklistenAuflösung
    {
        public int MatNr { get; set; }
        public TeilTyp Typ { get; set; }
        public Dictionary<int, int> BenötigteTeile { get; set; }
    }
}