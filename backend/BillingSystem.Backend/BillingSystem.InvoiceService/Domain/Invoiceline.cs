using BillingSystem.Shared.Persistance.Entity;

namespace BillingSystem.InvoiceService.Domain
{
    public class InvoiceLine : TEntity, IEntity
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;
    }
}
