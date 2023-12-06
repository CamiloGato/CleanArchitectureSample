using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQuery(string userName) : IRequest<List<VideosVm>>
{
    public string UserName { get; set; } = userName;
}