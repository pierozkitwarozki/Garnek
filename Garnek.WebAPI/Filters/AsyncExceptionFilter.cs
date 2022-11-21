using FluentValidation;
using Garnek.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Garnek.WebAPI.Filters;

public class AsyncExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotFoundException notFoundException:
                context.Result = new NotFoundObjectResult(notFoundException.Message);
                return Task.CompletedTask;
            case ValidationException validationException:
                context.Result = new UnprocessableEntityObjectResult(validationException.Message);
                return Task.CompletedTask;
            default:
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                return Task.CompletedTask;
        }
    }
}