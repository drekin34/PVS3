namespace LAB3
{
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Отримання даних запиту
            var requestUrl = context.Request.Path + context.Request.QueryString;
            var requestTime = DateTime.UtcNow;
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            // Формування рядка для логування
            var logEntry = $"{requestTime:yyyy-MM-dd HH:mm:ss} | {requestUrl} | IP: {ipAddress}";

            // Запис логів у файл
            await File.AppendAllTextAsync("requests.log", logEntry + Environment.NewLine);

            // Продовжуємо обробку запиту
            await _next(context);
        }
    }
}
