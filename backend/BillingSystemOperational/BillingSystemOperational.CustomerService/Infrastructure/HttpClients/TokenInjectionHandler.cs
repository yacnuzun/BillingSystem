namespace BillingSystemOperational.CustomerService.Infrastructure.HttpClients
{
    public class TokenInjectionHandler : DelegatingHandler
    {
        private readonly RequestDelegate _next;


        public TokenInjectionHandler(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext request)
        {
            // HttpContext'ten gelen isteğin Authorization başlığını al
            if (request.Request.Headers.TryGetValue("Authorization", out var bearer))
            {
                //context.Request.Headers.Authorization = bearer.ToString();
                var token = bearer;

                request.Items["Authorization"] = token;

            }

            
            if (request.Request.Headers.TryGetValue("userid", out var user))
            {
                request.Items["userId"] = user;
            }


            // İsteği bir sonraki adıma ilet
            await _next(request);
        }
    }

}
