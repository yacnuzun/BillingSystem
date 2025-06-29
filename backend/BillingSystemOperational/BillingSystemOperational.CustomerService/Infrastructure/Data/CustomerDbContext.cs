using BillingSystemOperational.CustomerService.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BillingSystemOperational.CustomerService.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
