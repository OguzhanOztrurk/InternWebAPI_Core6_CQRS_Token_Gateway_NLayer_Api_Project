using Business.Handlers.Intern.Education.Commands;
using Business.Handlers.Intern.Education.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class EducationController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetEducationList()
    {
        return Ok(await Mediator.Send(new GetEducationQuery()));
    }
    [HttpPost]
    public async Task<IActionResult> AddEducation([FromBody] CreateEducationCommand createEducationCommand)
    {
        return Created("", await Mediator.Send(createEducationCommand));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateEducation([FromBody] UpdateEducationCommand updateEducationCommand)
    {
        return Ok(await Mediator.Send(updateEducationCommand));
    }
    [HttpPut("state")]
    public async Task<IActionResult> EducationUpdateState(int educationId)
    {
        return Ok(await Mediator.Send(new UpdateEducationStateCommand(){EducationId = educationId}));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteEducation(int educationId)
    {
        return Ok(await Mediator.Send(new DeleteEducationCommand(){EducationId = educationId}));
    }
}