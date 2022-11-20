using FluentValidation;
using Garnek.Infrastructure.Validators;
using Garnek.Model.Dtos.Request;

namespace Garnek.WebAPI.Configuration;

public static class ValidationInstaller
{
    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<InitializeGameRequest>, InitializeGameRequestValidator>();
        services.AddScoped<IValidator<AddPhrasesRequest>, AddPhrasesRequestValidator>();

        return services;
    }
}