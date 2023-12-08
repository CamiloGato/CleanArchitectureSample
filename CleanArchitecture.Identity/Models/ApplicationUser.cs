using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}