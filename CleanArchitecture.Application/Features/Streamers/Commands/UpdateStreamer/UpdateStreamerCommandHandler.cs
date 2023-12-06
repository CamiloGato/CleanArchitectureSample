using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandHandler(
        IStreamerRepository streamerRepository,
        IMapper mapper,
        ILogger logger
    ) : IRequestHandler<UpdateStreamerCommand>
{
    public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToUpdate = await streamerRepository.GetByIdAsync(request.Id);
        if (streamerToUpdate == null)
        {
            logger.LogError($"Could not found the streamer id {request.Id}");
            throw new NotFoundException(nameof(Streamer), request.Id);
        }

        mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));
        await streamerRepository.UpdateAsync(streamerToUpdate);
        logger.LogInformation($"Success Update Streamer {request.Id}");
        
        return Unit.Value;
    }
}