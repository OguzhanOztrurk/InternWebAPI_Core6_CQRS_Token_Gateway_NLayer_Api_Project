using AdminAPI.Controllers.BaseController;
using Business.Handlers.Admin.Appeal.Commands;
using Business.Handlers.Admin.Appeal.Query;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class AppealController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetAppealQuery()));
    }
    [HttpGet("intern/info")]
    public async Task<IActionResult> GetInternInfo(int appealId)
    {
        return Ok(await Mediator.Send(new GetAppealInternInfoQuery(){AppealId = appealId}));
    }
    [HttpPost]
    public async Task<IActionResult> AddAppealEvaluation([FromBody] CreateAppealEvaluationCommand createAppealEvaluationCommand)
    {
        return Created("", await Mediator.Send(createAppealEvaluationCommand));
    }
}