using BillingSystem.AccountService.Domain;
using BillingSystem.Shared.Persistance.Interface;

namespace BillingSystem.AccountService.Infrastructure.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
