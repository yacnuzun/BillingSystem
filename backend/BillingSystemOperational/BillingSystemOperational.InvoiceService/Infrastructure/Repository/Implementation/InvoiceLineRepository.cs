using BillingSystemOperational.InvoiceService.Domain;
using BillingSystemOperational.InvoiceService.Infrastructure.Data;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Interface;
using Shared.Persistance.Implementation;

namespace BillingSystemOperational.InvoiceService.Infrastructure.Repository.Implementation
{
    public class InvoiceLineRepository : EfRepository<InvoiceLine, InvoiceDbContext>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}
