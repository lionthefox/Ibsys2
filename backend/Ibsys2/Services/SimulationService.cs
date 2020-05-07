using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class SimulationService
    {
        private readonly FileRepository fileRepository;
        private readonly StücklistenService stücklistenService;
        private readonly DispoEfService dispoEfService;

        public results LastPeriodResults { get; set; }
        public IList<Artikel> ArtikelStammdaten { get; set; }
        public IList<PersonalMaschinen> PersonalMaschinenStammdaten { get; set; }
        public IList<Arbeitsplatz> Arbeitsplätze { get; set; }
        public IList<StücklistenPosition> Stückliste { get; set; }
        public IList<StücklistenAuflösung> StücklistenAuflösungen { get; set; }
        public Vertriebswunsch Vertriebswunsch { get; set; }
        public Forecast Forecast { get; set; }

        public SimulationService(
            FileRepository fileRepository, 
            StücklistenService stücklistenService,
            DispoEfService dispoEfService)
        {
            this.fileRepository = fileRepository;
            this.stücklistenService = stücklistenService;
            this.dispoEfService = dispoEfService;
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
            dispoEfService.Initialize(Vertriebswunsch, Forecast, LastPeriodResults);
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