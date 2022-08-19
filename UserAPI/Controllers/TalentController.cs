using Business.Handlers.Intern.Talent.Commands;
using Business.Handlers.Intern.Talent.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class TalentController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetTalent()
    {
        return Ok(await Mediator.Send(new GetTalentQuery()));
    }
    [HttpPost]
    public async Task<IActionResult> AddTalent([FromBody] CreateTalentCommand createTalentCommand)
    {
        return Created("", await Mediator.Send(createTalentCommand));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateTalent([FromBody] UpdateTalentCommand updateTalentCommand)
    {
        return Ok(await Mediator.Send(updateTalentCommand));
    }
    [HttpPut("isactive")]
    public async Task<IActionResult> UpdateTalentState(int talentId)
    {
        return Ok(await Mediator.Send(new UpdateTalentStateCommand(){TalentId = talentId}));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteTalent(int talentId)
    {
        return Ok(await Mediator.Send(new DeleteTalentCommand(){TalenId = talentId}));
    }
}