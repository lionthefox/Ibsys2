using System.Collections.Generic;

namespace Ibsys2.Models.Stueckliste
{
    public class ArbeitsplatzNachfolger
    {
        public int Matnr { get; set; }
        public int Platz { get; set; }
        public IList<int> Nachfolger { get; set; }
    }
}