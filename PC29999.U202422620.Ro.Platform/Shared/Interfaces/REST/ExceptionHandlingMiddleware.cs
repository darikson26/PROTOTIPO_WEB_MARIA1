using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Exceptions;

namespace PC29999.U202422620.Ro.Platform.Shared.Interfaces.REST;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Request failed.");
            var (statusCode, messageKey) = exception switch
            {
                PrescriptionConflictException => (StatusCodes.Status409Conflict, "conflict"),
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "invalidRange"),
                ArgumentException => (StatusCodes.Status400BadRequest, "invalidArgument"),
                DbUpdateException { InnerException: MySqlException { Number: 1062 } } => (StatusCodes.Status409Conflict, "conflict"),
                DbUpdateException => (StatusCodes.Status500InternalServerError, "serverError"),
                _ => (StatusCodes.Status500InternalServerError, "serverError")
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = LocalizedMessage(context, messageKey) }));
        }
    }

    private static string LocalizedMessage(HttpContext context, string key)
    {
        var spanish = AcceptLanguageSelector.IsSpanish(context.Request.Headers.AcceptLanguage.ToString());
        return (key, spanish) switch
        {
            ("conflict", true) => "La solicitud entra en conflicto con una receta existente o con el paciente indicado.",
            ("invalidRange", true) => "Uno de los valores está fuera del rango permitido.",
            ("invalidArgument", true) => "Uno o más valores de la solicitud no son válidos.",
            ("serverError", true) => "Se produjo un error interno del servidor.",
            ("conflict", false) => "The request conflicts with an existing prescription or the specified patient.",
            ("invalidRange", false) => "One of the values is outside the allowed range.",
            ("invalidArgument", false) => "One or more request values are invalid.",
            _ => "An internal server error occurred."
        };
    }
}
