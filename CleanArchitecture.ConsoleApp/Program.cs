
using CleanArchitecture.Data;
using CleanArchitecture.Domain;

var builder = new StreamerDbContextFactory();
await using var dbContext = builder.CreateDbContext(args);


await dbContext?.SaveChangesAsync()!;

var movies = new List<Video>()
{
    new()
    {
        Name = "The Matrix",
        Url = "https://www.netflix.com/watch/20557937",
        StreamerId = 1,
    },
    new()
    {
        Name = "The Matrix Reloaded",
        Url = "https://www.netflix.com/watch/60021228",
        StreamerId = 1,
    },
    new()
    {
        Name = "The Matrix Revolutions",
        Url = "https://www.netflix.com/watch/60024942",
        StreamerId = 1,
    },
    new()
    {
        Name = "The Matrix",
        Url = "https://www.primevideo.com/detail/The-Matrix/0GUR2L4N7JFVXZQYQYQYQYQYQY",
        StreamerId = 2,
    },
    new()
    {
        Name = "The Matrix Reloaded",
        Url = "https://www.primevideo.com/detail/The-Matrix-Reloaded/0GUR2L4N7JFVXZQYQYQYQYQYQY",
        StreamerId = 2,
    },
    new()
    {
        Name = "The Matrix Revolutions",
        Url = "https://www.primevideo.com/detail/The-Matrix-Revolutions/0GUR2L4N7JFVXZQYQYQYQYQYQY",
        StreamerId = 2,
    },
};

await dbContext?.Videos?.AddRangeAsync(movies)!;

await dbContext?.SaveChangesAsync()!;