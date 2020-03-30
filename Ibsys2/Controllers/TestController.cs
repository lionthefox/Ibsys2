using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Controllers
{
  [ApiController]
  [Route("test")]
  public class TestController : ControllerBase
  {
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
      _logger = logger;
    }

    [HttpGet("get")]
    public results Get()
    {
      return ParseResultsXml();
    }

    private results ParseResultsXml()
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
  }
}
