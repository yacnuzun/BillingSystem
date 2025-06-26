namespace BillingSystem.InvoiceService.Application.Dto
{
    public class InvoiceListItemDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerTitle { get; set; }
    }
}
