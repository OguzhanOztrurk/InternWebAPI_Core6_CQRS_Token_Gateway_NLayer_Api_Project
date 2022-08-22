using AdminAPI.Controllers.BaseController;
using Business.Handlers.Admin.WorkplaceIntern.Commands;
using Business.Handlers.Admin.WorkplaceIntern.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class WorkplaceInternController : BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetWorkplaceInternList()
    {
        return Ok(await Mediator.Send(new GetWorkplaceInternQuery()));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStateWorkplaceIntern(int workplaceInternId, bool workplaceInternState)
    {
        return Ok(await Mediator.Send(new UpdateWorkplaceInternStateCommand(){workplaceInternId = workplaceInternId, WorkplaceInternState = workplaceInternState}));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteWorkplaceIntern(int workplaceInternID)
    {
        return Ok(await Mediator.Send(new DeleteWorkplaceInternCommand() { WorkplaceInterId = workplaceInternID }));
    }
}