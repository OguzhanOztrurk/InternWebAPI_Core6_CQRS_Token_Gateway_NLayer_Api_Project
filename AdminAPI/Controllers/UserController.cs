using AdminAPI.Controllers.BaseController;
using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class UserController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] CreateUserAdminCommand createUserAdminCommand)
    {
        return Created("", await Mediator.Send(createUserAdminCommand));
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAdminInfo()
    {
        return Ok(await Mediator.Send(new GetUserAdminQuery()));
    }

    [HttpPut("admin/update")]
    public async Task<IActionResult> UpdateAdmin([FromBody] UpdateUserAdminCommand updateUserAdminCommand)
    {
        return Ok(await Mediator.Send(updateUserAdminCommand));
    }

    [HttpPut("admin/update/password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdateAdminPassCommand updateAdminPassCommand)
    {
        return Ok(await Mediator.Send(updateAdminPassCommand));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAdmin([FromBody] DeleteUserAdminCommand deleteUserAdminCommand)
    {
        return Ok(await Mediator.Send(deleteUserAdminCommand));
    }
}