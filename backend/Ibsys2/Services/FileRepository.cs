using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using CsvHelper;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Services
{
    public class FileRepository
    {
        private readonly ILogger<FileRepository> _logger;

        private readonly string _dataDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
        
        public FileRepository(ILogger<FileRepository> logger)
        {
            _logger = logger;
        }

        public results ParseResultsXml()
        {
            try
            {
                using var fileStream = new FileStream(Path.Combine(_dataDirectory, "last_period_results.xml"), FileMode.Open);
                var xmlSerializer = new XmlSerializer(typeof(results));
                var results = xmlSerializer.Deserialize(fileStream) as results;

                return results;
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseResultsXml failed:", exception.Message);
                return null;
            }
        }


        public IList<Artikel> ParseArtikelCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(_dataDirectory,"Artikel.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<Artikel>(csvReader.GetRecords<Artikel>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseArtikelCsv failed:", exception.Message);
                return null;
            }
        }   
        
        public IList<PersonalMaschinen> ParsePersonalMaschinenCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(_dataDirectory,"PersonalMaschinen.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csvReader.GetRecords<PersonalMaschinen>().ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError("ParsePersonalMaschinenCsv failed:", exception.Message);
                return null;
            }
        }

        public IList<StuecklistenPosition> ParseStuecklistenAufloesungCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(_dataDirectory, "Stuecklisten_Aufloesung.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<StuecklistenPosition>(csvReader.GetRecords<StuecklistenPosition>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseStuecklistenAufloesungCsv failed:", exception.Message);
                return null;
            }
        }

        public IList<Arbeitsplatz> ParseArbeitsplaetzeCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(_dataDirectory, "Arbeitsplaetze.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<Arbeitsplatz>(csvReader.GetRecords<Arbeitsplatz>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseArbeitsplaetzeCsv failed:", exception.Message);
                return null;
            }
        }
        
        public IList<Lieferdaten> ParseLieferdatenCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(_dataDirectory, "Lieferdaten.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<Lieferdaten>(csvReader.GetRecords<Lieferdaten>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseLieferdatenCsv failed:", exception.Message);
                return null;
            }
        }
    }
}