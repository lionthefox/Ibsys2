using System;
using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;

namespace Ibsys2.Services
{
    public class DispoEFService
    {
        public DispoEFP1 dispoEFP1;
        public DispoEFP2 dispoEFP2;
        public DispoEFP3 dispoEFP3;
        public DispoEFService(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults,
            Direktverkauf direktverkauf, List<StücklistenAuflösung> stücklistenAuflösungen)
        {
            this.dispoEFP1 = new DispoEFP1(vertriebswunsch, forecast, lastPeriodResults);
            this.dispoEFP2 = new DispoEFP2(vertriebswunsch, forecast, lastPeriodResults);
            this.dispoEFP3 = new DispoEFP3(vertriebswunsch, forecast, lastPeriodResults);
        }

        public static int getLagerbestand(int article_id, results lastPeriodResults)
        {
            foreach (resultsWarehousestockArticle stockArticle in lastPeriodResults.warehousestock.article)
                if (stockArticle.id == article_id)
                    return stockArticle.amount;
            return 0;
        }

        public static int calcProduktion(DispoEFPos dispoEfPos)
        {
            return dispoEfPos.Vertrieb + dispoEfPos.AuftragUebernahme + dispoEfPos.Sicherheitsbestand -
                   dispoEfPos.Lagerbestand - dispoEfPos.AuftraegeWarteschlange - dispoEfPos.AuftraegeBearbeitung;
        }
    }
}
