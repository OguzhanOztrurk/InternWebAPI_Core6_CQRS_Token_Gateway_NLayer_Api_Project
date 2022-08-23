using Business.Handlers.Intern.AdvertCategory.Queries;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class AdvertCategoryController : BaseApiController
{
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await Mediator.Send(new GetCategoriesQuery()));
    }
}