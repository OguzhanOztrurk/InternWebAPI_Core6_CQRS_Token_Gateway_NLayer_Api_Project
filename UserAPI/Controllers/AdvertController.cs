using Business.Handlers.Intern.Advert.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class AdvertController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetAdvertsQuery()));
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategoryInAdvert(int categoryId)
    {
        return Ok(await Mediator.Send(new GetAdvertInCategoryQuery(){CategoryId = categoryId}));
    }

    [HttpGet("workplace")]
    public async Task<IActionResult> GetWorkplaceInAdvert(int workplaceId)
    {
        return Ok(await Mediator.Send(new GetAdvertsInWorkplaceQuery() { WorkplaceId = workplaceId }));
    }
}