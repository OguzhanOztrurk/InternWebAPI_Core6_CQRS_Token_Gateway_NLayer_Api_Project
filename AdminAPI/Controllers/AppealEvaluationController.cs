using AdminAPI.Controllers.BaseController;
using Business.Handlers.Admin.AppealEvaluation.Commands;
using Business.Handlers.Admin.AppealEvaluation.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class AppealEvaluationController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAppealEvaluation()
    {
        return Ok(await Mediator.Send(new GetAppealEvaluationQuery()));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAppealEvaluation(int appealId)
    {
        return Ok(await Mediator.Send(new DeleteAppealEvaluationCommand(){AppealId = appealId}));
    }
}