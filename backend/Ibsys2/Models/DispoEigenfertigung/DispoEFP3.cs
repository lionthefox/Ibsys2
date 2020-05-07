using System;
using System.Collections.Generic;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
  public class DispoEFP3
  {
    public List<DispoEFPos> ListDispoEfPos { get; set; } = new List<DispoEFPos>();
    public List<int> ArticleIds = new List<int> {3, 26, 31, 16, 17, 30, 6, 12, 29, 9, 15, 20};

    public DispoEFP3(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults)
    {
      foreach (var articleId in ArticleIds)
      {
        var dispoEfPos = new DispoEFPos {ArticleId = articleId};
        if (articleId == 3)
          dispoEfPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
        else if (articleId == 26 || articleId == 31)
          dispoEfPos.Vertrieb = ListDispoEfPos[0].Produktion;
        else if (articleId == 16 || articleId == 17 || articleId == 30)
          dispoEfPos.Vertrieb = ListDispoEfPos[2].Produktion;
        else if (articleId == 6 || articleId == 12 || articleId == 29)
          dispoEfPos.Vertrieb = ListDispoEfPos[5].Produktion;
        else if (articleId == 9 || articleId == 15 || articleId == 20)
          dispoEfPos.Vertrieb = ListDispoEfPos[8].Produktion;

        if (articleId == 3)
          dispoEfPos.AuftragUebernahme = 0;
        else if (articleId == 26 || articleId == 31)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
        else if (articleId == 16 || articleId == 17 || articleId == 30)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
        else if (articleId == 6 || articleId == 12 || articleId == 29)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
        else if (articleId == 9 || articleId == 15 || articleId == 20)
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
      return Convert.ToInt32((vertriebswunsch.Produkt3 + forecast.Periode2.Produkt3 + forecast.Periode3.Produkt3 +
                              forecast.Periode4.Produkt3) / 4 * 0.5);
    }
  }
}