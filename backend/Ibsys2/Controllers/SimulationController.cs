using System;
using System.Collections;
using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.KapazitaetsPlan;
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

                var dispoEigenfertigungen = _simulationService.Start(input);
                _simulationService.Kapaplan(dispoEigenfertigungen);
                _logger.LogInformation("Simulation successful");
                return dispoEigenfertigungen;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPut("update-dispo-ef/1")]
        public IList<DispoEFPos> UpdateDispoEfP1([FromBody] IList<DispoEFPos> input)
        {
            try
            {
                return new DispoEFP1(
                    _simulationService.Vertriebswunsch, 
                    _simulationService.Forecast,
                    _simulationService.LastPeriodResults,
                    input
                    ).ListDispoEfPos;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPut("update-dispo-ef/2")]
        public IList<DispoEFPos> UpdateDispoEfP2([FromBody] IList<DispoEFPos> input)
        {
            try
            {
                return new DispoEFP2(
                    _simulationService.Vertriebswunsch,
                    _simulationService.Forecast,
                    _simulationService.LastPeriodResults,
                    input
                    ).ListDispoEfPos;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }

        [HttpPut("update-dispo-ef/3")]
        public IList<DispoEFPos> UpdateDispoEfP3([FromBody] IList<DispoEFPos> input)
        {
            try
            {
                return new DispoEFP3(
                    _simulationService.Vertriebswunsch, 
                    _simulationService.Forecast,
                    _simulationService.LastPeriodResults,
                    input
                    ).ListDispoEfPos;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return null;
            }
        }
    }
}