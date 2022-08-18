using Business.Handlers.Intern.WorkHistory.Commands;
using Business.Handlers.Intern.WorkHistory.Queries;
using Business.Handlers.Workplace.Commands;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class WorkHistoryController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetWorkHistory()
    {
        return Ok(await Mediator.Send(new GetWorkHistoryQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkHistory([FromBody] CreateWorkHistoryCommand createWorkHistoryCommand)
    {
        return Created("", await Mediator.Send(createWorkHistoryCommand));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateWorkHistory([FromBody] UpdateWorkHistoryCommand updateWorkHistoryCommand)
    {
        return Ok(await Mediator.Send(updateWorkHistoryCommand));
    }
    [HttpPut("isactive")]
    public async Task<IActionResult> UpdateWorkHistoryState(int workHistoryId)
    {
        return Ok(await Mediator.Send(new UpdateWorkHistoryStateCommand(){WorkHistoryId = workHistoryId}));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteWorkHistory(int workhistoryId)
    {
        return Ok(await Mediator.Send(new DeleteWorkHistoryCommand(){WorkHistoryId = workhistoryId}));
    }
}