using System;
namespace Ibsys2.Models.DispoEigenfertigung
{
    public class DispoEFPos
    {   
        public int ArticleId {get; set;}
        public string Name { get; set; }
        public int Vertrieb { get; set; }
        public int AuftragUebernahme { get; set; }
        public int Sicherheitsbestand { get; set; }
        public int Lagerbestand { get; set; }
        public int AuftraegeWarteschlange { get; set; }
        public int AuftraegeBearbeitung { get; set; }
        public int Produktion  { get; set; }
    }
}
