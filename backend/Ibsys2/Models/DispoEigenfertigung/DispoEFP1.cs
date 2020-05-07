using System;
using System.Collections.Generic;

namespace Ibsys2.Models.DispoEigenfertigung
{
    public class DispoEFP1
    {
        public List<DispoEFPos> dispoEFPos {get; set;} = new List<DispoEFPos>();
        public List<int> articel_ids = List<int>(1, 26, 51, 16, 17, 50, 4, 10, 49, 7, 13, 18);

        public DispoEFP1 (Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults){

            foreach( int articel_id in articel_ids) {
                dispoEFPos = new DispoEFPos();
                dispoEFPos.articel_id = articel_id;
                if (articel_id == 1) {
                    dispoEFPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
                } else {
                }
                this.dispoEFPos.Add(new DispoEFPos());
            }
        }

        public void calcDispo(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults,
            Direktverkauf direktverkauf) {

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
