using Business.Handlers.Intern.Workplace.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class WorkplaceController : BaseApiController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetWorkplacesQuery()));
    }
}