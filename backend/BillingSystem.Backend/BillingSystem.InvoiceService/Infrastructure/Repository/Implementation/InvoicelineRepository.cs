using BillingSystem.InvoiceService.Domain;
using BillingSystem.InvoiceService.Infrastructure.Data;
using BillingSystem.InvoiceService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Persistance.Implementation;

namespace BillingSystem.InvoiceService.Infrastructure.Repository.Implementation
{
    public class InvoiceLineRepository : EfRepository<InvoiceLine, InvoiceDbContext>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}
