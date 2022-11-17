using Garnek.Application.Repositories;
using Garnek.Infrastructure.Repositories;

namespace Garnek.WebAPI.Configuration;

public static class RepositoriesInstaller
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
