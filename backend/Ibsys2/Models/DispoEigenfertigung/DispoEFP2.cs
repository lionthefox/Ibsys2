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

    public static void Uebernahmemengen(DispoEFPos dispoEfPos, Vertriebswunsch vertriebswunsch, IList<DispoEFPos> dispoEfPoses)
    {
      switch (dispoEfPos.ArticleId)
      {
        case 2:
          dispoEfPos.Vertrieb = vertriebswunsch.Produkt2 + vertriebswunsch.Direktverkauf.Produkt2.Menge;
          dispoEfPos.AuftragUebernahme = 0;
          break;
        case 26:
        case 56:
          dispoEfPos.Vertrieb = dispoEfPoses[0].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[0].AuftraegeWarteschlange;
          break;
        case 16:
        case 17:
        case 55:
          dispoEfPos.Vertrieb = dispoEfPoses[2].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[2].AuftraegeWarteschlange;
          break;
        case 5:
        case 11:
        case 54:
          dispoEfPos.Vertrieb = dispoEfPoses[5].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[5].AuftraegeWarteschlange;
          break;
        case 8:
        case 14:
        case 19:
          dispoEfPos.Vertrieb = dispoEfPoses[8].Produktion;
          dispoEfPos.AuftragUebernahme = dispoEfPoses[8].AuftraegeWarteschlange;
          break;
      }
    }

    public int CalcSicherheitsbestand(Forecast forecast, Vertriebswunsch vertriebswunsch)
    {
      return Convert.ToInt32((vertriebswunsch.Produkt2 + forecast.Periode2.Produkt2 + forecast.Periode3.Produkt2 +
                              forecast.Periode4.Produkt2) / 4 * 0.5);
    }
  }
}