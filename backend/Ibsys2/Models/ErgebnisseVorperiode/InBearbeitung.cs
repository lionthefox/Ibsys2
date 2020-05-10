using System.Collections.Generic;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class InBearbeitung
    {
        public IList<AuftraegeWarteschlange> AuftraegeInBearbeitung { get; set; }
        
        public InBearbeitung(results lastPeriodResults) {
            foreach (var item in lastPeriodResults.ordersinwork)
            {
              if (item.amount == 0) 
                continue;

              var auInBearbeitung = new AuftraegeWarteschlange
                {
                  Arbeitsplatz = item.id,
                  Fertigungsauftrag = item.order,
                  ErstesLos = item.batch,
                  LetztesLos = item.batch,
                  Periode = item.period,
                  Teil = item.item,
                  Zeitbedarf = item.timeneed,
                  Ruestzeit = 0
                };
                AuftraegeInBearbeitung.Add(auInBearbeitung);
            }       
        }
    }
}