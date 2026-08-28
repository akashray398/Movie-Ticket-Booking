using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Exceptions;

namespace MovieTicketBooking.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                InvalidLanguageException or InvalidDurationException or ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                InvalidOperationException => (int)HttpStatusCode.Conflict,
                DbUpdateException => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = exception.Message, statusCode = context.Response.StatusCode }));
        }
    }
}
