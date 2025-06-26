using BillingSystem.CustomerService.Domain;

namespace BillingSystem.CustomerService.Application.Dto
{
    public class CustomerDetailDto
    {
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public static CustomerDetailDto GetModel(Customer customer)
        {
            return new CustomerDetailDto
            {
                CustomerId = customer.CustomerId,
                Address = customer.Address,
                Email = customer.EMail,
                TaxNumber = customer.TaxNumber,
                Title = customer.Title
            };
        }
    }
}