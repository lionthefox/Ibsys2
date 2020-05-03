using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Services
{
    public class SimulationService
    {
        private readonly FileRepository _fileRepository;

        public results LastPeriodResults { get; set; }
        public IList<Artikel> ArtikelStammdaten { get; set; }
        public IList<PersonalMaschinen> PersonalMaschinenStammdaten { get; set; }
        public IList<Arbeitsplatz> Arbeitsplätze { get; set; }
        public IList<StücklistenPosition> Stückliste { get; set; }
        public IList<StücklistenAuflösung> StücklistenAuflösungen { get; set; } = new List<StücklistenAuflösung>();

        public SimulationService(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            Initialize();
        }

        private void Initialize()
        {
            ParseStammdaten();
            Stücklistenauflösung();
        }

        public void Start(results results)
        {
            LastPeriodResults = results;
        }

        private void Stücklistenauflösung()
        {
            foreach (var position in Stückliste)
            {
                // Falls für die Materialnummer noch keine Auflösung existiert, füge eine neue hinzu
                var existingAuflösung = StücklistenAuflösungen.FirstOrDefault(x => x.MatNr == position.Matnr);
                if (existingAuflösung == null)
                {
                    var stücklistenAuflösung = new StücklistenAuflösung
                    {
                        MatNr = position.Matnr,
                        // Hole den Typ des Materials aus den Stammdaten und setze den entsprechenden Enum Wert
                        Typ = ArtikelStammdaten.FirstOrDefault(artikel => artikel.Artikelnummer == position.Matnr)
                                  ?.Typ ==
                              'K'
                            ? TeilTyp.Kauf
                            : TeilTyp.Eigenferigung,
                        BenötigteTeile = new Dictionary<int, int>()
                    };
                    GetBenötigteTeile(stücklistenAuflösung.BenötigteTeile, position);
                    StücklistenAuflösungen.Add(stücklistenAuflösung);
                }

                // Falls für die Materialnummer bereits eine Auflösung existiert, verwende diese
                else
                {
                    GetBenötigteTeile(existingAuflösung.BenötigteTeile, position);
                }
            }
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


            // Iteriere erneut über die gesamte Stückliste:
            // Wenn die Materialnummer mit der Nummer des benötigten Teils übereinstimmt ist dies ein Eigenfertigunsteil
            // => Wiederhole die gesamte Funktion für das benötigte Eigenfertigunsteil
            foreach (var position in Stückliste)
            {
                if (position.Matnr == stücklistenPosition.Teil)
                    GetBenötigteTeile(teile, position);
            }
        }

        private void ParseStammdaten()
        {
            ArtikelStammdaten = _fileRepository.ParseArtikelCsv();
            PersonalMaschinenStammdaten = _fileRepository.ParsePersonalMaschinenCsv();
            Arbeitsplätze = _fileRepository.ParseArbeitsplätzeCsv();
            Stückliste = _fileRepository.ParseStücklistenAuflösungCsv();
        }
    }
}