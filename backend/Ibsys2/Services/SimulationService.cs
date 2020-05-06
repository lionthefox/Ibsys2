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
        public IList<StücklistenAuflösung> StücklistenAuflösungen { get; set; } = new List<StücklistenAuflösung>();

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

        public void Start(results results)
        {
            LastPeriodResults = results;
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