using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandler(
        IStreamerRepository streamerRepository,
        ILogger logger
    ) : IRequestHandler<DeleteStreamerCommand>
{
    public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await streamerRepository.GetByIdAsync(request.Id);
        if (streamerToDelete == null)
        {
            logger.LogError($"The Streamer {request.Id} not exist");
            throw new NotFoundException(nameof(Streamer), request.Id);
        }

        await streamerRepository.DeleteAsync(streamerToDelete);
        
        logger.LogInformation($"Streamer {request.Id} deleted success");
        
        return Unit.Value;
    }
}