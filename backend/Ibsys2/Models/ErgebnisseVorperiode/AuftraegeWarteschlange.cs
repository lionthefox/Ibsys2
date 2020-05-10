namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class AuftraegeWarteschlange
    {
        public int Arbeitsplatz { get; set; }
        public int Periode { get; set; }
        public int Fertigungsauftrag { get; set; }
        public int ErstesLos { get; set; }
        public int LetztesLos { get; set; }
        public int Teil { get; set; }
        public int Menge { get; set; }
        public int Zeitbedarf { get; set; }
        public int Ruestzeit { get; set; }
        
    }
}