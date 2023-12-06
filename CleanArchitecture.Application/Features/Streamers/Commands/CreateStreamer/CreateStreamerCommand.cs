using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommand(string name, string url) : IRequest<int>
{
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
}