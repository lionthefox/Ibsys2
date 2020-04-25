using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class SimulationService
    {
        private readonly FileRepository fileRepository;

        private results LastPeriodResults { get; set; }
        private IList<Artikel> ArtikelStammdaten { get; set; }
        private IList<PersonalMaschinen> PersonalMaschinenStammdaten { get; set; }
        public IList<Arbeitsplatz> Arbeitsplätze { get; set; }
        public IList<Kaufteil> Kaufteile { get; set; }

        public SimulationService(FileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public void Initialize()
        {
            LastPeriodResults = fileRepository.ParseResultsXml();
            ArtikelStammdaten = fileRepository.ParseArtikelCsv();
            PersonalMaschinenStammdaten = fileRepository.ParsePersonalMaschinenCsv();
            Arbeitsplätze = fileRepository.ParseArbeitsplätzeCsv();
            Kaufteile = fileRepository.ParseKaufteileCsv();
        }
    }
}