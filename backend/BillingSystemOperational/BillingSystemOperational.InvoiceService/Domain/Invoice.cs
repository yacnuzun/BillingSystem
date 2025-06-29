using Shared.Persistance.Entity;

namespace BillingSystemOperational.InvoiceService.Domain
{
    public class Invoice : TEntity, IEntity
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        public ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
