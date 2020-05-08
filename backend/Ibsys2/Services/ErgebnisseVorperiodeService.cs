using System.Collections.Generic;
using System.ComponentModel.Design;
using Ibsys2.Models;
using Ibsys2.Models.ErgebnisseVorperiode;

namespace Ibsys2.Services
{
    public class ErgebnisseVorperiodeService
    {
        public WartelisteArbeitsplaetze WartelisteArbeitsplaetze { get; set; }
        public InBearbeitung InBearbeitung { get; set; }
        
        public void GetErgebnisse(results lastPeriodResults, IList<Artikel> artikelStammdaten)
        {
            
        }
    }
}