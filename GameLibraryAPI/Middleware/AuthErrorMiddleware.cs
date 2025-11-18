using System.Net;
using System.Text.Json;

namespace GameLibraryAPI.Middleware
{
    public class AuthErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 401,
                    error = "Unauthorized",
                    message = "Необходимо войти в систему"
                }));
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 403,
                    error = "Forbidden",
                    message = "У вас нет прав для выполнения этого действия"
                }));
            }
        }
    }

    public static class AuthErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthErrorMiddleware>();
        }
    }
}
