using Garnek.Application.Services;
using Garnek.Infrastructure.Services;
using HashidsNet;

namespace Garnek.WebAPI.Configuration;

public static class ServicesInstaller
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var salt = configuration.GetSection("Hashid:Salt").Value;
        var alphabet = configuration.GetSection("Hashid:Alphabet").Value;
        
        services.AddSingleton<IHashids>(new Hashids(salt, alphabet: alphabet));
        services.AddSingleton<IHashidsService, HashidService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}