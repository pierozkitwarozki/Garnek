using System.Reflection;
using Garnek.Infrastructure.Configuration;
using Garnek.Infrastructure.DataAccess;
using Garnek.Infrastructure.Services;
using Garnek.WebAPI.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);   
});
builder.Services
    .AddDatabaseConnection(builder.Configuration)
    .AddRepositories();

builder.Services.AddHostedService<DatabaseCleanerService>();

builder.Services.AddCors(x => 
    x.AddPolicy("defaultPolicy", x 
        => x.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()));

builder.Services.AddCors(x => 
    x.AddPolicy("prodPolicy", x 
        => x.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://garnek.azurewebsites.net", 
                "http://garnek.azurewebsites.net")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
//}

app.UseCors(app.Environment.IsDevelopment() ? "defaultPolicy" : "prodPolicy");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<DatabaseContext>();
if (db is not null) await db.Database.MigrateAsync();

app.Run();

