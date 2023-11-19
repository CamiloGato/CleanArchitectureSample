
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

var builder = new StreamerDbContextFactory();
await using var dbContext = builder.CreateDbContext(args);

await AddNewRecords(dbContext);
QueryStreamers(dbContext);
return;

void QueryStreamers(StreamerDbContext contextDb)
{
    var videos = contextDb.Videos?.Include(video => video.Streamer).ToList();
    if (videos is null) return;
    foreach (var video in videos)
    {
        Console.WriteLine($"Video: {video.Name}");
        Console.WriteLine($"Streamer: {video.Streamer?.Name}");
        Console.WriteLine($"Url: {video.Url}");
        Console.WriteLine();
    }
}

async Task AddNewRecords(StreamerDbContext contextDb)
{
    var streamer = new Streamer()
    {
        Name = "Disney",
        Url = "https://www.disneyplus.com/",
    };
    
    contextDb.Streamers?.Add(streamer);
    await contextDb.SaveChangesAsync();
    
    var movies = new List<Video>()
    {
        new Video()
        {
            Name = "The Mandalorian",
            Url = "https://www.disneyplus.com/series/the-mandalorian/3jLIGMDYINqD",
            StreamerId = streamer.Id,
        },
        new Video()
        {
            Name = "WandaVision",
            Url = "https://www.disneyplus.com/series/wandavision/4SrN28ZjDLwH",
            StreamerId = streamer.Id,
        },
        new Video()
        {
            Name = "The Falcon and the Winter Soldier",
            Url = "https://www.disneyplus.com/series/the-falcon-and-the-winter-soldier/4gglDBMx8icA",
            StreamerId = streamer.Id,
        },
        new Video()
        {
            Name = "Loki",
            Url = "https://www.disneyplus.com/series/loki/6pARMvILBGzF",
            StreamerId = streamer.Id,
        },
    };
    
    await contextDb.Videos?.AddRangeAsync(movies)!;
    await contextDb.SaveChangesAsync();
}