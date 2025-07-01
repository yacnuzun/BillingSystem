using BillingSystem.AccountService.Infrastructure.Helpers.JWT;

namespace BillingSystem.AccountService.Applicaiton
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public AccessToken Token { get; set; }
    }
}