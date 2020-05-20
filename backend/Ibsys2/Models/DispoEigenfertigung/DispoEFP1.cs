using System;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
  public class DispoEFP1
  {
    private readonly List<int> _articleIds = new List<int> {1, 26, 51, 16, 17, 50, 4, 10, 49, 7, 13, 18};
    public List<DispoEFPos> ListDispoEfPos { get; set; } = new List<DispoEFPos>();

    public DispoEFP1(Vertriebswunsch vertriebsWunsch, Forecast forecast, results lastPeriodResults, IList<Artikel> artikelStammdaten, IList<DispoEFPos> updatedDispo = null)
    {
      foreach (var articleId in _articleIds)
      {
        var dispoEfPos = new DispoEFPos
        {
          ArticleId = articleId,
          Name = artikelStammdaten.FirstOrDefault(x => x.Artikelnummer == articleId)?.Bezeichnung,
          NameEng = artikelStammdaten.FirstOrDefault(x => x.Artikelnummer == articleId)?.NameEng
        };

        switch (articleId)
        {
          case 1:
            dispoEfPos.Vertrieb = vertriebsWunsch.Produkt1 + vertriebsWunsch.Direktverkauf.Produkt1.Menge;
            dispoEfPos.AuftragUebernahme = 0;
            break;
          case 26:
          case 51:
            dispoEfPos.Vertrieb = ListDispoEfPos[0].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
            break;
          case 16:
          case 17:
          case 50:
            dispoEfPos.Vertrieb = ListDispoEfPos[2].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
            break;
          case 4:
          case 10:
          case 49:
            dispoEfPos.Vertrieb = ListDispoEfPos[5].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
            break;
          case 7:
          case 13:
          case 18:
            dispoEfPos.Vertrieb = ListDispoEfPos[8].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[8].AuftraegeWarteschlange;
            break;
        }

        dispoEfPos.Sicherheitsbestand = updatedDispo?.FirstOrDefault(x => x.ArticleId == articleId)?.Sicherheitsbestand ?? CalcSicherheitsbestand(forecast, vertriebsWunsch);
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