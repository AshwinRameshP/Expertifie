using ExpertifieWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserSettings _userSettings;

    public UserController(IOptions<UserSettings> userSettings)
    {
        _userSettings = userSettings.Value;
    }

    [HttpGet("info")]
    public IActionResult GetUserInfo()
    {
        return Ok(new
        {
            Name = _userSettings.DefaultName,
            City = _userSettings.City
        });
    }
}