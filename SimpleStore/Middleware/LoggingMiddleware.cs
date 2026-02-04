namespace SimpleStore.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.ToString().StartsWith("/Product"))
                Console.WriteLine($"[My Logging] : {context.Request.Path} access At :{DateTime.Now}");
            await _next(context);
        }
    }
}
