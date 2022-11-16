using System;
using Garnek.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Garnek.WebAPI.Configuration;

public static class DatabaseInstaller
{
	public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<DatabaseContext>(context => context
			.UseSqlServer(configuration
			.GetConnectionString("SqlServer"),
				x => x.MigrationsAssembly("Garnek.WebAPI")));

		return services;
	}
}


