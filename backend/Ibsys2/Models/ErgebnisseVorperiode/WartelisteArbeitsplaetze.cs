using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class WartelisteArbeitsplaetze
    {
        public IList<WartelisteArbeitsplatz> ArbeitsplatzWarteListe = new List<WartelisteArbeitsplatz>();

        public WartelisteArbeitsplaetze(results lastPeriodResults, IList<Artikel> artikelStammdaten,
            IList<ArbeitsplatzNachfolger> arbeitsplaetzeNachfolger, InBearbeitung auftraegeInBearbeitung)
        {
            foreach (var item in lastPeriodResults.waitinglistworkstations)
            {
                if(item.waitinglist != null)
                    ArbeitsplatzWarteListe.Add(new WartelisteArbeitsplatz(item.id, lastPeriodResults, artikelStammdaten));
            }
            // Für die Arbeitsplätze in der WarteListe
            IList<WartelisteArbeitsplatz> warteliste = new List<WartelisteArbeitsplatz>();
            foreach (var element in ArbeitsplatzWarteListe)
            {
                warteliste.Add(element);
            }
            foreach (var element in warteliste)
            {
                foreach (var auftrag in element.ArbeitsplatzWartelisteAuftraege)
                {
                    var arbeitsplatz = arbeitsplaetzeNachfolger.FirstOrDefault(x =>
                        x.Matnr == auftrag.Teil && x.Platz == element.Arbeitsplatz);
                    if (arbeitsplatz.Nachfolger.Count == 0)
                        continue;
                    foreach (var nachfolger in arbeitsplatz.Nachfolger)
                    {
                        var existingArbeitsplatz =
                            ArbeitsplatzWarteListe.FirstOrDefault(x => x.Arbeitsplatz == nachfolger);
                        if (existingArbeitsplatz != null)
                        {
                            var auftragNachfolger = new AuftraegeWarteschlange();
                            auftragNachfolger.Arbeitsplatz = nachfolger;
                            auftragNachfolger.Fertigungsauftrag = auftrag.Fertigungsauftrag;
                            auftragNachfolger.Menge = auftrag.Menge;
                            auftragNachfolger.Periode = auftrag.Periode;
                            auftragNachfolger.Teil = auftrag.Teil;
                            auftragNachfolger.ErstesLos = auftrag.ErstesLos;
                            auftragNachfolger.LetztesLos = auftrag.LetztesLos;
                            auftragNachfolger.Ruestzeit =
                                WartelisteArbeitsplatz.GetRuestzeit(nachfolger, auftrag.Teil,
                                    artikelStammdaten);
                            auftragNachfolger.Zeitbedarf = WartelisteArbeitsplatz.getZeitbedarf(nachfolger,
                                auftrag.Teil, auftrag.Menge, artikelStammdaten);
                            existingArbeitsplatz.ArbeitsplatzWartelisteAuftraege.Add(auftragNachfolger);
                            element.Arbeitszeit += auftragNachfolger.Zeitbedarf;
                            element.Ruestzeit += auftragNachfolger.Ruestzeit;
                        }
                        else
                        {
                            var Arbeitsplatz = new WartelisteArbeitsplatz(nachfolger, auftrag, artikelStammdaten);
                            ArbeitsplatzWarteListe.Add(Arbeitsplatz);
                        }
                    }
                }
            }
        }
    }
}