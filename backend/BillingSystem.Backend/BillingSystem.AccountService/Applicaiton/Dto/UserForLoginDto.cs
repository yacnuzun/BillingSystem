using BillingSystem.Shared.Abstract;

namespace BillingSystem.AccountService.Applicaiton
{
    public class UserForLoginDto : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
