using Garnek.Infrastructure.Configuration;
using Garnek.Infrastructure.DataAccess;
using Garnek.WebAPI.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(builder.Configuration.GetConnectionString("SqlServer"));
builder.Services.AddControllers(opt => opt.Filters.Add<AsyncExceptionFilter>());
builder.Services.RegisterValidators();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Garnek.WebAPI",
        Description = "An ASP.NET Core Web API for mobile game - Garnek",
        Contact = new OpenApiContact
        {
            Name = "LinkedIn",
            Url = new Uri("https://www.linkedin.com/in/bartlomiejpierog98/")
        }
    });
});
builder.Services
    .AddDatabaseConnection(builder.Configuration)
    .AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<DatabaseContext>();
if (db is not null) await db.Database.MigrateAsync();

app.Run();

