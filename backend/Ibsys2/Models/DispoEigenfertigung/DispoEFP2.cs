using System;
using System.Collections.Generic;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
    public class DispoEfP2
    {
        public List<DispoEFPos> ListDispoEfPos {get; set;} = new List<DispoEFPos>();
        public List<int> ArticleIds = new List<int>{2, 26, 56, 16, 17, 55, 5, 11, 54, 8, 14, 19};

        public DispoEfP2 (Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults){

            foreach (int articleId in ArticleIds)
            {
                var dispoEFPos = new DispoEFPos();
                dispoEFPos.article_id = articleId;
                if (articleId == 2)
                    dispoEFPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
                else if (articleId == 26 || articleId == 56)
                    dispoEFPos.Vertrieb = ListDispoEfPos[0].Produktion;
                else if (articleId == 16 || articleId == 17 || articleId == 55)
                    dispoEFPos.Vertrieb = ListDispoEfPos[2].Produktion;
                else if (articleId == 5 || articleId == 11 || articleId == 54)
                    dispoEFPos.Vertrieb = ListDispoEfPos[5].Produktion;
                else if (articleId == 8 || articleId == 14 || articleId == 19)
                    dispoEFPos.Vertrieb = ListDispoEfPos[8].Produktion;
                
                if (articleId == 2)
                    dispoEFPos.AuftragUebernahme = 0;
                if (articleId == 26 || articleId == 56)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
                else if (articleId == 16 || articleId == 17 || articleId == 55)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
                else if (articleId == 5 || articleId == 11 || articleId == 54)
                    dispoEFPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
                else if (articleId == 8 || articleId == 14 || articleId == 19)
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
            return Convert.ToInt32((vertriebswunsch.Produkt2 + forecast.Periode2.Produkt2 + forecast.Periode3.Produkt2 +
                                    forecast.Periode4.Produkt2) / 4 * 0.5);
        }
    } 
}