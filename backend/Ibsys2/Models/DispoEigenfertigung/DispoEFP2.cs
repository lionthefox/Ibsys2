using System;
using System.Collections.Generic;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
  public class DispoEFP2
  {
    public List<DispoEFPos> ListDispoEfPos { get; set; } = new List<DispoEFPos>();
    public List<int> ArticleIds = new List<int> {2, 26, 56, 16, 17, 55, 5, 11, 54, 8, 14, 19};

    public DispoEFP2(Vertriebswunsch vertriebswunsch, Forecast forecast, results lastPeriodResults)
    {
      foreach (var articleId in ArticleIds)
      {
        var dispoEfPos = new DispoEFPos {ArticleId = articleId};
        if (articleId == 2)
          dispoEfPos.Vertrieb = vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge;
        else if (articleId == 26 || articleId == 56)
          dispoEfPos.Vertrieb = ListDispoEfPos[0].Produktion;
        else if (articleId == 16 || articleId == 17 || articleId == 55)
          dispoEfPos.Vertrieb = ListDispoEfPos[2].Produktion;
        else if (articleId == 5 || articleId == 11 || articleId == 54)
          dispoEfPos.Vertrieb = ListDispoEfPos[5].Produktion;
        else if (articleId == 8 || articleId == 14 || articleId == 19)
          dispoEfPos.Vertrieb = ListDispoEfPos[8].Produktion;

        if (articleId == 2)
          dispoEfPos.AuftragUebernahme = 0;
        if (articleId == 26 || articleId == 56)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
        else if (articleId == 16 || articleId == 17 || articleId == 55)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
        else if (articleId == 5 || articleId == 11 || articleId == 54)
          dispoEfPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
        else if (articleId == 8 || articleId == 14 || articleId == 19)
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
      return Convert.ToInt32((vertriebswunsch.Produkt2 + forecast.Periode2.Produkt2 + forecast.Periode3.Produkt2 +
                              forecast.Periode4.Produkt2) / 4 * 0.5);
    }
  }
}