using System;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
  public class DispoEFP3
  {
    private readonly List<int> _articleIds = new List<int> {3, 26, 31, 16, 17, 30, 6, 12, 29, 9, 15, 20 };
    public List<DispoEFPos> ListDispoEfPos { get; set; } = new List<DispoEFPos>();

    public DispoEFP3(Vertriebswunsch vertriebsWunsch, Forecast forecast, results lastPeriodResults,
        IList<Artikel> artikelStammdaten, IList<DispoEFPos> updatedDispo = null)
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
          case 3:
            dispoEfPos.Vertrieb = vertriebsWunsch.Produkt3 + vertriebsWunsch.Direktverkauf.Produkt3.Menge;
            dispoEfPos.AuftragUebernahme = 0;
            break;
          case 26:
          case 31:
            dispoEfPos.Vertrieb = ListDispoEfPos[0].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
            break;
          case 16:
          case 17:
          case 30:
            dispoEfPos.Vertrieb = ListDispoEfPos[2].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
            break;
          case 6:
          case 12:
          case 29:
            dispoEfPos.Vertrieb = ListDispoEfPos[5].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
            break;
          case 9:
          case 15:
          case 20:
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
      return Convert.ToInt32((vertriebswunsch.Produkt3 + forecast.Periode2.Produkt3 + forecast.Periode3.Produkt3 +
                              forecast.Periode4.Produkt3) / 4 * 0.5);
    }
  }
}