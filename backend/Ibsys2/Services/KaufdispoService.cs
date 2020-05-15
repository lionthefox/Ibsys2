using System;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.Kaufdispo;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class KaufdispoService
    {
        private readonly ErgebnisseVorperiodeService _service;

        public IList<KaufdispoPos> GetKaufDispo(IList<Lieferdaten> lieferdaten, Forecast forecast, Vertriebswunsch vertriebswunsch, results lastPeriodResults)
        {
            var kaufDispo = new List<KaufdispoPos>();
            foreach (var bestellinfo in lieferdaten)
            {
                var kaufdispoPos = GetBestellmengen(bestellinfo, forecast, vertriebswunsch, lastPeriodResults);
                kaufDispo.Add(kaufdispoPos);
            }
            return kaufDispo;
        }

        private KaufdispoPos GetBestellmengen(Lieferdaten lieferdaten, Forecast forecast,
            Vertriebswunsch vertriebswunsch, results lastPeriodResults)
        {
            var kaufdispoPos = new KaufdispoPos();
            kaufdispoPos.MatNr = lieferdaten.Kaufteil;
            kaufdispoPos.BedarfPeriode1 =
                GetBedarf(1, lieferdaten, forecast, vertriebswunsch);
            kaufdispoPos.BedarfPeriode2 =
                GetBedarf(2, lieferdaten, forecast, vertriebswunsch);
            kaufdispoPos.BedarfPeriode3 =
                GetBedarf(3, lieferdaten, forecast, vertriebswunsch);
            kaufdispoPos.BedarfPeriode4 =
                GetBedarf(4, lieferdaten, forecast, vertriebswunsch);
            var mengeBestellart = GetMengeBestellart(kaufdispoPos.BedarfPeriode1, kaufdispoPos.BedarfPeriode2, kaufdispoPos.BedarfPeriode3, kaufdispoPos.BedarfPeriode4, lieferdaten, lastPeriodResults);
            kaufdispoPos.Menge = mengeBestellart[0];
            kaufdispoPos.Bestellart = mengeBestellart[1];
            return kaufdispoPos;
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

        public IList<int> GetMengeBestellart(int bedarfP1, int bedarfP2, int bedarfP3, int bedarfP4, Lieferdaten lieferdaten, results lastPeriodResults)
        {
            int menge = 0;
            int bestellart = 5;
            var lagerbestand = lastPeriodResults.warehousestock.article.FirstOrDefault(x => x.id == lieferdaten.Kaufteil);
            var startmengeLagerbestand = Convert.ToDouble(lagerbestand?.startamount);
            if (lagerbestand != null)
            {
                if (lieferdaten.MaxLieferzeit < 1 &&
                    ((lagerbestand.amount - bedarfP1) <= (startmengeLagerbestand * 0.1)))
                {
                    menge = lieferdaten.Diskontmenge;
                    if ((lagerbestand.amount - bedarfP1) <= 0)
                        bestellart = 4;
                } 
                else if (lieferdaten.MaxLieferzeit >= 1 && lieferdaten.MaxLieferzeit < 2 &&
                         ((lagerbestand.amount - (bedarfP1 + bedarfP2) <= (startmengeLagerbestand * 0.1))))
                {
                    menge = lieferdaten.Diskontmenge;
                    if((lagerbestand.amount - (bedarfP1 + bedarfP2) <= 0))
                        bestellart = 4;
                }
                else if (lieferdaten.MaxLieferzeit >= 2 && lieferdaten.MaxLieferzeit < 3 &&
                         ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3) <= (startmengeLagerbestand * 0.1))))
                {
                    menge = lieferdaten.Diskontmenge;
                    if ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3) <= 0))
                        bestellart = 4;
                }
                    
                else if (lieferdaten.MaxLieferzeit > 3 &&
                         ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3 + bedarfP4) <=
                           (startmengeLagerbestand * 0.1))))
                {
                    menge = lieferdaten.Diskontmenge;
                    if ((lagerbestand.amount - (bedarfP1 + bedarfP2 + bedarfP3 + bedarfP4) <= 0))
                        bestellart = 4;
                }
                    
            }
            return new List<int>(){menge, bestellart};
        }
    }
}