using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

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
    public object Get()
    {
      return new
      {
        Name = "Test",
        Author = "Leon",
        Date = DateAndTime.Now
      };
    }
  }
}
