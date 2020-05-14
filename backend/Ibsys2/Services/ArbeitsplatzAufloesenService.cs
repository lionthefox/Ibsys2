using System;
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

      public List<ArbeitsplatzNachfolger> ArbeitsplatzAuflösen(IList<Arbeitsplatz> arbeitsplaetze, IList<StuecklistenPosition> stueckliste)
      {
        Arbeitsplaetze = arbeitsplaetze;
        var aufloesung = new List<ArbeitsplatzNachfolger>();
        foreach (var Arbeitsplatz in arbeitsplaetze)
        {
          // Falls für die Arbeitsplatz, Materialnummerkombination noch kein Eintrag vorhanden ist.
          var existingItem = aufloesung.FirstOrDefault(x => x.Platz == Arbeitsplatz.Platz && x.Matnr == Arbeitsplatz.Matnr);
          if (existingItem == null)
          {
            var stücklistenAufloesung = new ArbeitsplatzNachfolger
            {
              Platz = Arbeitsplatz.Platz,
              Matnr = Arbeitsplatz.Matnr,
              Nachfolger = new List<int>(),
              BenoetigteTeile = new Dictionary<int, int>()
            };
            GetNachfolger(stücklistenAufloesung.Nachfolger, Arbeitsplatz);
            GetBenoetigteTeile(stücklistenAufloesung.BenoetigteTeile, Arbeitsplatz, stücklistenAufloesung.Nachfolger, stueckliste);
            aufloesung.Add(stücklistenAufloesung);
          }

          // Falls für dise Arbeitsplatz-Materialnummerkombination schon ein Eintrag exisitiert.
          else
          {
            GetNachfolger(existingItem.Nachfolger, Arbeitsplatz);
          }
        }
        return aufloesung;
      }

      private void GetBenoetigteTeile(Dictionary<int, int> benoetigteTeile,Arbeitsplatz arbeitsplatz, IList<int> nachfolger,IList<StuecklistenPosition> stueckliste)
      {
        // Für den aktuellen Arbeitsplatz
        foreach (var material in stueckliste)
          if (material.Arbeitsplatz == arbeitsplatz.Platz && material.Matnr == arbeitsplatz.Matnr)
            benoetigteTeile.Add(material.Teil, material.Anzahl);
          
        
        
        // Für alle folgenden Arbeitsplätze
        foreach (var nachfolgerEinzel in nachfolger)
        foreach (var position in stueckliste)
            if (position.Arbeitsplatz == nachfolgerEinzel && position.Matnr == arbeitsplatz.Matnr)
              benoetigteTeile.Add(position.Teil, position.Anzahl);
      }

      private void GetNachfolger(IList<int> nachfolger, Arbeitsplatz arbeitsplatz)
      {
        // füge den Nachfolgearbeitsplatz den Nachfolgern hinzu
        var existingArbeitsplatz = nachfolger.FirstOrDefault(x => x == arbeitsplatz.Nachfolger);
        if (existingArbeitsplatz == 0 && arbeitsplatz.Nachfolger.HasValue)
          nachfolger.Add(arbeitsplatz.Nachfolger.Value);
        else
          return;
        
        //Iteriere erneut über die Arbeitsplätze
        foreach (var position in Arbeitsplaetze)
        {
          if (position.Platz == arbeitsplatz.Nachfolger)
            GetNachfolger(nachfolger, position);
          else if (position.Nachfolger == null) 
            return;
        }
      }
    }
}