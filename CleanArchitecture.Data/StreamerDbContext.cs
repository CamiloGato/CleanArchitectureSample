using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data;

public class StreamerDbContext : DbContext
{
    public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options) {}
    
    public DbSet<Streamer>? Streamers { get; set; }
    public DbSet<Video>? Videos { get; set; }

}