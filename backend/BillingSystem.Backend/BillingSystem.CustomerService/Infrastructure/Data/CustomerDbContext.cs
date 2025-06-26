using BillingSystem.CustomerService.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BillingSystem.CustomerService.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
