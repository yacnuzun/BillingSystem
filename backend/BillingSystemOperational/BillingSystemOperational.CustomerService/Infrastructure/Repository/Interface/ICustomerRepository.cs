using BillingSystemOperational.CustomerService.Domain;
using Shared.Persistance.Interface;

namespace BillingSystemOperational.CustomerService.Infrastructure.Repository.Interface
{
    public interface ICustomerRepository : IRepository<Customer> { }
}
