using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
}