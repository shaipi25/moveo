using System.Text.Json;

namespace moveo.Excpetions
{
    public class ExcepctionMiddelware
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
                await WriteResponse(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception occured while executing {context}");
                await WriteResponse(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        private static async Task WriteResponse(HttpContext context, int httpStatusCode, string message)
        {
            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = message
            }));
        }
    }
}
