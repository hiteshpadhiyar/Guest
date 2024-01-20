namespace Guest.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string apiKeyName = "ApiAccessKey";

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(apiKeyName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key not given with Middleware ");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var key = appSettings.GetValue<string>(apiKeyName);
            if (!key.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key is not valid /Unauthorized with ApiKeyAuthMiddleware");
                return;
            }
            await _next(context);
        }
    }
}
