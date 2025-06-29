using BillingSystemOperational.InvoiceService.Domain;
using BillingSystemOperational.InvoiceService.Infrastructure.Data;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Interface;
using Shared.Persistance.Implementation;

namespace BillingSystemOperational.InvoiceService.Infrastructure.Repository.Implementation
{
    public class InvoiceRepository : EfRepository<Invoice, InvoiceDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}
