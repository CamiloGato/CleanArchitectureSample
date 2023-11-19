using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Data;

public class StreamerDbContextFactory : IDesignTimeDbContextFactory<StreamerDbContext>
{
    public StreamerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StreamerDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=streamer;Username=postgres;Password=postgrespw");

        return new StreamerDbContext(optionsBuilder.Options);
    }
}