using System.Collections.Generic;
using System.Linq;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class SimulationService
    {
        private readonly FileRepository fileRepository;
        private readonly StücklistenService stücklistenService;

        public results LastPeriodResults { get; set; }
        public IList<Artikel> ArtikelStammdaten { get; set; }
        public IList<PersonalMaschinen> PersonalMaschinenStammdaten { get; set; }
        public IList<Arbeitsplatz> Arbeitsplätze { get; set; }
        public IList<StücklistenPosition> Stückliste { get; set; }
        public IList<StücklistenAuflösung> StücklistenAuflösungen { get; set; }
        public Vertriebswunsch Vertriebswunsch { get; set; }
        public Forecast Forecast { get; set; }

        public SimulationService(FileRepository fileRepository, StücklistenService stücklistenService)
        {
            this.fileRepository = fileRepository;
            this.stücklistenService = stücklistenService;
            Initialize();
        }

        private void Initialize()
        {
            ParseStammdaten();
            StücklistenAuflösungen = stücklistenService.Stücklistenauflösung(Stückliste, ArtikelStammdaten);
        }

        public void SetResults(results results)
        {
            LastPeriodResults = results;
        }

        public void Start(SimulationInput input)
        {
            Vertriebswunsch = input.Vertriebswunsch;
            Forecast = input.Forecast;
            
        }

        private void ParseStammdaten()
        {
            ArtikelStammdaten = fileRepository.ParseArtikelCsv();
            PersonalMaschinenStammdaten = fileRepository.ParsePersonalMaschinenCsv();
            Arbeitsplätze = fileRepository.ParseArbeitsplätzeCsv();
            Stückliste = fileRepository.ParseStücklistenAuflösungCsv();
        }
    }
}