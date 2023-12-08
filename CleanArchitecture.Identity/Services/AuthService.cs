using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Identity.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IOptions<JwtSettings> optionsJwtSettings
) : IAuthService
{
    private JwtSettings JwtSettings => optionsJwtSettings.Value;
    
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new Exception($"User with email {request.Email} does not exist.");

        var response = await signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false,
            lockoutOnFailure: false);
        if (!response.Succeeded)
            throw new Exception($"The credentials are wrong");

        var token = await GenerateToken(user);
        var authResponse = new AuthResponse()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email!,
            UserName = user.UserName
        };

        return authResponse;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await userManager.FindByNameAsync(request.UserName);
        if (existingUser != null)
            throw new Exception($"User name assigned in other account");

        var existingEmail = await userManager.FindByEmailAsync(request.Email);
        if (existingEmail != null)
            throw new Exception($"User email assigned in other account");

        var user = new ApplicationUser()
        {
            Email = request.Email,
            Name = request.Name,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Operator");
            var token = await GenerateToken(user);
            return new RegistrationResponse()
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                UserName = user.UserName,
            };
        }

        throw new Exception($"{result.Errors}");
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();
        roleClaims.AddRange(
            roles.Select(r => new Claim(ClaimTypes.Role, r))
        );

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(CustomClaimsTypes.Uid, user.Id),
        }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtSettings.Key)
        );
        var signinCredentials = new SigningCredentials(
            symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256Signature
        );
    
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(JwtSettings.DurationInMinutes),
            signingCredentials: signinCredentials
        );

        return jwtSecurityToken;
    }

}