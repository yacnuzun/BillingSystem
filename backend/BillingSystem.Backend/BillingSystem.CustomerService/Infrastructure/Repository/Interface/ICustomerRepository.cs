using BillingSystem.CustomerService.Domain;
using BillingSystem.Shared.Persistance.Interface;

namespace BillingSystem.CustomerService.Infrastructure.Repository.Interface
{
    public interface ICustomerRepository : IRepository<Customer> { }
}
