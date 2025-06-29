using BillingSystemOperational.InvoiceService.Domain;
using Shared.Persistance.Interface;

namespace BillingSystemOperational.InvoiceService.Infrastructure.Repository.Interface
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
    }
}
