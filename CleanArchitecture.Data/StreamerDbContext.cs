using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data;

public class StreamerDbContext : DbContext
{
    public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streamer>()
            .HasMany(m => m.Videos)
            .WithOne(m => m.Streamer)
            .HasForeignKey(m => m.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Video>()
            .HasMany(v => v.Actors)
            .WithMany(a => a.Videos)
            .UsingEntity<VideoActor>(
                pk =>
                    pk.HasKey( va => new { va.ActorId, va.VideoId} )
                );

    }
    
    public DbSet<Streamer>? Streamers { get; set; }
    public DbSet<Video>? Videos { get; set; }

}