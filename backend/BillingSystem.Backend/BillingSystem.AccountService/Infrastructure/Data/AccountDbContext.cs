using BillingSystem.AccountService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.AccountService.Infrastructure.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        
    }
}
