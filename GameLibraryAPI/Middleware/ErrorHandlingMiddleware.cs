using System.Text.Json;

namespace GameLibraryAPI.Middleware
{
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
            _logger.LogDebug("ErrorHandlingMiddleware: Enter InvokeAsync for {Path}", context.Request.Path);
            Console.WriteLine($"[Middleware] Enter for {context.Request.Method} {context.Request.Path}");

            try
            {
                await _next(context);
                _logger.LogDebug("ErrorHandlingMiddleware: _next returned normally for {Path}", context.Request.Path);
                Console.WriteLine($"[Middleware] _next returned for {context.Request.Path} (StatusCode {context.Response.StatusCode})");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Caught BadRequestException in middleware for {Path}", context.Request.Path);
                Console.WriteLine($"[Middleware] Caught BadRequestException: {ex.Message}");

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 400,
                    message = ex.Message
                }));

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in middleware for {Path}", context.Request.Path);
                Console.WriteLine($"[Middleware] Caught Exception: {ex.GetType().FullName}: {ex.Message}");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 500,
                    message = "Внутренняя ошибка сервера",
                    detail = ex.Message
                }));
            }
        }
    }
}
