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

    public async Task InvokeAsync(
        HttpContext httpContext,
        ILogger<ErrorHandlingMiddleware> logger,
        ProblemDetailsFactory problemDetailsFactory)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Error has happened with {RequestPath}, the message is {ErrorMessage}",
                httpContext.Request.Path.Value, exception.Message);

            ProblemDetails problemDetails;
            switch (exception)
            {
                case IntentionManagerException intentionManagerException:
                    problemDetails = problemDetailsFactory.CreateFrom(httpContext, intentionManagerException);
                    break;

                case DomainException domainException:
                    problemDetails = problemDetailsFactory.CreateFrom(httpContext, domainException);
                    logger.LogError(exception, "Domain exception occured");
                    break;

                case ValidationException validationException:
                    problemDetails = problemDetailsFactory.CreateFrom(httpContext, validationException);
                    break;

                default:
                    problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, StatusCodes.Status500InternalServerError,
                    "Unhandled error! Please contact us.", detail: exception.Message);
                    logger.LogError(exception, "Unhandled exception occured");
                    break;
            }
            ;

            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            if (problemDetails is ValidationProblemDetails validationProblemDetails)
                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails);
            else await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
