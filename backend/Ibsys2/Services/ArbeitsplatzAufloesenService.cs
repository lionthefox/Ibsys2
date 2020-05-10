using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Services
{
    public class ArbeitsplatzAufloesenService
    {
        private IList<Arbeitsplatz> Arbeitsplaetze { get; set; }

    public List<ArbeitsplatzNachfolger> ArbeitsplatzAuflösen(IList<Arbeitsplatz> arbeitsplaetze)
    {
      Arbeitsplaetze = arbeitsplaetze;
      var aufloesung = new List<ArbeitsplatzNachfolger>();
      foreach (var position in arbeitsplaetze)
      {
        // Falls für die Arbeitsplatz, Materialnummerkombination noch kein Eintrag vorhanden ist.
        var existingItem = aufloesung.FirstOrDefault(x => x.Platz == position.Platz && x.Matnr == position.Matnr);
        if (existingItem == null)
        {
          var stücklistenAufloesung = new ArbeitsplatzNachfolger
          {
            Platz = position.Platz,
            Matnr = position.Matnr,
            Nachfolger = new List<int>()
          };
          GetNachfolger(stücklistenAufloesung.Nachfolger, position);
          aufloesung.Add(stücklistenAufloesung);
        }

        // Falls für dise Arbeitsplatz-Materialnummerkombination schon ein Eintrag exisitiert.
        else
        {
          GetNachfolger(existingItem.Nachfolger, position);
        }
      }

      return aufloesung;
    }

    private void GetNachfolger(IList<int> nachfolger, Arbeitsplatz arbeitsplatz)
    {
      // füge den Nachfolgearbeitsplatz den Nachfolgern hinzu
      var existingArbeitsplatz = nachfolger.FirstOrDefault(x => x == arbeitsplatz.Nachfolger);
      nachfolger.Add(existingArbeitsplatz);
      

      //Iteriere erneut über die Arbeitsplätze
      foreach (var position in Arbeitsplaetze)
      {
        if (position.Platz == arbeitsplatz.Nachfolger)
          GetNachfolger(nachfolger, position);
      }
    }
    }
}