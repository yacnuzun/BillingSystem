using BillingSystem.InvoiceService.Domain;
using BillingSystem.InvoiceService.Infrastructure.Data;
using BillingSystem.InvoiceService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Persistance.Implementation;
using System.Linq.Expressions;

namespace BillingSystem.InvoiceService.Infrastructure.Repository.Implementation
{
    public class InvoiceRepository : EfRepository<Invoice, InvoiceDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(InvoiceDbContext context) : base(context)
        {
        }
    }
}
