namespace BillingSystemOperational.CustomerService.Infrastructure.HttpClients
{
    public class TokenInjectionHandler : DelegatingHandler
    {
        private readonly RequestDelegate _next;


        public TokenInjectionHandler(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext request)
        {
            if (request.Request.Headers.TryGetValue("Authorization", out var bearer))
            {
                var token = bearer;

                request.Items["Authorization"] = token;

            }

            
            if (request.Request.Headers.TryGetValue("userid", out var user))
            {
                request.Items["userId"] = user;
            }


            await _next(request);
        }
    }

}
