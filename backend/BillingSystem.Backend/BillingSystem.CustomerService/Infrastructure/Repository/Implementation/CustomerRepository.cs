using BillingSystem.CustomerService.Domain;
using BillingSystem.CustomerService.Infrastructure.Data;
using BillingSystem.CustomerService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Persistance.Implementation;

namespace BillingSystem.CustomerService.Infrastructure.Repository.Implementation
{
    public class CustomerRepository : EfRepository<Customer, CustomerDbContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
