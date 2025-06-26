using BillingSystem.InvoiceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.InvoiceService.Infrastructure.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> Invoicelines { get; set; }
    }
}
