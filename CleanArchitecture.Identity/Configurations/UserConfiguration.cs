using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser()
            {
                Id = "d51063ce-1197-4728-a116-528401844762",
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com",
                Name = "Camilo",
                LastName = "Rojas",
                UserName = "CamiloRojas",
                NormalizedUserName = "camilorojas",
                PasswordHash = hasher.HashPassword(null!, "camilorojas"),
            },new ApplicationUser()
            {
                Id = "be4b12f7-20fd-4b31-b270-b9f98348d6a7",
                Email = "test@localhost.com",
                NormalizedEmail = "test@localhost.com",
                Name = "Test",
                LastName = "Test",
                UserName = "Test",
                NormalizedUserName = "testtest",
                PasswordHash = hasher.HashPassword(null!, "test"),
            }
        );

    }
}