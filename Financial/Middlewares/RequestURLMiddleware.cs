namespace Financial.Middlewares
{
    /// <summary>
    /// This is used to return the endpoint of the request, that will be usefful when we put load balancer
    /// </summary>
    public class RequestURLMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestURLMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Captura o URL completo da requisição
            var requestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";

            // Armazena o URL no contexto
            context.Items["RequestUrl"] = requestUrl;

            // Chama o próximo middleware
            await _next(context);
        }
    }

}
