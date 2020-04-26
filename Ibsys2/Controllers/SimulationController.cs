using System;
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

        [HttpPost]
        public string Simulate([FromBody] results results)
        {
            try
            {
                _simulationService.Initialize(results);
                return "Simulation successful";
            }
            catch (Exception exception)
            {
                _logger.LogError("Simulation failed:", exception);
                return $"Simulation failed: {exception.Message}";
            }
        }
    }
}