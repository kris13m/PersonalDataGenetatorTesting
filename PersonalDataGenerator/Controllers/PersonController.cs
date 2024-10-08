using Microsoft.AspNetCore.Mvc;

namespace PersonalDataGenerator.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    [HttpGet]
    public string GetHello()
    {
        return "Hello";
    }
}