using Microsoft.AspNetCore.Identity;

namespace BillingSystem.AccountService.Infrastructure.Helpers.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
