using AdminAPI.Controllers.BaseController;
using Business.Handlers.Workplace.Commands;
using Business.Handlers.Workplace.Queries;
using DataAccess.Concrete.Enum;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class WorkplaceController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return Ok(await Mediator.Send(new GetWorkplaceQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkplace([FromBody] CreateWorkplaceCommand createWorkplaceCommand)
    {
        return Created("", await Mediator.Send(createWorkplaceCommand));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWorkplace([FromBody] UpdateWorkplaceCommand updateWorkplaceCommand)
    {
        return Ok(await Mediator.Send(updateWorkplaceCommand));
    }

    [HttpPut("isactive")]
    public async Task<IActionResult> UpdateState([FromBody] StateWorkplaceCommand stateWorkplaceCommand)
    {
        return Ok(await Mediator.Send(stateWorkplaceCommand));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteWorkplace([FromBody] DeleteWorkplaceCommand deleteWorkplaceCommand)
    {
        return Ok(await Mediator.Send(deleteWorkplaceCommand));
    }
    /*[HttpGet("{id}")]
    public async Task<IActionResult> getstate(EducationLevelEnum abc)
    {
        return Ok(await Mediator.Send(new GetWorkplaceQuery(){Abc = abc}));
    }*/
}