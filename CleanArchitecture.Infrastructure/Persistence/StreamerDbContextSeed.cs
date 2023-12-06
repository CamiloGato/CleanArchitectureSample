using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence;

public class StreamerDbContextSeed
{
    public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContext> logger)
    {
        if (!context.Streamers!.Any())
        {
            context.Streamers!.AddRange(GetPreconfiguredStreamer());
            await context.SaveChangesAsync();
            logger.LogInformation($"New records added to DataBase {context}", typeof(StreamerDbContext));
        }
    }

    private static IEnumerable<Streamer> GetPreconfiguredStreamer()
    {
        return new List<Streamer>()
        {
            new()
            {
                CreatedBy = "camilo",
                Name = "HBO Plus",
                Url = $"hboPlus.com"
            },
            new()
            {
                CreatedBy = "camilo",
                Name = "Netflix Plus",
                Url = $"netflixPlus.com"
            },
            new(){
                CreatedBy = "camilo",
                Name = "Prime Plus",
                Url = $"primePlus.com"
            },
        };
    }
}