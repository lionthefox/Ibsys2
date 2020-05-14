namespace Ibsys2.Models.Stammdaten
{
    public class Lieferdaten
    {
        public int Kaufteil { get; set; }
        public decimal MaxLieferzeit { get; set; }
        public int VerwendungP1 { get; set; }
        public int VerwendungP2 { get; set; }
        public int VerwendungP3 { get; set; }
        public int Bestellkosten { get; set; }
        public int Diskontmenge { get; set; }
    }
}