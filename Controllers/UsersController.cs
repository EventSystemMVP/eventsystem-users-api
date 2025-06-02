using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        return Ok(new
        {
            Name = "Anv�ndare X",
            Email = "anvandare@example.com"
        });
    }
}