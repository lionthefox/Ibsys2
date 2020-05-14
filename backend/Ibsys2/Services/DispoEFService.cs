using System;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.ErgebnisseVorperiode;

namespace Ibsys2.Services
{
    public class DispoEfService
    {
        public DispoEFP1 DispoEfP1 { get; set; }
        public DispoEFP2 DispoEfP2 { get; set; }
        public DispoEFP3 DispoEfP3 { get; set; }

        public DispoEigenfertigungen GetEfDispo(Vertriebswunsch vertriebWunsch, Forecast forecast,
            results lastPeriodResults, ErgebnisseVorperiodeService ergebnisseVorperiodeService)
        {
            DispoEfP1 = new DispoEFP1(vertriebWunsch, forecast, lastPeriodResults);
            DispoEfP2 = new DispoEFP2(vertriebWunsch, forecast, lastPeriodResults);
            DispoEfP3 = new DispoEFP3(vertriebWunsch, forecast, lastPeriodResults);

            // Aufträge in Bearbeitung in die Dispo-Eigenfertigung übertragen
            foreach (var auftrag in ergebnisseVorperiodeService.InBearbeitung.AuftraegeInBearbeitung)
            {
                switch (auftrag.Teil)
                {
                    case 26:
                    case 16:
                    case 17:
                        decimal menge = auftrag.Menge / 3;
                        int roundedMenge = Convert.ToInt32(Math.Round(menge, 0, MidpointRounding.AwayFromZero));
                        var dispoPosP1 = DispoEfP1
                            .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                        dispoPosP1.AuftraegeBearbeitung = roundedMenge;
                        var dispoPosP2 = DispoEfP2
                            .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                        dispoPosP2.AuftraegeBearbeitung = roundedMenge;
                        var dispoPosP3 = DispoEfP3
                            .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                        dispoPosP3.AuftraegeBearbeitung = roundedMenge;
                        break;
                    default:
                        var dispoPos = DispoEfP1
                            .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                        if (dispoPos == null)
                            dispoPos = DispoEfP2
                                .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                        if (dispoPos == null)
                            dispoPos = DispoEfP3
                                .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                        dispoPos.AuftraegeBearbeitung = auftrag.Menge;
                        break;
                }
            }

            // Aufträge in der Warteschlange in die Dispo-Eigenfertigung übertragen
            foreach (var arbeitsplatz in ergebnisseVorperiodeService.WartelisteArbeitsplaetze.ArbeitsplatzWarteListe)
            {
                foreach (var auftrag in arbeitsplatz.ArbeitsplatzWartelisteAuftraege)
                {
                    if (auftrag.BasisAuftrag)
                    {
                        switch (auftrag.Teil)
                        {
                            case 26:
                            case 16:
                            case 17:
                                decimal menge = auftrag.Menge / 3;
                                int roundedMenge = Convert.ToInt32(Math.Round(menge, 0, MidpointRounding.AwayFromZero));
                                var dispoPosP1 = DispoEfP1
                                    .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                                dispoPosP1.AuftraegeWarteschlange = roundedMenge;
                                var dispoPosP2 = DispoEfP2
                                    .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                                dispoPosP2.AuftraegeWarteschlange = roundedMenge;
                                var dispoPosP3 = DispoEfP3
                                    .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                                dispoPosP3.AuftraegeWarteschlange = roundedMenge;
                                break;
                            default:
                                var dispoPos = DispoEfP1
                                    .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                                if (dispoPos == null)
                                    dispoPos = DispoEfP2
                                        .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                                if (dispoPos == null)
                                    dispoPos = DispoEfP3
                                        .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                                dispoPos.AuftraegeWarteschlange = auftrag.Menge;
                                break;
                        }
                    }
                }
            }

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
                            dispoEfPos.Lagerbestand - dispoEfPos.AuftraegeWarteschlange -
                            dispoEfPos.AuftraegeBearbeitung;
            return prodMenge > 0 ? prodMenge : 0;
        }
    }
}