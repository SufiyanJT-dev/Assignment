using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace HotelBookingSystem.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;
            var message = "An unexpected error occurred.";
            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case DbUpdateConcurrencyException:
                    message = "A concurrency conflict occurred while updating the data. Please try again.";
                    statusCode = HttpStatusCode.Conflict;
                    break;

                case DbUpdateException:
                    message = "A database update error occurred. Check your input data or foreign key relationships.";
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case ArgumentNullException:
                    message = "One or more required fields were missing.";
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case KeyNotFoundException:
                    message = "The requested resource could not be found.";
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case InvalidOperationException:
                    message = "Invalid operation attempted.";
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case TaskCanceledException:
                    message = "The operation was canceled.";
                    statusCode = HttpStatusCode.RequestTimeout;
                    break;
            }

            logger.LogError(exception, "Exception caught by middleware: {Message}", message);

            var errorResponse = new
            {
                error = message,
                details = exception.Message
            };

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
