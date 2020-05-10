using System.Collections.Generic;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Models
{
    public class StuecklistenAufloesung
    {
        public int MatNr { get; set; }
        public TeilTyp Typ { get; set; }
        public int Arbeitsplatz { get; set; }
        public IList<Teil> BenoetigteTeile { get; set; }
    }
}