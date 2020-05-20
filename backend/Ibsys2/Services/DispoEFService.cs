using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class DispoEfService
    {
        private readonly ErgebnisseVorperiodeService _ergebnisseVorperiodeService;

        public DispoEfService(ErgebnisseVorperiodeService ergebnisseVorperiodeService)
        {
            _ergebnisseVorperiodeService = ergebnisseVorperiodeService;
        }

        public DispoEFP1 DispoEfP1 { get; set; }
        public DispoEFP2 DispoEfP2 { get; set; }
        public DispoEFP3 DispoEfP3 { get; set; }

        public DispoEigenfertigungen GetEfDispo(Vertriebswunsch vertriebWunsch, Forecast forecast, results lastPeriodResults, IList<Artikel> artikelStammdaten)
        {
        DispoEfP1 = new DispoEFP1(vertriebWunsch, forecast, lastPeriodResults, artikelStammdaten);
        DispoEfP2 = new DispoEFP2(vertriebWunsch, forecast, lastPeriodResults, artikelStammdaten);
        DispoEfP3 = new DispoEFP3(vertriebWunsch, forecast, lastPeriodResults, artikelStammdaten);
      
      // Aufträge in Bearbeitung in die Dispo-Eigenfertigung übertragen
        foreach (var auftrag in _ergebnisseVorperiodeService.InBearbeitung.AuftraegeInBearbeitung)
        {
            switch (auftrag.Teil)
            {
                case 26:
                case 16:
                case 17:
                    decimal menge = auftrag.Menge / 3;
                    int roundedMenge = Convert.ToInt32(Math.Round(menge, 0, MidpointRounding.AwayFromZero));
                    var dispoPosP1 = DispoEfP1
                        .ListDispoEfPos.First(x => x.ArticleId == auftrag.Teil);
                    dispoPosP1.AuftraegeBearbeitung = roundedMenge;
                    DispoEFP1.Uebernahmemengen(dispoPosP1, vertriebWunsch, DispoEfP1.ListDispoEfPos);
                    var dispoPosP2 = DispoEfP2
                        .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                    dispoPosP2.AuftraegeBearbeitung = roundedMenge;
                    DispoEFP2.Uebernahmemengen(dispoPosP2, vertriebWunsch, DispoEfP2.ListDispoEfPos);
                    var dispoPosP3 = DispoEfP3
                        .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                    dispoPosP3.AuftraegeBearbeitung = roundedMenge;
                    DispoEFP3.Uebernahmemengen(dispoPosP3, vertriebWunsch, DispoEfP3.ListDispoEfPos);
                    dispoPosP1.Produktion = CalcProduktion(dispoPosP1);
                    dispoPosP2.Produktion = CalcProduktion(dispoPosP2);
                    dispoPosP3.Produktion = CalcProduktion(dispoPosP3);
                    break;
                default:
                    var dispoPos = DispoEfP1
                        .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);
                    if (dispoPos != null)
                    {
                        dispoPos.AuftraegeBearbeitung = auftrag.Menge;
                        DispoEFP1.Uebernahmemengen(dispoPos, vertriebWunsch, DispoEfP1.ListDispoEfPos);
                    } else
                        dispoPos = DispoEfP2
                            .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);

                    if (dispoPos != null)
                    {
                        dispoPos.AuftraegeBearbeitung = auftrag.Menge;
                        DispoEFP2.Uebernahmemengen(dispoPos, vertriebWunsch, DispoEfP2.ListDispoEfPos);
                    } else
                        dispoPos = DispoEfP3
                            .ListDispoEfPos.FirstOrDefault(x => x.ArticleId == auftrag.Teil);

                    if (dispoPos != null)
                    {
                        dispoPos.AuftraegeBearbeitung = auftrag.Menge;
                        DispoEFP3.Uebernahmemengen(dispoPos, vertriebWunsch, DispoEfP3.ListDispoEfPos);
                    }
                    dispoPos.Produktion = CalcProduktion(dispoPos);
                    break;
            }
        }

        // Aufträge in der Warteschlange in die Dispo-Eigenfertigung übertragen
        foreach (var arbeitsplatz in _ergebnisseVorperiodeService.WartelisteArbeitsplaetze.ArbeitsplatzWarteListe)
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
                            dispoPosP1.Produktion = CalcProduktion(dispoPosP1);
                            dispoPosP2.Produktion = CalcProduktion(dispoPosP2);
                            dispoPosP3.Produktion = CalcProduktion(dispoPosP3);
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
                            dispoPos.Produktion = CalcProduktion(dispoPos);
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
            //return ((prodMenge / 10 + 1) * 10) > 0 ? ((prodMenge / 10 + 1) * 10) : 0;
        }
    }
}