using System;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models.Stammdaten;
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
      if (updatedDispo != null)
        ListDispoEfPos = new List<DispoEFPos>();
      foreach (var articleId in _articleIds)
      {
        DispoEFPos dispoEfPos;
        
        if (updatedDispo != null)
          dispoEfPos = updatedDispo.First(x => x.ArticleId == articleId);
        else 
          dispoEfPos = new DispoEFPos
          {
            ArticleId = articleId,
            Name = artikelStammdaten.FirstOrDefault(x => x.Artikelnummer == articleId)?.Bezeichnung,
            NameEng = artikelStammdaten.FirstOrDefault(x => x.Artikelnummer == articleId)?.NameEng
          };
        
        Uebernahmemengen(dispoEfPos, vertriebsWunsch, ListDispoEfPos);

        dispoEfPos.Sicherheitsbestand = updatedDispo?.FirstOrDefault(x => x.ArticleId == articleId)?.Sicherheitsbestand ?? CalcSicherheitsbestand(forecast, vertriebsWunsch);
        // Lagerbestand
        switch (articleId)
        {
          case 26:
          case 16:
          case 17:
            dispoEfPos.Lagerbestand = (DispoEfService.GetLagerbestand(articleId, lastPeriodResults) / 3);
            break;
          default:
            dispoEfPos.Lagerbestand = DispoEfService.GetLagerbestand(articleId, lastPeriodResults);
            break;
        }

        if (updatedDispo == null)
        {
          dispoEfPos.AuftraegeBearbeitung = 0;
          dispoEfPos.AuftraegeWarteschlange = 0;
        }

        dispoEfPos.Produktion = DispoEfService.CalcProduktion(dispoEfPos);

        ListDispoEfPos.Add(dispoEfPos);
      }
    }

    public static void Uebernahmemengen(DispoEFPos dispoEfPos, Vertriebswunsch vertriebswunsch,
      IList<DispoEFPos> dispoEfPoses)
    {
      switch (dispoEfPos.ArticleId)
      {
        case 3:
          dispoEfPos.Vertrieb = vertriebswunsch.Produkt3 + vertriebswunsch.Direktverkauf.Produkt3.Menge;
          dispoEfPos.AuftragUebernahme = 0;
          break;
        case 26:
        case 31:
          dispoEfPos.Vertrieb = dispoEfPoses[0].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[0].AuftraegeWarteschlange;
          break;
        case 16:
        case 17:
        case 30:
          dispoEfPos.Vertrieb = dispoEfPoses[2].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[2].AuftraegeWarteschlange;
          break;
        case 6:
        case 12:
        case 29:
          dispoEfPos.Vertrieb = dispoEfPoses[5].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[5].AuftraegeWarteschlange;
          break;
        case 9:
        case 15:
        case 20:
          dispoEfPos.Vertrieb = dispoEfPoses[8].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[8].AuftraegeWarteschlange;
          break;
      }
    }

    public int CalcSicherheitsbestand(Forecast forecast, Vertriebswunsch vertriebswunsch)
    {
      return Convert.ToInt32((vertriebswunsch.Produkt3 + forecast.Periode2.Produkt3 + forecast.Periode3.Produkt3 +
                              forecast.Periode4.Produkt3) / 4 * 0.4);
    }
  }
}