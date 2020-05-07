using System;
using System.Collections.Generic;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
    public class DispoEfP1
    {
        public List<DispoEFPos> ListDispoEfPos {get; set;} = new List<DispoEFPos>();
        public List<int> ArticleIds = new List<int>{1, 26, 51, 16, 17, 50, 4, 10, 49, 7, 13, 18};

        public DispoEfP1 (Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults){

            foreach (int articleId in ArticleIds)
            {
                var dispoEFPos = new DispoEFPos();
                dispoEFPos.article_id = articleId;
                if (articleId == 1)
                    dispoEFPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
                else if (articleId == 26 || articleId == 51)
                    dispoEFPos.Vertrieb = ListDispoEfPos[0].Produktion;
                else if (articleId == 16 || articleId == 17 || articleId == 50)
                    dispoEFPos.Vertrieb = ListDispoEfPos[2].Produktion;
                else if (articleId == 4 || articleId == 10 || articleId == 49)
                    dispoEFPos.Vertrieb = ListDispoEfPos[5].Produktion;
                else if (articleId == 7 || articleId == 13 || articleId == 18)
                    dispoEFPos.Vertrieb = ListDispoEfPos[8].Produktion;

                if (articleId == 1)
                    dispoEFPos.AuftragUebernahme = 0;
                if (articleId == 26 || articleId == 51)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
                else if (articleId == 16 || articleId == 17 || articleId == 50)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
                else if (articleId == 4 || articleId == 10 || articleId == 49)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
                else if (articleId == 7 || articleId == 13 || articleId == 18)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[8].AuftraegeWarteschlange;
                
                dispoEFPos.Sicherheitsbestand = CalcSicherheitsbestand(forecast, vertriebswunsch);

                dispoEFPos.Lagerbestand = DispoEfService.GetLagerbestand(articleId, lastPeriodResults);

                // Muss noch erweitert werden
                dispoEFPos.AuftraegeBearbeitung = 0;
                dispoEFPos.AuftraegeWarteschlange = 0;

                dispoEFPos.Produktion = DispoEfService.CalcProduktion(dispoEFPos);
                
                ListDispoEfPos.Add(dispoEFPos);
            }
        }

        public int CalcSicherheitsbestand(Forecast forecast, Vertriebswunsch vertriebswunsch)
        {
            return Convert.ToInt32((vertriebswunsch.Produkt1 + forecast.Periode2.Produkt1 + forecast.Periode3.Produkt1 +
                                    forecast.Periode4.Produkt1) / 4 * 0.5);
        }
    }                 
}
