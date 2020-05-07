using System;
using Ibsys2.Models;
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
      return
        "To start the simulation send a POST request, with the last period results as json body, to this URL.";
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
        _logger.LogError("UploadLastPeriodResults failed:", exception);
        return null;
      }
    }

    [HttpPost("start")]
    public SimulationInput Start([FromBody] SimulationInput input)
    {
      try
      {
        if (_simulationService.LastPeriodResults == null)
          throw new Exception("Upload LastPeriodResults first!");

        _simulationService.Start(input);
        _logger.LogInformation("Simulation successful");
        return input;
      }
      catch (Exception exception)
      {
        _logger.LogError("Simulation failed:", exception);
        return null;
      }
    }
  }
}