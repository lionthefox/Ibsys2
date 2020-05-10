using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Services
{
  public class StuecklistenService
  {
    private IList<StuecklistenPosition> Stueckliste { get; set; }

    public List<StuecklistenAufloesung> Stuecklistenaufloesung(IList<StuecklistenPosition> stueckliste, IList<Artikel> artikelStammdaten)
    {
      Stueckliste = stueckliste;
      var aufloesung = new List<StuecklistenAufloesung>();
      foreach (var position in stueckliste)
      {
        // Falls für die Materialnummer noch keine Auflösung existiert, füge eine neue hinzu
        var existingItem = aufloesung.FirstOrDefault(x => x.MatNr == position.Matnr);
        if (existingItem == null)
        {
          var stücklistenAufloesung = new StuecklistenAufloesung
          {
            MatNr = position.Matnr,
            // Hole den Typ des Materials aus den Stammdaten und setze den entsprechenden Enum Wert
            Typ = artikelStammdaten.FirstOrDefault(artikel => artikel.Artikelnummer == position.Matnr)
                    ?.Typ ==
                  'K'
              ? TeilTyp.Kauf
              : TeilTyp.Eigenferigung,
            Arbeitsplatz = position.Arbeitsplatz,
            BenoetigteTeile = new List<Teil>()
          };
          GetBenötigteTeile(stücklistenAufloesung.BenoetigteTeile, position);
          aufloesung.Add(stücklistenAufloesung);
        }

        // Falls für die Materialnummer bereits eine Auflösung existiert, verwende diese
        else
        {
          GetBenötigteTeile(existingItem.BenoetigteTeile, position);
        }
      }

      return aufloesung;
    }

    private void GetBenötigteTeile(IList<Teil> teile, StuecklistenPosition stuecklistenPosition)
    {
      // Füge das benötigte Teil mit der entsprechenden Anzahl hinzu
      // Falls für das Teil bereits ein Eintrag existiert, nehme diesen und addiere die Anzahl hinzu

      var existingTeil = teile.FirstOrDefault(x => x.MatNr == stuecklistenPosition.Teil);
      if (existingTeil == null)
        teile.Add(new Teil
        {
          MatNr = stuecklistenPosition.Teil,
          Anzahl = stuecklistenPosition.Anzahl,
          Arbeitsplatz = stuecklistenPosition.Arbeitsplatz
        });

      else
        existingTeil.Anzahl += stuecklistenPosition.Anzahl;


      // Iteriere erneut über die gesamte stueckliste:
      // Wenn die Materialnummer mit der Nummer des benötigten Teils übereinstimmt ist dies ein Eigenfertigunsteil
      // => Wiederhole die gesamte Funktion für das benötigte Eigenfertigunsteil
      foreach (var position in Stueckliste)
      {
        if (position.Matnr == stuecklistenPosition.Teil)
          GetBenötigteTeile(teile, position);
      }
    }
  }
}