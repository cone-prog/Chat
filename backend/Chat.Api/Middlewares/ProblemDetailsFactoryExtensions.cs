using Chat.Domain.Authorization;
using Chat.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chat.Api.Middlewares;

public static class ProblemDetailsFactoryExtensions
{
    public static ProblemDetails CreateFrom(
        this ProblemDetailsFactory factory,
        HttpContext httpContext,
        IntentionManagerException intentionManagerException
    ) =>
        factory.CreateProblemDetails(
            httpContext,
            StatusCodes.Status403Forbidden,
            "Authorization Failed", detail: intentionManagerException.Message);

    public static ProblemDetails CreateFrom(
        this ProblemDetailsFactory factory,
        HttpContext httpContext,
        DomainException domainException
    ) =>
        factory.CreateProblemDetails(
            httpContext,
            statusCode: domainException.ErrorCode switch
            {
                ErrorCode.Gone => StatusCodes.Status410Gone,
                _ => StatusCodes.Status500InternalServerError
            },
            domainException.Message);

    public static ProblemDetails CreateFrom(
        this ProblemDetailsFactory factory,
        HttpContext httpContext,
        ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in validationException.Errors)
        {
            modelStateDictionary.AddModelError(error.PropertyName, error.ErrorCode);
        }

        return factory.CreateValidationProblemDetails(
            httpContext,
            modelStateDictionary,
            StatusCodes.Status400BadRequest,
            "Validation failed"
        );
    }
}
