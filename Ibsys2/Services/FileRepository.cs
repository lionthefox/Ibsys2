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
    public class FileRepository
    {
        private readonly ILogger<FileRepository> logger;

        public FileRepository(ILogger<FileRepository> logger)
        {
            this.logger = logger;
        }

        public results ParseResultsXml()
        {
            try
            {
                using var fileStream = new FileStream(@".\Data\last_period_results.xml", FileMode.Open);
                var xmlSerializer = new XmlSerializer(typeof(results));
                var results = xmlSerializer.Deserialize(fileStream) as results;
                return results;
            }
            catch (Exception exception)
            {
                logger.LogError("ParseResultsXml failed:", exception);
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
                logger.LogError("ParseArtikelCsv failed:", exception);
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
                logger.LogError("ParsePersonalMaschinenCsv failed:", exception);
                return null;
            }
        }
    }
}