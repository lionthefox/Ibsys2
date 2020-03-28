using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ibsys2.Controllers
{
  [ApiController]
  [Route("test")]
  public class TestController : ControllerBase
  {
    private readonly ILogger<TestController> logger;

    public TestController(ILogger<TestController> logger)
    {
      this.logger = logger;
    }

    [HttpGet("get")]
    public object Get()
    {
      return JsonSerializer.Serialize(ParseResultsXml());
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
        logger.LogError(exception, exception.Message);
        return null;
      }
    }
  }
}
