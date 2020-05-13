﻿using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Services
{
    public class SimulationService
    {
        private readonly FileRepository _fileRepository;
        private readonly StuecklistenService _stuecklistenService;
        private readonly DispoEfService _dispoEfService;
        private readonly ErgebnisseVorperiodeService _ergebnisseVorperiodeService;
        private readonly ArbeitsplatzAufloesenService _arbeitsplatzAufloesenService;
        private readonly KapazitaetService _kapazitaetService;
        public results LastPeriodResults { get; set; }
        public IList<Artikel> ArtikelStammdaten { get; set; }
        public IList<PersonalMaschinen> PersonalMaschinenStammdaten { get; set; }
        public IList<Arbeitsplatz> Arbeitsplaetze { get; set; }
        public IList<StuecklistenPosition> Stueckliste { get; set; }
        public IList<StuecklistenAufloesung> StuecklistenAufloesungen { get; set; }
        public IList<ArbeitsplatzNachfolger> ArbeitsplatzAufloesungen { get; set; }
        public DispoEigenfertigungen DispoEigenfertigungen { get; set; }

    public Vertriebswunsch Vertriebswunsch { get; set; }
        public Forecast Forecast { get; set; }

        public SimulationService(
            FileRepository fileRepository,
            StuecklistenService stuecklistenService,
            DispoEfService dispoEfService,
            ErgebnisseVorperiodeService ergebnisseVorperiodeService,
            ArbeitsplatzAufloesenService arbeitsplatzAufloesenService,
            KapazitaetService kapazitaetService)
        {
            _fileRepository = fileRepository;
            _stuecklistenService = stuecklistenService;
            _dispoEfService = dispoEfService;
            _ergebnisseVorperiodeService = ergebnisseVorperiodeService;
            _arbeitsplatzAufloesenService = arbeitsplatzAufloesenService;
            _kapazitaetService = kapazitaetService;

            Initialize();
        }

        private void Initialize()
        {
            ParseStammdaten();
            StuecklistenAufloesungen = _stuecklistenService.Stuecklistenaufloesung(Stueckliste, ArtikelStammdaten);
            ArbeitsplatzAufloesungen = _arbeitsplatzAufloesenService.ArbeitsplatzAuflösen(Arbeitsplaetze, Stueckliste);
        }
        
        public void SetResults(results results)
        {
            LastPeriodResults = results;
        }

        public void Start(SimulationInput input)
        {
            Vertriebswunsch = input.Vertriebswunsch;
            Forecast = input.Forecast;
            _ergebnisseVorperiodeService.GetErgebnisse(LastPeriodResults, ArtikelStammdaten, ArbeitsplatzAufloesungen, Stueckliste);

            DispoEigenfertigungen = _dispoEfService.GetEfDispo(Vertriebswunsch, Forecast, LastPeriodResults, _ergebnisseVorperiodeService, ArtikelStammdaten);
        }


        public void Kapaplan(DispoEigenfertigungen dispoEigenfertigungen)
        {
            _kapazitaetService.clalcKapaPlan(dispoEigenfertigungen, ArtikelStammdaten);
        }

        private void ParseStammdaten()
        {
            ArtikelStammdaten = _fileRepository.ParseArtikelCsv();
            PersonalMaschinenStammdaten = _fileRepository.ParsePersonalMaschinenCsv();
            Arbeitsplaetze = _fileRepository.ParseArbeitsplaetzeCsv();
            Stueckliste = _fileRepository.ParseStuecklistenAufloesungCsv();
        }
    }
}