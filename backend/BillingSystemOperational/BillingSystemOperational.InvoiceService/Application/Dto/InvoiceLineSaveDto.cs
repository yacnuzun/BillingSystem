namespace BillingSystemOperational.InvoiceService.Application.Dto
{
    public class InvoiceLineSaveDto
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
