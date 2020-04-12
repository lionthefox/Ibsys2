using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CsvHelper;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Services
{
    public class ParserService
    {
        private readonly ILogger<ParserService> _logger;

        public ParserService(ILogger<ParserService> logger)
        {
            _logger = logger;
        }

        public results ParseResultsXml()
        {
            try
            {
                using var fileStream = new FileStream(@".\Data\resultServlet.xml", FileMode.Open);
                var xmlSerializer = new XmlSerializer(typeof(results));
                var results = xmlSerializer.Deserialize(fileStream) as results;
                return results;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }


        public IList<Artikel> ParseArtikelCsv()
        {
            try
            {
                using var reader = new StreamReader(@".\Data\Artikel.csv");
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return new List<Artikel>(csvReader.GetRecords<Artikel>().ToList());csvReader.GetRecords<Artikel>().ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }   
        
        public IList<PersonalMaschinen> ParsePersonalMaschinenCsv()
        {
            try
            {
                using var reader = new StreamReader(@".\Data\PersonalMaschinen.csv");
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csvReader.GetRecords<PersonalMaschinen>().ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }
    }
}