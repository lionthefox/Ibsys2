using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.KapazitaetsPlan;
using Ibsys2.Models.Kaufdispo;
using Ibsys2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Controllers
{
    [ApiController]
    [Route("simulation")]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private readonly SimulationService _simulationService;

        private Regex regex = new Regex(@"<sellwish>[\r\n]+\W*<item article=""\d"" quantity=""\d+"" (price=""0"" penalty=""0"")\W\/>[\r\n]+\W*<item article=""\d"" quantity=""\d+"" (price=""0"" penalty=""0"")\W\/>[\r\n]+\W*<item article=""\d"" quantity=""\d+"" (price=""0"" penalty=""0"")\W\/>[\r\n]+\W*sellwish>", RegexOptions.Compiled);

        public SimulationController(
            ILogger<SimulationController> logger,
            SimulationService simulationService)
        {
            _logger = logger;
            _simulationService = simulationService;
        }

        [HttpGet]
        public string Info()
        {
            return "Backend ready for requests.";
        }

        [HttpPost("results-input")]
        public results UploadLastPeriodResults([FromBody] results results)
        {
            try
            {
                _simulationService.SetResults(results);
                return _simulationService.LastPeriodResults;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPost("start")]
        public DispoEigenfertigungen Start([FromBody] SimulationInput input)
        {
            try
            {
                if (_simulationService.LastPeriodResults == null)
                    throw new Exception("Upload LastPeriodResults first!");

                var dispoEigenfertigungen = _simulationService.GetEfDispo(input);
                _logger.LogInformation("Simulation successful");
                return dispoEigenfertigungen;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPut("update-dispo-ef/p1")]
        public IList<DispoEFPos> UpdateDispoEfP1([FromBody] IList<DispoEFPos> input)
        {
            try
            {
                var dispoEFP1 = new DispoEFP1(
                    _simulationService.Vertriebswunsch,
                    _simulationService.Forecast,
                    _simulationService.LastPeriodResults,
                    _simulationService.ArtikelStammdaten,
                    input
                ).ListDispoEfPos;
                _simulationService.DispoEigenfertigungen.P1 = dispoEFP1;
                return dispoEFP1;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPut("update-dispo-ef/p2")]
        public IList<DispoEFPos> UpdateDispoEfP2([FromBody] IList<DispoEFPos> input)
        {
            try
            {
               var dispoEFP2 = new DispoEFP2(
                   _simulationService.Vertriebswunsch,
                   _simulationService.Forecast,
                   _simulationService.LastPeriodResults,
                   _simulationService.ArtikelStammdaten,
                   input
               ).ListDispoEfPos;
                _simulationService.DispoEigenfertigungen.P2 = dispoEFP2;
                return dispoEFP2;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPut("update-dispo-ef/p3")]
        public IList<DispoEFPos> UpdateDispoEfP3([FromBody] IList<DispoEFPos> input)
        {
            try
            {
                var dispoEFP3 = new DispoEFP3(
                    _simulationService.Vertriebswunsch, 
                    _simulationService.Forecast,
                    _simulationService.LastPeriodResults,
                    _simulationService.ArtikelStammdaten,
                    input
                ).ListDispoEfPos;
                _simulationService.DispoEigenfertigungen.P3 = dispoEFP3;
                return dispoEFP3;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpGet("kapa-plan")]
        public IList<KapazitaetsPlan> GetKapazitaetsPlaene()
        {
            try
            {
                return _simulationService.GetKapaPlaene();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }        
        
        [HttpGet("kauf-dispo")]
        public IList<KaufdispoPos> GetKaufDispos()
        {
            try
            {
                return _simulationService.GetKaufDispos();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }
        
        [HttpPut("update-kauf-dispo")]
        public IList<KaufdispoPos> UpdateKaufDispo([FromBody] IList<KaufdispoPos> input)
        {
            try
            {
                return _simulationService.GetKaufDispos(input);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPost("convert-to-xml")]
        public string ConvertToXml([FromBody] Input json)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(json.GetType());

                using var ms = new MemoryStream();
                using var xw = XmlWriter.Create(ms,
                    new XmlWriterSettings
                    {
                        Encoding = new UTF8Encoding(false),
                        Indent = true,
                        NewLineOnAttributes = false,


                    });
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xmlSerializer.Serialize(xw, json, ns);
                var xmlString = Encoding.UTF8.GetString(ms.ToArray());

                var x = regex.Replace(xmlString,
                    $@"<sellwish>
    <item article=""{json.sellwish[0].article}"" quantity=""{json.sellwish[0].quantity}"" />
    <item article=""{json.sellwish[1].article}"" quantity= ""{json.sellwish[1].quantity}"" />
    <item article=""{json.sellwish[2].article}"" quantity= ""{json.sellwish[2].quantity}"" />
  </sellwish>"
                );

                return x;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return exception.Message;
            }
        }
    }
}