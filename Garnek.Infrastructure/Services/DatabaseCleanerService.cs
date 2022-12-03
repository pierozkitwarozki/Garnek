using Garnek.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Garnek.Infrastructure.Services;

public class DatabaseCleanerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseCleanerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetService<DatabaseContext>();

                if (context is null)
                {
                    await Task.Delay(30000, stoppingToken);
                    continue;
                };

                var teams = await context.Teams
                    .Where(x => x.CreatedAt <= DateTime.UtcNow.AddHours(-2))
                    .ToListAsync(stoppingToken);
                var users = await context.Users
                    .Where(x => x.CreatedAt <= DateTime.UtcNow.AddHours(-2))
                    .ToListAsync(stoppingToken);
                var phrases = await context.Phrases
                    .Where(x => x.CreatedAt <= DateTime.UtcNow.AddHours(-2))
                    .ToListAsync(stoppingToken);
                var games = await context.Games
                    .Where(x => x.CreatedAt <= DateTime.UtcNow.AddHours(-2))
                    .ToListAsync(stoppingToken);
                
                context.RemoveRange(teams);
                context.RemoveRange(phrases);
                context.RemoveRange(users);
                context.RemoveRange(games);

                await context.SaveChangesAsync(stoppingToken);
                await context.DisposeAsync();
                const int hours = 1;
                await Task.Delay(hours * 60 * 60 * 1000, stoppingToken);
            }
            catch (Exception ex)
            {
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}