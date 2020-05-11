using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;

namespace Ibsys2.Services
{
  public class DispoEfService
  {
    public DispoEFP1 DispoEfP1 { get; set; }
    public DispoEFP2 DispoEfP2 { get; set; }
    public DispoEFP3 DispoEfP3 { get; set; }

    public DispoEigenfertigungen GetEfDispo(Vertriebswunsch vertriebWunsch, Forecast forecast, results lastPeriodResults)
    {
      DispoEfP1 = new DispoEFP1(vertriebWunsch, forecast, lastPeriodResults);
      DispoEfP2 = new DispoEFP2(vertriebWunsch, forecast, lastPeriodResults);
      DispoEfP3 = new DispoEFP3(vertriebWunsch, forecast, lastPeriodResults);

      return new DispoEigenfertigungen
      {
          P1 = DispoEfP1.ListDispoEfPos,
          P2 = DispoEfP2.ListDispoEfPos,
          P3 = DispoEfP3.ListDispoEfPos
      };
    }

    public static int GetLagerbestand(int articleId, results lastPeriodResults)
    {
      return lastPeriodResults.warehousestock.article.FirstOrDefault(x => x.id == articleId)?.amount ?? 0;
    }

    public static int CalcProduktion(DispoEFPos dispoEfPos)
    {
      var prodMenge = dispoEfPos.Vertrieb + dispoEfPos.AuftragUebernahme + dispoEfPos.Sicherheitsbestand -
                      dispoEfPos.Lagerbestand - dispoEfPos.AuftraegeWarteschlange - dispoEfPos.AuftraegeBearbeitung;
      return prodMenge > 0 ? prodMenge : 0;
    }
  }
}