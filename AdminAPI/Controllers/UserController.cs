using AdminAPI.Controllers.BaseController;
using Business.Handlers.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class UserController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] CreateUserAdminCommand createUserAdminCommand)
    {
        return Created("", await Mediator.Send(createUserAdminCommand));
    }
}