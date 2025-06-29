using BillingSystemOperational.CustomerService.Application.Dto;
using Shared.Helper.GenericResultModel;

namespace BillingSystemOperational.CustomerService.Application.Service.Interface
{
    public interface ICustomerService
    {
        public Task<IDataResult<CustomerListResponseDto>> GetCustomers();
        public Task<IDataResult<CustomerDetailDto>> GetCustomer(int id);
    }
}
