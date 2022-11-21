using Garnek.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Garnek.WorkerService;

public class DatabaseOldRecordsCleanerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseOldRecordsCleanerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<DatabaseContext>();
            const string query = """
                EXECUTE [dbo].[sp_DeleteOldRecords] 
            """;
            await context.Database.ExecuteSqlRawAsync(query, stoppingToken);
            await context.DisposeAsync();
            const int hours = 1;
            await Task.Delay(hours * 60 * 60 * 1000, stoppingToken);
        }
    }
}