using BillingSystem.AccountService.Domain;
using BillingSystem.AccountService.Infrastructure.Data;
using BillingSystem.AccountService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Persistance.Implementation;

namespace BillingSystem.AccountService.Infrastructure.Repository.Implementation
{
    public class UserRepository : EfRepository<User, AccountDbContext>, IUserRepository
    {
        public UserRepository(AccountDbContext context) : base(context)
        {
        }

    }
}
