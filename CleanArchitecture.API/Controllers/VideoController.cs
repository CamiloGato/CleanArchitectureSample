using System.Net;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VideoController(
    IMediator mediator
    ) : ControllerBase
{
    [HttpGet("{username}", Name = "GetVideo")]
    [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUserNameAsync(string username)
    {
        var query = new GetVideosListQuery(username);
        var videos = await mediator.Send(query);
        return Ok(videos);
    }
}