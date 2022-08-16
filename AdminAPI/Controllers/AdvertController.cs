using AdminAPI.Controllers.BaseController;
using Business.Handlers.Advert.Command;
using Business.Handlers.Advert.Query;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers;

public class AdvertController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAdvert(int workPlaceId)
    {
        return Ok(await Mediator.Send(new GetAdvertQuery(){WorkplaceId = workPlaceId}));
    }

    [HttpPost]
    public async Task<IActionResult> AddAdvert([FromBody] CreateAdvertCommand createAdvertCommand)
    {
        return Created("", await Mediator.Send(createAdvertCommand));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAdvert([FromBody] UpdateAdvertCommand updateAdvertCommand)
    {
        return Ok(await Mediator.Send(updateAdvertCommand));
    }

    [HttpPut("state")]
    public async Task<IActionResult> StateChange([FromBody] StateAdvertCommand stateAdvertCommand)
    {
        return Ok(await Mediator.Send(stateAdvertCommand));
    }

    [HttpPut("delete")]
    public async Task<IActionResult> DeleteAdvert([FromBody] DeleteAdvertCommand deleteAdvertCommand)
    {
        return Ok(await Mediator.Send(deleteAdvertCommand));
    }
}