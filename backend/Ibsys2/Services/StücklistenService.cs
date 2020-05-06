using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class StücklistenService
    {
        private IList<StücklistenPosition> Stückliste { get; set; }

        public List<StücklistenAuflösung> Stücklistenauflösung(IList<StücklistenPosition> stückliste, IList<Artikel> artikelStammdaten)
        {
            Stückliste = stückliste;
            var auflösung = new List<StücklistenAuflösung>();
            foreach (var position in stückliste)
            {
                // Falls für die Materialnummer noch keine Auflösung existiert, füge eine neue hinzu
                var existingAuflösung = auflösung.FirstOrDefault(x => x.MatNr == position.Matnr);
                if (existingAuflösung == null)
                {
                    var stücklistenAuflösung = new StücklistenAuflösung
                    {
                        MatNr = position.Matnr,
                        // Hole den Typ des Materials aus den Stammdaten und setze den entsprechenden Enum Wert
                        Typ = artikelStammdaten.FirstOrDefault(artikel => artikel.Artikelnummer == position.Matnr)
                                  ?.Typ ==
                              'K'
                            ? TeilTyp.Kauf
                            : TeilTyp.Eigenferigung,
                        BenötigteTeile = new Dictionary<int, int>()
                    };
                    GetBenötigteTeile(stücklistenAuflösung.BenötigteTeile, position);
                    auflösung.Add(stücklistenAuflösung);
                }

                // Falls für die Materialnummer bereits eine Auflösung existiert, verwende diese
                else
                {
                    GetBenötigteTeile(existingAuflösung.BenötigteTeile, position);
                }
            }

            return auflösung;
        }

        private void GetBenötigteTeile(
            Dictionary<int, int> teile,
            StücklistenPosition stücklistenPosition
        )
        {
            // Füge das benötigte Teil mit der entsprechenden Anzahl hinzu
            // Falls für das Teil bereits ein Eintrag existiert, nehme diesen und addiere die Anzahl hinzu
            if (!teile.TryAdd(stücklistenPosition.Teil, stücklistenPosition.Anzahl))
                teile[stücklistenPosition.Teil] += stücklistenPosition.Anzahl;


            // Iteriere erneut über die gesamte stückliste:
            // Wenn die Materialnummer mit der Nummer des benötigten Teils übereinstimmt ist dies ein Eigenfertigunsteil
            // => Wiederhole die gesamte Funktion für das benötigte Eigenfertigunsteil
            foreach (var position in Stückliste)
            {
                if (position.Matnr == stücklistenPosition.Teil)
                    GetBenötigteTeile(teile, position);
            }
        }
    }
}