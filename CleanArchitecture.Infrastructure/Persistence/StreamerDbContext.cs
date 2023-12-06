using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public class StreamerDbContext(DbContextOptions<StreamerDbContext> options) : DbContext(options)
{

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "system";
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "system";
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streamer>()
            .HasMany(m => m.Videos)
            .WithOne(m => m.Streamer)
            .HasForeignKey(m => m.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

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