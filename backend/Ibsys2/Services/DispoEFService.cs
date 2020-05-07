using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;

namespace Ibsys2.Services
{
    public class DispoEfService
    {
        public DispoEFP1 DispoEfp1 { get; set; }
        public DispoEFP2 DispoEfp2 { get; set; }
        public DispoEFP3 DispoEfP3 { get; set; }

        public void Initialize(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults)
        {
            DispoEfp1 = new DispoEFP1(vertriebswunsch, forecast, lastPeriodResults);
            DispoEfp2 = new DispoEFP2(vertriebswunsch, forecast, lastPeriodResults);
            DispoEfP3 = new DispoEFP3(vertriebswunsch, forecast, lastPeriodResults);
        }

        public static int GetLagerbestand(int articleId, results lastPeriodResults)
        {
            foreach (var stockArticle in lastPeriodResults.warehousestock.article)
                if (stockArticle.id == articleId)
                    return stockArticle.amount;
            return 0;
        }

        public static int CalcProduktion(DispoEFPos dispoEfPos)
        {
            var prodMenge = dispoEfPos.Vertrieb + dispoEfPos.AuftragUebernahme + dispoEfPos.Sicherheitsbestand -
                            dispoEfPos.Lagerbestand - dispoEfPos.AuftraegeWarteschlange - dispoEfPos.AuftraegeBearbeitung;
            if (prodMenge < 0)
                prodMenge = 0;
            return prodMenge;
        }
    }
}
