using Business.Handlers.Intern.Appeal.Commands;
using Business.Handlers.Intern.Appeal.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class AppealController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAppeal()
    {
        return Ok(await Mediator.Send(new GetAppealQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> AddAppeal(int advertId)
    {
        return Created( "",await Mediator.Send(new CreateAppealCommand(){AdvertId = advertId}));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAppealState(int appealId)
    {
        return Created( "",await Mediator.Send(new UpdateAppealStateCommand(){AppealId = appealId}));
    }
    
    [HttpDelete]
    public async Task<IActionResult> AppealDelete(int appealId)
    {
        return Ok(await Mediator.Send(new DeleteAppealCommand(){AppealId = appealId}));
    }
}