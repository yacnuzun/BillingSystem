using BillingSystemOperational.InvoiceService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BillingSystemOperational.InvoiceService.Infrastructure.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> Invoicelines { get; set; }
    }
}
