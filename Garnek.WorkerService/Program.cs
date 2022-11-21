using Garnek.Infrastructure.Configuration;
using Garnek.WorkerService;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDatabaseWorkerConnection(hostContext.Configuration);
        services.AddHostedService<DatabaseOldRecordsCleanerService>();
    })
    .Build();

host.Run();