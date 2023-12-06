using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class VideoActor : BaseDomainModel
{
    public int ActorId { get; set; }
    public int VideoId { get; set; }
}