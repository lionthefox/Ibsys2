using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
    public class BenoetigteTeile
    {
        public List<Teil> Teilliste { get; set; }

        public BenoetigteTeile(WartelisteArbeitsplaetze wartelisteArbeitsplaetze, IList<StuecklistenAufloesung> stuecklistenAufloesung, IList<Artikel> artikelStammdaten)
        {
            foreach (var arbeitsplatz in wartelisteArbeitsplaetze.ArbeitsplatzWarteListe)
            {
                foreach (var auftrag in arbeitsplatz.ArbeitsplatzWartelisteAuftraege)
                {
                    var benoetigteTeile = stuecklistenAufloesung.FirstOrDefault(x =>
                        x.Arbeitsplatz == arbeitsplatz.Arbeitsplatz && x.MatNr == auftrag.Teil);
                    foreach (var teil in benoetigteTeile.BenoetigteTeile)
                    {
                        TeilTyp typ =
                            artikelStammdaten.FirstOrDefault(artikel => artikel.Artikelnummer == teil.MatNr)?.Typ == 'K'
                                ? TeilTyp.Kauf
                                : TeilTyp.Eigenferigung;
                        
                        Teilliste.Add( new Teil
                        {
                            Anzahl = teil.Anzahl * auftrag.Menge,
                            Arbeitsplatz = arbeitsplatz.Arbeitsplatz,
                            MatNr = teil.MatNr,
                            Typ = typ
                        });
                    }
                }
            }
        }
    }
}