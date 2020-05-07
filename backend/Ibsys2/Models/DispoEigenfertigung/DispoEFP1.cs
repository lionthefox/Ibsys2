using System;
using System.Collections.Generic;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing.Matching;
namespace Ibsys2.Models.DispoEigenfertigung
{
    public class DispoEFP1
    {
        public List<DispoEFPos> listDispoEFPos {get; set;} = new List<DispoEFPos>();
        public List<int> articel_ids = new List<int>{1, 26, 51, 16, 17, 50, 4, 10, 49, 7, 13, 18};

        public DispoEFP1 (Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults){

            foreach (int article_id in articel_ids)
            {
                var dispoEFPos = new DispoEFPos();
                dispoEFPos.article_id = article_id;
                if (article_id == 1)
                    dispoEFPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
                else if (article_id == 26 || article_id == 51)
                    dispoEFPos.Vertrieb = listDispoEFPos[0].Produktion;
                else if (article_id == 16 || article_id == 17 || article_id == 50)
                    dispoEFPos.Vertrieb = listDispoEFPos[2].Produktion;
                else if (article_id == 4 || article_id == 10 || article_id == 49)
                    dispoEFPos.Vertrieb = listDispoEFPos[5].Produktion;
                else if (article_id == 7 || article_id == 13 || article_id == 18)
                    dispoEFPos.Vertrieb = listDispoEFPos[8].Produktion;

                if (article_id == 1)
                    dispoEFPos.AuftragUebernahme = 0;
                if (article_id == 26 || article_id == 51)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[0].AuftraegeWarteschlange;
                else if (article_id == 16 || article_id == 17 || article_id == 50)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[2].AuftraegeWarteschlange;
                else if (article_id == 4 || article_id == 10 || article_id == 49)
                    dispoEFPos.AuftragUebernahme = listDispoEFPos[5].AuftraegeWarteschlange;
                else if (article_id == 7 || article_id == 13 || article_id == 18)
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
            return Convert.ToInt32((vertriebswunsch.Produkt1 + forecast.Periode2.Produkt1 + forecast.Periode3.Produkt1 +
                                    forecast.Periode4.Produkt1) / 4 * 0.5);
        }
    }                 
}
