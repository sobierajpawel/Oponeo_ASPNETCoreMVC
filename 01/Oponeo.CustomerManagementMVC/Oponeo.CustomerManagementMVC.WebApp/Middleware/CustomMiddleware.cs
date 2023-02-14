namespace Oponeo.CustomerManagementMVC.WebApp.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;   
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Method.ToLowerInvariant() == "post")
            {
                string something = "";
            }

            await _next(context);
        }
    }
}
