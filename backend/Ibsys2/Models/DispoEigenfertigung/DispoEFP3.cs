using System;
using System.Collections.Generic;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
   public class DispoEFP3
    {
        public List<DispoEFPos> listDispoEFPos {get; set;} = new List<DispoEFPos>();
        public List<int> articel_ids = new List<int>{2, 26, 56, 16, 17, 55, 5, 11, 54, 8, 14, 19};

        public DispoEFP3 (Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults){

            foreach (int article_id in articel_ids)
            {
                var dispoEFPos = new DispoEFPos();
                dispoEFPos.article_id = article_id;
                if (article_id == 3)
                    dispoEFPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
                else if (article_id == 26 || article_id == 31)
                    dispoEFPos.Vertrieb = listDispoEFPos[0].Produktion;
                else if (article_id == 16 || article_id == 17 || article_id == 30)
                    dispoEFPos.Vertrieb = listDispoEFPos[2].Produktion;
                else if (article_id == 6 || article_id == 12 || article_id == 29)
                    dispoEFPos.Vertrieb = listDispoEFPos[5].Produktion;
                else if (article_id == 9 || article_id == 15 || article_id == 20)
                    dispoEFPos.Vertrieb = listDispoEFPos[8].Produktion;

                if (article_id == 3)
                    dispoEFPos.AuftragUebernahme = 0;
                else if (article_id == 26 || article_id == 31)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[0].AuftraegeWarteschlange;
                else if (article_id == 16 || article_id == 17 || article_id == 30)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[2].AuftraegeWarteschlange;
                else if (article_id == 6 || article_id == 12 || article_id == 29)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[5].AuftraegeWarteschlange;
                else if (article_id == 9 || article_id == 15 || article_id == 20)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[8].AuftraegeWarteschlange;
                
                dispoEFPos.Sicherheitsbestand = calcSicherheitsbestand(forecast, vertriebswunsch);

                dispoEFPos.Lagerbestand = DispoEFService.getLagerbestand(article_id, lastPeriodResults);

                // Muss noch erweitert werden
                dispoEFPos.AuftraegeBearbeitung = 0;
                dispoEFPos.AuftraegeWarteschlange = 0;
                
                dispoEFPos.Produktion = DispoEFService.calcProduktion(dispoEFPos);
                
                listDispoEFPos.Add(dispoEFPos);
            }
        }

        public int calcSicherheitsbestand(Forecast forecast, Vertriebswunsch vertriebswunsch)
        {
            return Convert.ToInt32((vertriebswunsch.Produkt3 + forecast.Periode2.Produkt3 + forecast.Periode3.Produkt3 +
                                    forecast.Periode4.Produkt3) / 4 * 0.5);
        }
    } 
}