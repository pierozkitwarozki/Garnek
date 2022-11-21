using Garnek.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Garnek.Infrastructure.Configuration;

public static class DatabaseInstaller
{
	public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<DatabaseContext>(b => b
			.UseLazyLoadingProxies()
			.UseSqlServer(configuration
			.GetConnectionString("SqlServer"),
				x => x.MigrationsAssembly("Garnek.WebAPI")));

		return services;
	}
	
	public static IServiceCollection AddDatabaseWorkerConnection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContextPool<DatabaseContext>(b => b
			.UseSqlServer(configuration
					.GetConnectionString("SqlServer")));

		return services;
	}
}


