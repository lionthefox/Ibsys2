using System.Collections.Generic;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class InBearbeitung
    {
        public IList<WarteListePos> AuftraegeInBearbeitung { get; set; }
        
        public InBearbeitung(results lastPeriodResults) {
            foreach (var item in lastPeriodResults.ordersinwork)
            {
                if (item.amount != 0)
                {
                    var auInBearbeitung = new WarteListePos();
                    auInBearbeitung.Arbeitsplatz = item.id;
                    auInBearbeitung.Fertigungsauftrag = item.order;
                    auInBearbeitung.ErstesLos = item.batch;
                    auInBearbeitung.LetztesLos = item.batch;
                    auInBearbeitung.Periode = item.period;
                    auInBearbeitung.Teil = item.item;
                    auInBearbeitung.Zeitbedarf = item.timeneed;
                    auInBearbeitung.Ruestzeit = 0;
                    AuftraegeInBearbeitung.Add(auInBearbeitung);
                }
            }       
        }
    }
}