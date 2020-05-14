using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Services
{
    public class ErgebnisseVorperiodeService
    {
        public WartelisteArbeitsplaetze WartelisteArbeitsplaetze { get; set; }
        public InBearbeitung InBearbeitung { get; set; }

        public BenoetigteTeile BenoetigteTeile { get; set; }

        public void GetErgebnisse(results lastPeriodResults, IList<Artikel> artikelStammdaten,
            IList<ArbeitsplatzNachfolger> arbeitsplatzNachfolger, IList<StuecklistenPosition> stueckliste)
        {
            InBearbeitung = new InBearbeitung(lastPeriodResults);
            WartelisteArbeitsplaetze = new WartelisteArbeitsplaetze(lastPeriodResults, artikelStammdaten,
                arbeitsplatzNachfolger, InBearbeitung, stueckliste);
            BenoetigteTeile = new BenoetigteTeile(WartelisteArbeitsplaetze, arbeitsplatzNachfolger, artikelStammdaten,
                stueckliste);
        }
    }
}