using BillingSystem.CustomerService.Application.Dto;
using BillingSystem.Shared.Helper.GenericResultModel;

namespace BillingSystem.CustomerService.Application.Service.Interface
{
    public interface ICustomerService
    {
        public Task<IDataResult<CustomerListResponseDto>> GetCustomers ();
        public Task<IDataResult<CustomerDetailDto>> GetCustomer(int id);
    }
}
