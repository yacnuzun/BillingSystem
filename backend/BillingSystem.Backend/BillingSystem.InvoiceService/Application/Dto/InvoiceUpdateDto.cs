namespace BillingSystem.InvoiceService.Application.Dto
{
    public class InvoiceUpdateDto
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }

        public List<InvoiceLineUpdateDto> InvoiceLines { get; set; }
    }
}
