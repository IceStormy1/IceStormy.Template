using Microsoft.AspNetCore.Mvc;

namespace IceStormy.Template.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(ILogger<TestController> logger) : BaseController
{
    [HttpGet]
    public IActionResult Test()
    {
        logger.LogInformation("Success");
        return Ok();
    }

    [HttpPost]
    public void ThrowArgumentException()
    {
        throw new ArgumentException("Test exception");
    }
}