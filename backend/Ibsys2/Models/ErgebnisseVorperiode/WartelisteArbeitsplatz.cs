using System.Collections.Generic;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class WartelisteArbeitsplatz
    {
        public IList<WarteListePos> ArbeitsplatzWarteliste = new List<WarteListePos>();
        public int Arbeitszeit { get; set; }
        public int Arbeitsplatz { get; set; }
        public int Ruestzeit { get; set; }

        public WartelisteArbeitsplatz(int arbeitsplatz_id, results lastPeriodResults, IList<Artikel> artikelStammdaten)
        {
            foreach (var item in lastPeriodResults.waitinglistworkstations)
            {
                if (item.id == arbeitsplatz_id)
                {
                    this.Arbeitszeit = item.timeneed;
                    this.Arbeitsplatz = item.id;
                    foreach (var teil in item.waitinglist)
                    {
                        var WarteListeTeil = new WarteListePos();
                        WarteListeTeil.Arbeitsplatz = arbeitsplatz_id;
                        WarteListeTeil.Fertigungsauftrag = teil.order;
                        WarteListeTeil.ErstesLos = teil.firstbatch;
                        WarteListeTeil.LetztesLos = teil.lastbatch;
                        WarteListeTeil.Periode = teil.period;
                        WarteListeTeil.Teil = teil.item;
                        WarteListeTeil.Zeitbedarf = teil.timeneed;
                        WarteListeTeil.Ruestzeit = getRuestzeit(arbeitsplatz_id, teil.item, artikelStammdaten);
                        ArbeitsplatzWarteliste.Add(WarteListeTeil);   
                    }
                }
            }
        }

        public int getRuestzeit(int arbeitsplatz_id, int MatNr, IList<Artikel> artikelStammdaten)
        {
            foreach (var artikel in artikelStammdaten)
            {
                if (artikel.Artikelnummer == MatNr)
                {
                    switch (arbeitsplatz_id)
                    {
                        case 1:
                            return artikel.RüstzeitPlatz1 ?? 0;
                        case 2:
                            return artikel.RüstzeitPlatz2 ?? 0;
                        case 3:
                            return artikel.RüstzeitPlatz3 ?? 0;
                        case 4:
                            return artikel.RüstzeitPlatz4 ?? 0;
                        case 6:
                            return artikel.RüstzeitPlatz6 ?? 0;
                        case 7:
                            return artikel.RüstzeitPlatz7 ?? 0;
                        case 8:
                            return artikel.RüstzeitPlatz8 ?? 0;
                        case 9:
                            return artikel.RüstzeitPlatz9 ?? 0;
                        case 10:
                            return artikel.RüstzeitPlatz10 ?? 0;
                        case 11:
                            return artikel.RüstzeitPlatz11 ?? 0;
                        case 12:
                            return artikel.RüstzeitPlatz12 ?? 0;
                        case 13:
                            return artikel.RüstzeitPlatz13 ?? 0;
                        case 14:
                            return artikel.RüstzeitPlatz14 ?? 0;
                        case 15:
                            return artikel.RüstzeitPlatz15 ?? 0;
                    }
                }
            }

            return 0;
        }
    }
}