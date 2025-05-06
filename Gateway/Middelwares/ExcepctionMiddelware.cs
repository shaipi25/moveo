using Excpetions;
using FluentValidation;
using System.Text.Json;

namespace Middelwares
{
    class ExcepctionMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExcepctionMiddelware> _logger;

        public ExcepctionMiddelware(RequestDelegate next, ILogger<ExcepctionMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogWarning(ex, $"Error occured while executing {context}");
                await WriteResponse(context, ex.StatusCode, ex.Message, null);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );


                await WriteResponse(context, StatusCodes.Status400BadRequest, "Validation failed", errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception occured while executing {context}");
                await WriteResponse(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred", null);
            }
        }

        private static async Task WriteResponse(HttpContext context, int httpStatusCode, string message, Dictionary<string, string[]> errors)
        {
            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message,
                errors
            }));
        }
    }
}
