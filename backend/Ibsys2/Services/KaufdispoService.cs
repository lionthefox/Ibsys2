using System;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.Kaufdispo;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class KaufdispoService
    {
        public IList<KaufdispoPos> GetKaufDispo(IList<Lieferdaten> lieferdaten, Forecast forecast,
            Vertriebswunsch vertriebswunsch, results lastPeriodResults, BenoetigteTeile benoetigteTeile, IList<KaufdispoPos> updatedKaufdispo = null)
        {
            var kaufDispo = new List<KaufdispoPos>();
            foreach (var bestellinfo in lieferdaten)
            {
                KaufdispoPos updatedKaufdispoPos = null;
                if (updatedKaufdispo != null)
                    updatedKaufdispoPos = updatedKaufdispo.FirstOrDefault(x => x.MatNr == bestellinfo.Kaufteil);
                var kaufdispoPos = GetBestellmengen(bestellinfo, forecast, vertriebswunsch, lastPeriodResults,
                    benoetigteTeile, updatedKaufdispoPos);
                kaufDispo.Add(kaufdispoPos);
            }

            return kaufDispo;
        }

        private KaufdispoPos GetBestellmengen(Lieferdaten lieferdaten, Forecast forecast,
            Vertriebswunsch vertriebswunsch, results lastPeriodResults, BenoetigteTeile benoetigteTeile, KaufdispoPos updatedKaufdispoPos = null)
        {
            var kaufdispoPos = new KaufdispoPos();
            kaufdispoPos.MatNr = lieferdaten.Kaufteil;
            kaufdispoPos.Lagermenge = lastPeriodResults.warehousestock.article
                .First(x => x.id == lieferdaten.Kaufteil).amount;
            kaufdispoPos.BedarfPeriode1 =
                GetBedarf(1, lieferdaten, forecast, vertriebswunsch);
            kaufdispoPos.BedarfPeriode2 =
                GetBedarf(2, lieferdaten, forecast, vertriebswunsch);
            kaufdispoPos.BedarfPeriode3 =
                GetBedarf(3, lieferdaten, forecast, vertriebswunsch);
            kaufdispoPos.BedarfPeriode4 =
                GetBedarf(4, lieferdaten, forecast, vertriebswunsch);

            if (updatedKaufdispoPos == null)
            {
                var mengeBestellart = GetMengeBestellart(kaufdispoPos.BedarfPeriode1, kaufdispoPos.BedarfPeriode2,
                    kaufdispoPos.BedarfPeriode3, kaufdispoPos.BedarfPeriode4, lieferdaten, lastPeriodResults,
                    benoetigteTeile);
                kaufdispoPos.Menge = mengeBestellart[0];
                if (kaufdispoPos.Bestellart == null)
                    kaufdispoPos.Bestellart = mengeBestellart[1];
            }
            else
            {
                kaufdispoPos.Menge = updatedKaufdispoPos.Menge;
                kaufdispoPos.Bestellart = updatedKaufdispoPos.Bestellart;
            }
            // Lagervorraussagen
            kaufdispoPos.LagerbestandPeriode1 =
                GetLagerbestandZukunft(1, kaufdispoPos.BedarfPeriode1, kaufdispoPos.Lagermenge, lieferdaten, kaufdispoPos.Menge, lastPeriodResults);
            kaufdispoPos.LagerbestandPeriode2 =
                GetLagerbestandZukunft(2, kaufdispoPos.BedarfPeriode1 + kaufdispoPos.BedarfPeriode2, kaufdispoPos.Lagermenge, lieferdaten, kaufdispoPos.Menge, lastPeriodResults);
            kaufdispoPos.LagerbestandPeriode3 =
                GetLagerbestandZukunft(3, kaufdispoPos.BedarfPeriode1 + kaufdispoPos.BedarfPeriode2 + kaufdispoPos.BedarfPeriode3, kaufdispoPos.Lagermenge, lieferdaten, kaufdispoPos.Menge, lastPeriodResults);
            kaufdispoPos.LagerbestandPeriode4 =
                GetLagerbestandZukunft(4, kaufdispoPos.BedarfPeriode1 + kaufdispoPos.BedarfPeriode2 + kaufdispoPos.BedarfPeriode3 + kaufdispoPos.BedarfPeriode4, kaufdispoPos.Lagermenge, lieferdaten, kaufdispoPos.Menge, lastPeriodResults);
            kaufdispoPos.Liefertermin = GetLiefertermin(lastPeriodResults.period, lieferdaten, 'D', kaufdispoPos.Bestellart);
            kaufdispoPos.LieferterminEng = GetLiefertermin(lastPeriodResults.period, lieferdaten, 'E', kaufdispoPos.Bestellart);
            return kaufdispoPos;
        }

        private int GetLagerbestandZukunft(int periode, int bedarf, int lagermenge, Lieferdaten lieferdaten, int menge, results lastPeriodResults)
        {
            var bestellung = lastPeriodResults.ordersinwork.FirstOrDefault(x => x.item == lieferdaten.Kaufteil);
            if (bestellung != null && bestellung.period == (lastPeriodResults.period + (periode - 1)))
                menge += bestellung.amount;
            if (lieferdaten.Standardlieferzeit < periode)
                return lagermenge - bedarf + menge;
            return lagermenge - bedarf;
        }

        private int GetBedarf(int periode, Lieferdaten lieferdaten, Forecast forecast, Vertriebswunsch vertriebswunsch)
        {
            int bedarf = 0;
            switch (periode)
            {
                case 1:
                    bedarf = (vertriebswunsch.Produkt1 + vertriebswunsch.Direktverkauf.Produkt1.Menge) * lieferdaten.VerwendungP1 ?? 0 +
                             (vertriebswunsch.Produkt2 + vertriebswunsch.Direktverkauf.Produkt2.Menge) * lieferdaten.VerwendungP2 ?? 0 +
                             (vertriebswunsch.Produkt3 + vertriebswunsch.Direktverkauf.Produkt3.Menge) * lieferdaten.VerwendungP3 ?? 0;
                    break;
                case 2:
                    bedarf = forecast.Periode2.Produkt1 * lieferdaten.VerwendungP1 ?? 0 +
                             forecast.Periode2.Produkt2 * lieferdaten.VerwendungP2 ?? 0 + 
                             forecast.Periode2.Produkt3 + lieferdaten.VerwendungP3 ?? 0;
                    break;
                case 3:
                    bedarf = forecast.Periode3.Produkt1 * lieferdaten.VerwendungP1 ?? 0+
                             forecast.Periode3.Produkt2 * lieferdaten.VerwendungP2 ?? 0+
                             forecast.Periode3.Produkt3 * lieferdaten.VerwendungP3 ?? 0;
                    break;
                case 4:
                    bedarf = forecast.Periode4.Produkt1 * lieferdaten.VerwendungP1 ?? 0+
                             forecast.Periode4.Produkt2 * lieferdaten.VerwendungP2 ?? 0+
                             forecast.Periode4.Produkt3 * lieferdaten.VerwendungP3 ?? 0;
                    break;
            }
            return bedarf;
        }

        public IList<int> GetMengeBestellart(int bedarfP1, int bedarfP2, int bedarfP3, int bedarfP4, Lieferdaten lieferdaten, results lastPeriodResults, BenoetigteTeile benoetigteTeile)
        {
            int menge = 0;
            int bestellart = 5;
            int actualPeriod = lastPeriodResults.period;
            var lagerbestand = lastPeriodResults.warehousestock.article.FirstOrDefault(x => x.id == lieferdaten.Kaufteil);
            int abzugWarteschlange = 0;
            foreach (var teil in benoetigteTeile.Teilliste)
                if (teil.MatNr == lieferdaten.Kaufteil)
                    abzugWarteschlange += teil.Anzahl;
            var startmengeLagerbestand = Convert.ToDouble(lagerbestand?.startamount);
            if (lagerbestand != null)
            {
                if (lieferdaten.MaxLieferzeit < 1 &&
                    ((lagerbestand.amount - (bedarfP1 + abzugWarteschlange)) <= (startmengeLagerbestand * 0.1)))
                {
                    menge = lieferdaten.Diskontmenge;
                    if ((lagerbestand.amount - bedarfP1 + abzugWarteschlange) <= 0)
                        bestellart = 4;
                } 
                else if (lieferdaten.MaxLieferzeit >= 1 && lieferdaten.MaxLieferzeit < 2 &&
                         ((lagerbestand.amount - (bedarfP1 + bedarfP2 + abzugWarteschlange) <= (startmengeLagerbestand * 0.1))))
                {
                    menge = lieferdaten.Diskontmenge;
                    if((lagerbestand.amount - (bedarfP1 + bedarfP2 + abzugWarteschlange) <= 0))
                        bestellart = 4;
                }
                else if (lieferdaten.MaxLieferzeit >= 2 && lieferdaten.MaxLieferzeit < 3 &&
                         ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3 + abzugWarteschlange) <= (startmengeLagerbestand * 0.1))))
                {
                    menge = lieferdaten.Diskontmenge;
                    if ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3 + abzugWarteschlange) <= 0))
                        bestellart = 4;
                }
                   
                else if (lieferdaten.MaxLieferzeit > 3 &&
                         ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3 + bedarfP4 + abzugWarteschlange) <=
                           (startmengeLagerbestand * 0.1))))
                {
                    menge = lieferdaten.Diskontmenge;
                    if ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3 + bedarfP4 + abzugWarteschlange) <= 0))
                        bestellart = 4;
                }
                    
            }
            return new List<int>(){menge, bestellart};
        }


        private string GetLiefertermin(int period, Lieferdaten lieferdaten, Char lang, int? bestellart)
        {
            decimal lieferdauer;
            string wochentag = "";
            period += 1;


            if (bestellart == 4)
                lieferdauer = lieferdaten.Standardlieferzeit / 2;
            else if (bestellart == 3)
                lieferdauer = lieferdaten.Standardlieferzeit * Convert.ToDecimal(0.3);
            else
                lieferdauer = lieferdaten.Standardlieferzeit;

            double dWochentag = Convert.ToDouble(lieferdauer % 1);
            dWochentag = Math.Round(dWochentag, 1, MidpointRounding.AwayFromZero);
            
            if (lang == 'D')
            {
                switch (dWochentag)
                {
                    case 0:
                    case 0.1:
                        wochentag = "Montag";
                        break;
                    case 0.2:
                    case 0.3:
                        wochentag = "Dienstag";
                        break;
                    case 0.4:
                    case 0.5:
                        wochentag = "Mittwoch";
                        break;
                    case 0.6:
                    case 0.7:
                        wochentag = "Donnerstag";
                        break;
                    case 0.8:
                    case 0.9:
                        wochentag = "Freitag";
                        break;
                    case 1:
                        wochentag = "Montag";
                        period += 1;
                        break;
                    default:
                        wochentag = "";
                        break;
                }
                if (lieferdauer < 1)
                    return $"Periode {period} am {wochentag}";
                else if (lieferdauer >= 1 && lieferdauer < 2)
                    return $"Periode {period + 1} am {wochentag}";
                else if (lieferdauer >= 2 && lieferdauer < 3)
                    return $"Periode {period + 2} am {wochentag}";
                else if (lieferdauer >= 3 && lieferdauer < 4)
                    return $"Periode {period + 3} am {wochentag}";
                else
                    return $"Über 4 Perioden";
            }
            else
            {
                switch (dWochentag)
                {
                    case 0:
                    case 0.1:
                        wochentag = "Monday";
                        break;
                    case 0.2:
                    case 0.3:
                        wochentag = "Tuesday";
                        break;
                    case 0.4:
                    case 0.5:
                        wochentag = "Wednesday";
                        break;
                    case 0.6:
                    case 0.7:
                        wochentag = "Thursday";
                        break;
                    case 0.8:
                    case 0.9:
                        wochentag = "Friday";
                        break;
                    case 1:
                        wochentag = "Monday";
                        period += 1;
                        break;
                    default:
                        wochentag = "";
                        break;
                }
                if (lieferdauer < 1)
                    return $"Period {period} at {wochentag}";
                else if (lieferdauer >= 1 && lieferdauer < 2)
                    return $"Period {period + 1} at {wochentag}";
                else if (lieferdauer >= 2 && lieferdauer < 3)
                    return $"Period {period + 2} at {wochentag}";
                else if (lieferdauer >= 3 && lieferdauer < 4)
                    return $"Period {period + 3} at {wochentag}";
                else
                    return $"More than 4 Periods";
            }
        }
    }
}