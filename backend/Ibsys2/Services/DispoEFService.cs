using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;

namespace Ibsys2.Services
{
    public class DispoEfService
    {
        public DispoEfP1 DispoEfP1 { get; set; }
        public DispoEfP2 DispoEfP2 { get; set; }
        public DispoEFP3 DispoEfP3 { get; set; }

        public void Initialize(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults)
        {
            DispoEfP1 = new DispoEfP1(vertriebswunsch, forecast, lastPeriodResults);
            DispoEfP2 = new DispoEfP2(vertriebswunsch, forecast, lastPeriodResults);
            DispoEfP3 = new DispoEFP3(vertriebswunsch, forecast, lastPeriodResults);
        }

        public static int GetLagerbestand(int article_id, results lastPeriodResults)
        {
            foreach (resultsWarehousestockArticle stockArticle in lastPeriodResults.warehousestock.article)
                if (stockArticle.id == article_id)
                    return stockArticle.amount;
            return 0;
        }

        public static int CalcProduktion(DispoEFPos dispoEfPos)
        {
            int prodMenge = dispoEfPos.Vertrieb + dispoEfPos.AuftragUebernahme + dispoEfPos.Sicherheitsbestand -
                            dispoEfPos.Lagerbestand - dispoEfPos.AuftraegeWarteschlange - dispoEfPos.AuftraegeBearbeitung;
            if (prodMenge < 0)
                prodMenge = 0;
            return prodMenge;
        }
    }
}
