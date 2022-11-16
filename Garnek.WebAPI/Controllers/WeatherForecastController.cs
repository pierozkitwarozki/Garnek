using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garnek.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public WeatherForecastController(DatabaseContext context)
    {
        _context = context;
    }

    private readonly DatabaseContext _context;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Empty<WeatherForecast>();
    }

    [HttpPost(Name = "AddWeatherForecast")]
    public bool Add()
    {
        _context.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        });
        _context.SaveChanges();
        return true;

    }
}

