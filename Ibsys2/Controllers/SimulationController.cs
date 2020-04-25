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
    private readonly ILogger<SimulationController> logger;
    private readonly SimulationService simulationService;

    public SimulationController(
      ILogger<SimulationController> logger,
      SimulationService simulationService)
    {
      this.logger = logger;
      this.simulationService = simulationService;
    }

    [HttpGet]
    public string Simulate()
    {
      try
      {
        simulationService.Initialize();
        return "Simulation successful";
      }
      catch (Exception exception)
      {
        logger.LogError("Simulation failed:", exception);
        return $"Simulation failed: {exception.Message}";
      }
    }
  }
}