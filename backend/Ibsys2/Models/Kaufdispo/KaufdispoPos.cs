namespace Ibsys2.Models.Kaufdispo
{
    public class KaufdispoPos
    {
        public int MatNr { get; set; }
        public int BedarfPeriode1 { get; set; }
        public int BedarfPeriode2 { get; set; }
        public int BedarfPeriode3 { get; set; }
        public int BedarfPeriode4 { get; set; }
        public int Menge { get; set; }
        public int Bestellart { get; set; }
        public int Lagermenge { get; set; }
        
        public string Liefertermin { get; set; }
    }
}