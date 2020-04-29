using System;
using System.Text.Json.Serialization;
using Ibsys2.Models;
using Ibsys2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
            return
                "To start the simulation send a POST request, with the last period results as json body, to this URL.";
        }

        [HttpPost]
        public results Simulate([FromBody] SimulationInput input)
        {
            try
            {
                _simulationService.Initialize(input.Results);
                _logger.LogInformation("Simulation successful");
                return _simulationService.LastPeriodResults;
            }
            catch (Exception exception)
            {
                _logger.LogError("Simulation failed:", exception);
                return null;
            }
        }
    }
}