using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;

namespace Ibsys2.Services
{
    public class SimulationService
    {
        private readonly FileRepository _fileRepository;

        private results LastPeriodResults { get; set; }
        private IList<Artikel> ArtikelStammdaten { get; set; }
        private IList<PersonalMaschinen> PersonalMaschinenStammdaten { get; set; }
        public IList<Arbeitsplatz> Arbeitsplätze { get; set; }
        public IList<Kaufteil> Kaufteile { get; set; }

        public SimulationService(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void Initialize(results results)
        {
            LastPeriodResults = results;
            ArtikelStammdaten = _fileRepository.ParseArtikelCsv();
            PersonalMaschinenStammdaten = _fileRepository.ParsePersonalMaschinenCsv();
            Arbeitsplätze = _fileRepository.ParseArbeitsplätzeCsv();
            Kaufteile = _fileRepository.ParseKaufteileCsv();
        }
    }
}