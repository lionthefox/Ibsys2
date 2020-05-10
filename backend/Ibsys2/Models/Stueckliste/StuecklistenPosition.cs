using System.Diagnostics;

namespace Ibsys2.Models.Stammdaten
{
    [DebuggerDisplay("MatNr = {Matnr}")]
    public class StuecklistenPosition
    {
        public int Matnr { get; set; }
        public int Arbeitsplatz { get; set; }
        public int Teil { get; set; }
        public int Anzahl { get; set; }
    }
}