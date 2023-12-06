using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQueryHandler(
        IVideoRepository videoRepository,
        IMapper mapper
        ) : IRequestHandler<GetVideosListQuery, List<VideosVm>> 
{
    public async Task<List<VideosVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
    {
        var videoList = await videoRepository.GetVideoByUserName(request.UserName);
        return mapper.Map<List<VideosVm>>(videoList);
    }
    
}