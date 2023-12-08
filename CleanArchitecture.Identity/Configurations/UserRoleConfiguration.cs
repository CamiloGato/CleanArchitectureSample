using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>()
            {
                UserId = "d51063ce-1197-4728-a116-528401844762",
                RoleId= "3b6ac46f-7cc7-478f-b1ec-23960474b574",
            },
            new IdentityUserRole<string>()
            {
                UserId = "be4b12f7-20fd-4b31-b270-b9f98348d6a7",
                RoleId = "6503c8bc-75aa-4a67-9fad-a69ea0809f12"
            }
        );
    }
}