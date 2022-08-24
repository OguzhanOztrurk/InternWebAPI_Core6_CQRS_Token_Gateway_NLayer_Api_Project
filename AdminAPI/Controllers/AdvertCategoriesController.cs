using AdminAPI.Controllers.BaseController;
using Business.Handlers.Admin.AdvertCategory.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class AdvertCategoriesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetAdvertCategoryQuery()));
    }
}