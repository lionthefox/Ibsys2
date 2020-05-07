using System.Collections.Generic;

namespace Ibsys2.Models
{
    public class StuecklistenAufloesung
    {
        public int MatNr { get; set; }
        public TeilTyp Typ { get; set; }
        public Dictionary<int, int> BenoetigteTeile { get; set; }
    }
}