using Chat.Domain.Authorization;
using Chat.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Chat.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext,
        ProblemDetailsFactory problemDetailsFactory)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            var problemDetails = exception switch
            {
                IntentionManagerException intentionManagerException
                    => problemDetailsFactory.CreateFrom(httpContext, intentionManagerException),

                DomainException domainException
                    => problemDetailsFactory.CreateFrom(httpContext, domainException),

                ValidationException validationException
                    => problemDetailsFactory.CreateFrom(httpContext, validationException),

                _ => problemDetailsFactory.CreateProblemDetails(httpContext, StatusCodes.Status500InternalServerError,
                    "Unhandled error! Please contact us.", detail: exception.Message)
            };

            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            if (problemDetails is ValidationProblemDetails validationProblemDetails)
                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails);
            else await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
