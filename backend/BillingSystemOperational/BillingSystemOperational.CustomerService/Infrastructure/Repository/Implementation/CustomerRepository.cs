using BillingSystemOperational.CustomerService.Domain;
using BillingSystemOperational.CustomerService.Infrastructure.Data;
using BillingSystemOperational.CustomerService.Infrastructure.Repository.Interface;
using Shared.Persistance.Implementation;

namespace BillingSystemOperational.CustomerService.Infrastructure.Repository.Implementation
{
    public class CustomerRepository : EfRepository<Customer, CustomerDbContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
