namespace A4WebApp.Middleware
{
    public class RedirectToIndex
    {
        private readonly RequestDelegate _next;

        public RedirectToIndex(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404)
            {
                context.Request.Path = "/Index"; 
                context.Response.StatusCode = 200; 
                await _next(context);
            }
        }
    }
}
