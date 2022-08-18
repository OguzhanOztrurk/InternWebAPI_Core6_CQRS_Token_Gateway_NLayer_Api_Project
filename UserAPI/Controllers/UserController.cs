using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class UserController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] CreateUserInternCommand createUserInternCommand)
    {
        return Created("",await Mediator.Send(createUserInternCommand));
    }

    [HttpGet("intern")]
    public async Task<IActionResult> GetInternInfo()
    {
        return Ok(await Mediator.Send(new GetUserInternQuery()));
    }

    [HttpPut("intern/update")]
    public async Task<IActionResult> UpdateIntern([FromBody] UpdateUserInternCommand updateUserInternCommand)
    {
        return Ok(await Mediator.Send(updateUserInternCommand));
    }

    [HttpPut("intern/update/password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdateInternPassCommand updateInternPassCommand)
    {
        return Ok(await Mediator.Send(updateInternPassCommand));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteIntern([FromBody] DeleteUserInternCommand deleteUserInternCommand)
    {
        return Ok(await Mediator.Send(deleteUserInternCommand));
    }
}