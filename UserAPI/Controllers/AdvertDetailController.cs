using Business.Handlers.Intern.AdvertDetail.Query;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Controllers.BaseController;

namespace UserAPI.Controllers;

public class AdvertDetailController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetDetail(int advertId)
    {
        return Ok(await Mediator.Send(new GetAdvertDetailQuery(){AdvertId = advertId}));
    }
}