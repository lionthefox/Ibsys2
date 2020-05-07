using System;
using System.Collections.Generic;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
  public class DispoEFP1
  {
    public List<DispoEFPos> ListDispoEfPos { get; set; } = new List<DispoEFPos>();
    public List<int> ArticleIds = new List<int> {1, 26, 51, 16, 17, 50, 4, 10, 49, 7, 13, 18};

    public DispoEFP1(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults)
    {
      foreach (var articleId in ArticleIds)
      {
        var dispoEfPos = new DispoEFPos {ArticleId = articleId};
        if (articleId == 1)
          dispoEfPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
        else if (articleId == 26 || articleId == 51)
          dispoEfPos.Vertrieb = ListDispoEfPos[0].Produktion;
        else if (articleId == 16 || articleId == 17 || articleId == 50)
          dispoEfPos.Vertrieb = ListDispoEfPos[2].Produktion;
        else if (articleId == 4 || articleId == 10 || articleId == 49)
          dispoEfPos.Vertrieb = ListDispoEfPos[5].Produktion;
        else if (articleId == 7 || articleId == 13 || articleId == 18)
          dispoEfPos.Vertrieb = ListDispoEfPos[8].Produktion;

        if (articleId == 1)
          dispoEfPos.AuftragUebernahme = 0;
        if (articleId == 26 || articleId == 51)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
        else if (articleId == 16 || articleId == 17 || articleId == 50)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
        else if (articleId == 4 || articleId == 10 || articleId == 49)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
        else if (articleId == 7 || articleId == 13 || articleId == 18)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[8].AuftraegeWarteschlange;

        dispoEfPos.Sicherheitsbestand = CalcSicherheitsbestand(forecast, vertriebswunsch);

        dispoEfPos.Lagerbestand = DispoEfService.GetLagerbestand(articleId, lastPeriodResults);

        // Muss noch erweitert werden
        dispoEfPos.AuftraegeBearbeitung = 0;
        dispoEfPos.AuftraegeWarteschlange = 0;

        dispoEfPos.Produktion = DispoEfService.CalcProduktion(dispoEfPos);

        ListDispoEfPos.Add(dispoEfPos);
      }
    }

    public int CalcSicherheitsbestand(Forecast forecast, Vertriebswunsch vertriebswunsch)
    {
      return Convert.ToInt32((vertriebswunsch.Produkt1 + forecast.Periode2.Produkt1 + forecast.Periode3.Produkt1 +
                              forecast.Periode4.Produkt1) / 4 * 0.5);
    }
  }
}