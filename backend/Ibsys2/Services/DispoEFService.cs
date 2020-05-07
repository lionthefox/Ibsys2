using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;

namespace Ibsys2.Services
{
  public class DispoEfService
  {
    public DispoEFP1 DispoEfp1 { get; set; }
    public DispoEFP2 DispoEfp2 { get; set; }
    public DispoEFP3 DispoEfP3 { get; set; }

    public void GetEfDispo(Vertriebswunsch vertriebWunsch, Forecast forecast, results lastPeriodResults)
    {
      DispoEfp1 = new DispoEFP1(vertriebWunsch, forecast, lastPeriodResults);
      DispoEfp2 = new DispoEFP2(vertriebWunsch, forecast, lastPeriodResults);
      DispoEfP3 = new DispoEFP3(vertriebWunsch, forecast, lastPeriodResults);
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