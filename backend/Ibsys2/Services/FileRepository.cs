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

        private readonly string dataDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
        
        public FileRepository(ILogger<FileRepository> logger)
        {
            _logger = logger;
        }

        public results ParseResultsXml()
        {
            try
            {
                using var fileStream = new FileStream(Path.Combine(dataDirectory, "last_period_results.xml"), FileMode.Open);
                var xmlSerializer = new XmlSerializer(typeof(results));
                var results = xmlSerializer.Deserialize(fileStream) as results;

                return results;
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseResultsXml failed:", exception);
                return null;
            }
        }


        public IList<Artikel> ParseArtikelCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(dataDirectory,"Artikel.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<Artikel>(csvReader.GetRecords<Artikel>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseArtikelCsv failed:", exception);
                return null;
            }
        }   
        
        public IList<PersonalMaschinen> ParsePersonalMaschinenCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(dataDirectory,"PersonalMaschinen.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csvReader.GetRecords<PersonalMaschinen>().ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError("ParsePersonalMaschinenCsv failed:", exception);
                return null;
            }
        }

        public IList<StücklistenPosition> ParseStücklistenAuflösungCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(dataDirectory, "Stücklisten_Auflösung.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<StücklistenPosition>(csvReader.GetRecords<StücklistenPosition>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseStücklistenAuflösungCsv failed:", exception);
                return null;
            }
        }

        public IList<Arbeitsplatz> ParseArbeitsplätzeCsv()
        {
            try
            {
                using var reader = new StreamReader(Path.Combine(dataDirectory, "Arbeitsplätze.csv"));
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<Arbeitsplatz>(csvReader.GetRecords<Arbeitsplatz>().ToList());
            }
            catch (Exception exception)
            {
                _logger.LogError("ParseArbeitsplätzeCsv failed:", exception);
                return null;
            }
        }
    }
}