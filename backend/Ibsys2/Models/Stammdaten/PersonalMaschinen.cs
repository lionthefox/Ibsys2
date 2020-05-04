namespace Ibsys2.Models.Stammdaten
{
    public class PersonalMaschinen
    {
        public int Arbeitsplatz { get; set; }
        public decimal LohnSchicht1 { get; set; }
        public decimal LohnSchicht2 { get; set; }
        public decimal LohnSchicht3 { get; set; }
        public decimal LohnÜberstunden { get; set; }
        public decimal VariableMaschinenkosten { get; set; }
        public decimal FixeMaschinenkosten { get; set; }
    }
}