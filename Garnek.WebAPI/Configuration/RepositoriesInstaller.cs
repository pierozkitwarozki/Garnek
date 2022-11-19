using Garnek.Application.Repositories;
using Garnek.Infrastructure.Repositories;

namespace Garnek.WebAPI.Configuration;

public static class RepositoriesInstaller
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPhraseRepository, PhraseRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
