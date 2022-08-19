using Business.Handlers.Intern.Appeal.Queries;
using Business.Handlers.Intern.AppealEvaluation.Commands;
using Business.Handlers.Intern.AppealEvaluation.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

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
        return Ok(await Mediator.Send(new DeleteAppealEvulationCommand(){AppealId = appealId}));
    }
}