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
            IList<ArbeitsplatzNachfolger> arbeitsplaetzeNachfolger, InBearbeitung auftraegeInBearbeitung, IList<StuecklistenPosition> stueckliste)
        {
            // Aufträge in der Warteschlange als "Basisaufträge" anlegen
            foreach (var item in lastPeriodResults.waitinglistworkstations)
            {
                if(item.waitinglist != null)
                    ArbeitsplatzWarteListe.Add(new WartelisteArbeitsplatz(item.id, lastPeriodResults, artikelStammdaten));
            }
            
            // Aufträge in der Warteschlange wegen Materialmangel als "Basisauftrag" setzen
            foreach (var missing in lastPeriodResults.waitingliststock)
            {
                if (missing.waitinglist != null)
                {
                    var arbeitsplatzNummer = GetArbeitsplatz(missing.id, lastPeriodResults, stueckliste);
                    var arbeitsplatz = ArbeitsplatzWarteListe.FirstOrDefault(x => x.Arbeitsplatz == arbeitsplatzNummer);
                    if (arbeitsplatz != null)
                    {
                        var auftrag = new AuftraegeWarteschlange();
                        auftrag.Arbeitsplatz = arbeitsplatzNummer;
                        auftrag.Fertigungsauftrag = auftrag.Fertigungsauftrag;
                        auftrag.Menge = auftrag.Menge;
                        auftrag.Periode = auftrag.Periode;
                        auftrag.Teil = auftrag.Teil;
                        auftrag.ErstesLos = auftrag.ErstesLos;
                        auftrag.LetztesLos = auftrag.LetztesLos;
                        auftrag.Ruestzeit =
                            WartelisteArbeitsplatz.GetRuestzeit(arbeitsplatzNummer, auftrag.Teil,
                                artikelStammdaten);
                        auftrag.Zeitbedarf = WartelisteArbeitsplatz.getZeitbedarf(arbeitsplatzNummer,
                            auftrag.Teil, auftrag.Menge, artikelStammdaten);
                        auftrag.BasisAuftrag = true;
                        arbeitsplatz.ArbeitsplatzWartelisteAuftraege.Add(auftrag);
                        arbeitsplatz.Arbeitszeit += auftrag.Zeitbedarf;
                        arbeitsplatz.Ruestzeit += auftrag.Ruestzeit;
                    }
                    else
                    {
                        var Arbeitsplatz = new WartelisteArbeitsplatz(arbeitsplatzNummer, new AuftraegeWarteschlange()
                        {
                            Arbeitsplatz = arbeitsplatzNummer,
                            Zeitbedarf = WartelisteArbeitsplatz.getZeitbedarf(arbeitsplatzNummer, missing.waitinglist.item, missing.waitinglist.amount, artikelStammdaten),
                            BasisAuftrag = true,
                            Fertigungsauftrag = missing.waitinglist.order,
                            ErstesLos = missing.waitinglist.firstbatch,
                            LetztesLos = missing.waitinglist.lastbatch,
                            Menge = missing.waitinglist.amount,
                            Periode = missing.waitinglist.period,
                            Teil = missing.waitinglist.item,
                            Ruestzeit = WartelisteArbeitsplatz.GetRuestzeit(arbeitsplatzNummer, missing.waitinglist.item, artikelStammdaten)
                        }, artikelStammdaten, true);
                        ArbeitsplatzWarteListe.Add(Arbeitsplatz);
                    }
                }
            }

            // Auflösung der Aufträge in der Warteliste
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
                            auftragNachfolger.BasisAuftrag = false;
                            existingArbeitsplatz.ArbeitsplatzWartelisteAuftraege.Add(auftragNachfolger);
                            element.Arbeitszeit += auftragNachfolger.Zeitbedarf;
                            element.Ruestzeit += auftragNachfolger.Ruestzeit;
                        }
                        else
                        {
                            var Arbeitsplatz = new WartelisteArbeitsplatz(nachfolger, auftrag, artikelStammdaten, false);
                            ArbeitsplatzWarteListe.Add(Arbeitsplatz);
                        }
                    }
                }
            }
            
            // Auflösung der Aufträge in Bearbeitung
            IList<WartelisteArbeitsplatz> AuInBearbeitung = new List<WartelisteArbeitsplatz>();
            foreach (var auftrag in auftraegeInBearbeitung.AuftraegeInBearbeitung)
            {
                var arbeitsplatz = arbeitsplaetzeNachfolger.FirstOrDefault(x =>
                    x.Matnr == auftrag.Teil && x.Platz == auftrag.Arbeitsplatz);
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
                        auftragNachfolger.BasisAuftrag = false;
                        existingArbeitsplatz.ArbeitsplatzWartelisteAuftraege.Add(auftragNachfolger);
                        existingArbeitsplatz.Arbeitszeit += auftragNachfolger.Zeitbedarf;
                        existingArbeitsplatz.Ruestzeit += auftragNachfolger.Ruestzeit;
                    }
                    else
                    {
                        var Arbeitsplatz = new WartelisteArbeitsplatz(nachfolger, auftrag, artikelStammdaten, false);
                        ArbeitsplatzWarteListe.Add(Arbeitsplatz);
                    }
                }
            }
        }

        public int GetArbeitsplatz(int missing_id, results lastPeriodResults, IList<StuecklistenPosition> stueckliste)
        {
            var auftrag = lastPeriodResults.waitingliststock.FirstOrDefault(x => x.id == missing_id);
            int arbeitsplatz = stueckliste.FirstOrDefault(x => x.Matnr == auftrag.waitinglist.item && x.Teil == missing_id).Arbeitsplatz;
            return arbeitsplatz;

        }
    }
}