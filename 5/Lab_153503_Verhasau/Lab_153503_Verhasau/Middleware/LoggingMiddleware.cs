using Serilog.Core;

namespace Lab_153503_Verhasau.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger _logger;

        public LoggingMiddleware(RequestDelegate next, Logger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var statusCode = context.Response.StatusCode;
            if (statusCode / 100 != 2)
            {
                string logMessage = $" ---> request {context.Request.Path} {context.Response.StatusCode}";
                _logger.Information(logMessage);
            }
        }
    }
}
