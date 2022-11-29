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
                
                var teams = context?.Teams.Where(x => x.CreatedAt < DateTime.UtcNow.AddHours(-3));
                var users = teams.SelectMany(x => x.Users).ToList();
                var phrases = users.SelectMany(x => x.Phrases).ToList();
                var games = users.Select(x => x.Game).ToList();

                if (teams != null) context?.RemoveRange(teams);
                if (phrases != null) context?.RemoveRange(phrases);
                if (users != null) context?.RemoveRange(users);
                if (games != null) context?.RemoveRange(games);

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