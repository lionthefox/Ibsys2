using System;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;

namespace Ibsys2.Services
{
    public class DispoEFService
    {
        public DispoEFService()
        {
        }

        public void Dispo(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults,
            Direktverkauf direktverkauf)
        {
            // Berechnung P1
            var dispoEFP1 = new DispoEFP1();
            dispoEFP1.P1.Vertrieb = vertriebswunsch.Produkt1 + direktverkauf.Produkt1.Menge;

            // Der Sicherheitsbestand sollte noch nach der Formel aus dem kompletten Forecast ermittelt werden
            dispoEFP1.P1.Sicherheitsbestand = Convert.ToInt32(vertriebswunsch.Produkt1 * 0.5);
            dispoEFP1.P1.Lagerbestand = getLagerbestand(1, lastPeriodResults);

            // Aufträge in Bearbeitung bzw. in Warteschlange fehlt noch
            dispoEFP1.P1.AuftraegeWarteschlange = 0;
            dispoEFP1.P1.AuftraegeBearbeitung = 0;

            dispoEFP1.P1.ProduktionPeriode = dispoEFP1.P1.Vertrieb + dispoEFP1.P1.Sicherheitsbestand -
                                             dispoEFP1.P1.Lagerbestand - dispoEFP1.P1.AuftraegeWarteschlange -
                                             dispoEFP1.P1.AuftraegeBearbeitung;
            
            //
        }

        public int getLagerbestand(int article_id, results lastPeriodResults)
        {
            foreach (resultsWarehousestockArticle stockArticle in lastPeriodResults.warehousestock.article)
                if (stockArticle.id == article_id)
                    return stockArticle.amount;
            return 0;
        }
    }
}
