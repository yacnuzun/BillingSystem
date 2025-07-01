using BillingSystem.Shared.Abstract;

namespace BillingSystem.AccountService.Applicaiton
{
    public class UserForRegisterDto : IDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}