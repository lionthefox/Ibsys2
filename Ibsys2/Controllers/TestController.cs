using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ParserService _parserService;

        public TestController(
            ILogger<TestController> logger,
            ParserService parserService
        )
        {
            _logger = logger;
            _parserService = parserService;
        }

        [HttpGet("parse-results")]
        public results ParseResults()
        {
            return _parserService.ParseResultsXml();
        }

        [HttpGet("parse-artikel")]
        public IList<Artikel> ParseArticles()
        {
            return _parserService.ParseArtikelCsv();
        }

        [HttpGet("parse-personal-maschinen")]
        public IList<PersonalMaschinen> ParsePersonalMaschinen()
        {
            return _parserService.ParsePersonalMaschinenCsv();
        }
    }
}