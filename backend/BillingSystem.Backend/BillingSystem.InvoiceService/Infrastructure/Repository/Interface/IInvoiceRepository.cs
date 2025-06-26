using BillingSystem.InvoiceService.Domain;
using BillingSystem.Shared.Persistance.Interface;

namespace BillingSystem.InvoiceService.Infrastructure.Repository.Interface
{
    public interface IInvoiceRepository:IRepository<Invoice>
    {
    }
}
