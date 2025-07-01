using BillingSystemOperational.CustomerService.Domain;

namespace BillingSystemOperational.CustomerService.Application.Dto
{
    public class CustomerAddDto
    {
        public string TaxNumber { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public int UserId { get; set; }
    }
    public class CustomerAddResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
