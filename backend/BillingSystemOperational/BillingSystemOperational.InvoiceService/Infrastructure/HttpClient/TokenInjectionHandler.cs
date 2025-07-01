using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared.Helper.Security;
using System.Security.Claims;

namespace BillingSystemOperational.InvoiceService.Infrastructure.HttpClient
{
    public class TokenInjectionHandler
    {
        private readonly RequestDelegate _next;


        public TokenInjectionHandler(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var bearer))
            {
                //context.Request.Headers.Authorization = bearer.ToString();
                var token = bearer;

                context.Items["Authorization"] = token;
                
            }

            await _next(context);
        }
    }
}
