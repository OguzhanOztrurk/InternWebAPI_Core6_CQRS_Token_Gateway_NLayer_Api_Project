using Business.Handlers.Users.Queries;
using IdentityAPI.Controllers.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers;

public class UserController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("admin/login")]
    public async Task<IActionResult> Login(string userName,string password)
    {
        return Ok(await Mediator.Send(new GetLoginAdminQuery() {UserName = userName, Password = password}));
    }
    
     
}