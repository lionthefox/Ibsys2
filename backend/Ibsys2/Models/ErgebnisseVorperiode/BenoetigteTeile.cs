using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;
using Ibsys2.Services;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class BenoetigteTeile
    {
        public IList<Teil> Teilliste { get; set; } = new List<Teil>();

        public BenoetigteTeile(WartelisteArbeitsplaetze wartelisteArbeitsplaetze, IList<ArbeitsplatzNachfolger> arbeitsplatzNachfolger, IList<Artikel> artikelStammdaten,IList<StuecklistenPosition> stueckliste)
        {
            foreach (var arbeitsplatz in wartelisteArbeitsplaetze.ArbeitsplatzWarteListe)
            {
                foreach (var auftrag in arbeitsplatz.ArbeitsplatzWartelisteAuftraege)
                {
                    var benoetigteTeile = arbeitsplatzNachfolger.FirstOrDefault(x =>
                        x.Platz == arbeitsplatz.Arbeitsplatz && x.Matnr == auftrag.Teil);
                    if (benoetigteTeile == null)
                        continue;
                    foreach (var teil in benoetigteTeile.BenoetigteTeile)
                    {
                        
                        Teilliste.Add(new Teil() {
                            Anzahl = teil.Value * auftrag.Menge,
                            Arbeitsplatz = arbeitsplatz.Arbeitsplatz,
                            MatNr = teil.Key,
                            Typ = artikelStammdaten.FirstOrDefault(artikel => artikel.Artikelnummer == teil.Key)
                                      ?.Typ ==
                                  'K'
                                ? TeilTyp.Kauf
                                : TeilTyp.Eigenferigung
                        });
                    }
                }
            }
        }
    }
}