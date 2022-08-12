using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class DenemeController : BaseApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new List<string> { "deneme", "deneme2" });
    }
}