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
            var dispoEFP1 = new DispoEFP1();
            var dispoEFP2 = new DispoEFP2();
            var dispoEFP3 = new DispoEFP3();
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
