using System.Collections.Generic;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Models.ErgebnisseVorperiode
{
  public class WartelisteArbeitsplatz
  {
    public IList<AuftraegeWarteschlange> ArbeitsplatzWartelisteAuftraege = new List<AuftraegeWarteschlange>();
    public int Arbeitszeit { get; set; }
    public int Arbeitsplatz { get; set; }
    public int Ruestzeit { get; set; }

    public WartelisteArbeitsplatz(int arbeitsplatzId, results lastPeriodResults, IList<Artikel> artikelStammdaten)
    {
      foreach (var arbeitsplatz in lastPeriodResults.waitinglistworkstations)
      {
        if (arbeitsplatz.id != arbeitsplatzId)
          continue;

        Arbeitszeit = arbeitsplatz.timeneed;
        Arbeitsplatz = arbeitsplatz.id;
            if (arbeitsplatz.waitinglist == null)
                continue;
        foreach (var teil in arbeitsplatz.waitinglist)
        {
          var warteListeTeil = new AuftraegeWarteschlange
          {
            Arbeitsplatz = arbeitsplatzId,
            Fertigungsauftrag = teil.order,
            ErstesLos = teil.firstbatch,
            LetztesLos = teil.lastbatch,
            Periode = teil.period,
            Teil = teil.item,
            Zeitbedarf = teil.timeneed,
            Ruestzeit = GetRuestzeit(arbeitsplatzId, teil.item, artikelStammdaten)
          };
          ArbeitsplatzWartelisteAuftraege.Add(warteListeTeil);
        }
      }
    }

    public WartelisteArbeitsplatz(int arbeitsplatz, AuftraegeWarteschlange auftragVorgaenger, IList<Artikel> artikelStammdaten)
    {
      Arbeitsplatz = arbeitsplatz;
      Arbeitszeit = 0;
      Ruestzeit = 0;
      var auftrag = new AuftraegeWarteschlange
      {
        Arbeitsplatz = arbeitsplatz,
        Fertigungsauftrag = auftragVorgaenger.Fertigungsauftrag,
        ErstesLos = auftragVorgaenger.ErstesLos,
        LetztesLos = auftragVorgaenger.LetztesLos,
        Periode = auftragVorgaenger.Periode,
        Teil = auftragVorgaenger.Teil,
        Zeitbedarf = getZeitbedarf(arbeitsplatz, auftragVorgaenger.Teil, auftragVorgaenger.Menge, artikelStammdaten),
        Ruestzeit = GetRuestzeit(arbeitsplatz, auftragVorgaenger.Teil, artikelStammdaten)
      };
      ArbeitsplatzWartelisteAuftraege.Add(auftrag);
    }
    

    public static int GetRuestzeit(int arbeitsplatzId, int matNr, IList<Artikel> artikelStammdaten)
    {
      foreach (var artikel in artikelStammdaten)
      {
        if (artikel.Artikelnummer == matNr)
        {
          switch (arbeitsplatzId)
          {
            case 1:
              return artikel.RüstzeitPlatz1 ?? 0;
            case 2:
              return artikel.RüstzeitPlatz2 ?? 0;
            case 3:
              return artikel.RüstzeitPlatz3 ?? 0;
            case 4:
              return artikel.RüstzeitPlatz4 ?? 0;
            case 6:
              return artikel.RüstzeitPlatz6 ?? 0;
            case 7:
              return artikel.RüstzeitPlatz7 ?? 0;
            case 8:
              return artikel.RüstzeitPlatz8 ?? 0;
            case 9:
              return artikel.RüstzeitPlatz9 ?? 0;
            case 10:
              return artikel.RüstzeitPlatz10 ?? 0;
            case 11:
              return artikel.RüstzeitPlatz11 ?? 0;
            case 12:
              return artikel.RüstzeitPlatz12 ?? 0;
            case 13:
              return artikel.RüstzeitPlatz13 ?? 0;
            case 14:
              return artikel.RüstzeitPlatz14 ?? 0;
            case 15:
              return artikel.RüstzeitPlatz15 ?? 0;
          }
        }
      }

      return 0;
    }

    public static int getZeitbedarf(int arbeitsplatzId, int matNr, int menge, IList<Artikel> artikelStammdaten)
    {
      foreach (var artikel in artikelStammdaten)
      {
        if (artikel.Artikelnummer == matNr)
        {
          switch (arbeitsplatzId)
          {
            case 1:
              return artikel.BearbeitungszeitPlatz1 * menge ?? 0;
            case 2:
              return artikel.BearbeitungszeitPlatz2 * menge ?? 0;
            case 3:
              return artikel.BearbeitungszeitPlatz3 * menge ?? 0;
            case 4:
              return artikel.BearbeitungszeitPlatz4 * menge ?? 0;
            case 6:
              return artikel.BearbeitungszeitPlatz6 * menge ?? 0;
            case 7:
              return artikel.BearbeitungszeitPlatz7 * menge ?? 0;
            case 8:
              return artikel.BearbeitungszeitPlatz8 * menge ?? 0;
            case 9:
              return artikel.BearbeitungszeitPlatz9 * menge ?? 0;
            case 10:
              return artikel.BearbeitungszeitPlatz10 * menge ?? 0;
            case 11:
              return artikel.BearbeitungszeitPlatz11 * menge ?? 0;
            case 12:
              return artikel.BearbeitungszeitPlatz12 * menge ?? 0;
            case 13:
              return artikel.BearbeitungszeitPlatz13 * menge ?? 0;
            case 14:
              return artikel.BearbeitungszeitPlatz14 * menge ?? 0;
            case 15:
              return artikel.BearbeitungszeitPlatz15 * menge ?? 0;
          }
        }
      }
      return 0;
    }
  }
}