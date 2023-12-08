using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Id = "3b6ac46f-7cc7-478f-b1ec-23960474b574",
                Name = "Amin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole()
            {
                Id = "6503c8bc-75aa-4a67-9fad-a69ea0809f12",
                Name = "Operator",
                NormalizedName = "OPERATOR"
            }
        );
    }
}