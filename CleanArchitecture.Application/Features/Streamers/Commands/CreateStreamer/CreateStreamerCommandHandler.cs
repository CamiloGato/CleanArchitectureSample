using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommandHandler(
        IStreamerRepository streamerRepository,
        IMapper mapper,
        IEmailService emailService,
        ILogger<CreateStreamerCommandHandler> logger
    ) : IRequestHandler<CreateStreamerCommand, int>
{
    public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerEntity = mapper.Map<Streamer>(request);
        var newStreamer = await streamerRepository.AddAsync(streamerEntity);
        
        logger.LogInformation($"{nameof(Streamer)} {newStreamer.Id} has been created success");
        
        await SendEmail(newStreamer);
        
        return newStreamer.Id;
    }

    private async Task SendEmail(Streamer streamer)
    {
        var email = new Email()
        {
            To = "cm.andres022@hotmail.com",
            Body = $"The streamer '{streamer.Name}' Company has been created success",
            Subject = "Alert Message"
        };

        try
        {
            await emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            logger.LogError($"Error: {e}");
        }
    }
}