namespace BillingSystem.InvoiceService.Application.Dto
{
    public class InvoiceSaveDto
    {
        public int CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }

        public List<InvoiceLineSaveDto> InvoiceLines { get; set; }
    }
}
