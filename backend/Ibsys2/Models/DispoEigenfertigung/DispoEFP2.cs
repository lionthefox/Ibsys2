using System;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Services;

namespace Ibsys2.Models.DispoEigenfertigung
{
  public class DispoEFP2
  {
    private readonly List<int> _articleIds = new List<int> {2, 26, 56, 16, 17, 55, 5, 11, 54, 8, 14, 19};
    public List<DispoEFPos> ListDispoEfPos { get; set; } = new List<DispoEFPos>();

    public DispoEFP2(Vertriebswunsch vertriebsWunsch, Forecast forecast, results lastPeriodResults,
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
          case 2:
            dispoEfPos.Vertrieb = vertriebsWunsch.Produkt2 + vertriebsWunsch.Direktverkauf.Produkt2.Menge;
            dispoEfPos.AuftragUebernahme = 0;
            break;
          case 26:
          case 56:
            dispoEfPos.Vertrieb = ListDispoEfPos[0].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[0].AuftraegeWarteschlange;
            break;
          case 16:
          case 17:
          case 55:
            dispoEfPos.Vertrieb = ListDispoEfPos[2].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[2].AuftraegeWarteschlange;
            break;
          case 5:
          case 11:
          case 54:
            dispoEfPos.Vertrieb = ListDispoEfPos[5].Produktion;
            dispoEfPos.AuftragUebernahme = ListDispoEfPos[5].AuftraegeWarteschlange;
            break;
          case 8:
          case 14:
          case 19:
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
      return Convert.ToInt32((vertriebswunsch.Produkt2 + forecast.Periode2.Produkt2 + forecast.Periode3.Produkt2 +
                              forecast.Periode4.Produkt2) / 4 * 0.5);
    }
  }
}