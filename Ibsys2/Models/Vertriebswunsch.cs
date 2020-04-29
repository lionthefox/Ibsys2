namespace Ibsys2.Models
{
    public class Vertriebswunsch
    {
        public int Produkt1 { get; set; }
        public int Produkt2 { get; set; }
        public int Produkt3 { get; set; }
        public Direktverkauf Direktverkauf { get; set; }
    }
}