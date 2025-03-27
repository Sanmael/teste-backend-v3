using System.Net;
using System.Text.Json;
using FluentValidation;
using TheatricalPlayersRefactoring.Application.Exceptions;

namespace TheatricalPlayersRefactoring.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private record ErrorResponse(
        int Status,
        string Message,
        IEnumerable<string>? Errors,
        string? DetailedMessage
    );

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = exception switch
        {
            ValidationException validationEx => new ErrorResponse(
                (int)HttpStatusCode.BadRequest,
                "Validation failed",
                validationEx.Errors.Select(e => e.ErrorMessage),
                null),

            NotFoundException notFoundEx => new ErrorResponse(
                (int)HttpStatusCode.NotFound,
                "Resource not found",
                null,
                notFoundEx.Message),

            ArgumentException argEx => new ErrorResponse(
                (int)HttpStatusCode.BadRequest,
                "Invalid argument",
                new[] { argEx.Message },
                argEx.ParamName),

            _ => new ErrorResponse(
                (int)HttpStatusCode.InternalServerError,
                "An error occurred while processing your request.",
                null,
                exception.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.Status;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}