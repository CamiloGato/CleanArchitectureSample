using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data;

public class StreamerDbContextFactory : IDesignTimeDbContextFactory<StreamerDbContext>
{
    public StreamerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StreamerDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost:5432;Database=streamer;Username=postgres;Password=postgrespw"   
        );

        optionsBuilder.LogTo(
            Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information
        );
        
        return new StreamerDbContext(optionsBuilder.Options);
    }
}