using System.Collections.Generic;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class WartelisteArbeitsplaetze
    {
        public IList<WartelisteArbeitsplatz> ArbeitsplatzWarteListe = new List<WartelisteArbeitsplatz>();

        public WartelisteArbeitsplaetze(results lastPeriodResults, IList<Artikel> artikelStammdaten)
        {
            foreach (var item in lastPeriodResults.waitinglistworkstations)
            {
                ArbeitsplatzWarteListe.Add(new WartelisteArbeitsplatz(item.id, lastPeriodResults, artikelStammdaten));
            }
        }
    }
}