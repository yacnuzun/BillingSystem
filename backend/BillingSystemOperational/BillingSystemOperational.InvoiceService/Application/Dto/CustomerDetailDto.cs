namespace BillingSystemOperational.InvoiceService.Application.Dto
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }
}
