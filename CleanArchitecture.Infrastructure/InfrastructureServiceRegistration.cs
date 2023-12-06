using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StreamerDbContext>(
            option =>
                option.UseNpgsql(
                    configuration.GetConnectionString("ConnectionString")    
                )
        );
        
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped(typeof(IVideoRepository), typeof(VideoRepository));
        services.AddScoped(typeof(IStreamerRepository), typeof(StreamerRepository));

        services.Configure<EmailSettings>(
            c =>
                configuration.GetSection("EmailSettings")
        );
        services.AddTransient(typeof(IEmailService), typeof(EmailService));
        services.AddSingleton<ILogger>(provider => provider.GetService<ILogger>()!);
        
        return services;
    }
}