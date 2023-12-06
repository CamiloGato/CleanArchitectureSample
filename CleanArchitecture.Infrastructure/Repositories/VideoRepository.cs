using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class VideoRepository(StreamerDbContext context) : RepositoryBase<Video>(context), IVideoRepository
{
    public async Task<Video> GetVideoByName(string videoName)
    {
        return (await context.Videos!.Where(v => v.Name == videoName).FirstOrDefaultAsync())!;
    }

    public async Task<IEnumerable<Video>> GetVideoByUserName(string userName)
    {
        return await context.Videos!.Where(v => v.CreatedBy == userName).ToListAsync();
    }
}