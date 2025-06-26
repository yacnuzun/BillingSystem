using BillingSystem.CustomerService.Application.Dto;
using BillingSystem.CustomerService.Application.Service.Interface;
using BillingSystem.CustomerService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Helper.GenericResultModel;
using BillingSystem.Shared.Persistance.Interface;
using static BillingSystem.CustomerService.Application.Dto.CustomerListItemDto;

namespace BillingSystem.CustomerService.Application.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IDataResult<CustomerDetailDto>> GetCustomer(int id)
        {
            try
            {
                var entity = await _customerRepository.GetAsync(c => c.CustomerId == id);

                return new SuccessDataResult<CustomerDetailDto>(CustomerDetailDto.GetModel(entity));
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CustomerDetailDto>(ex.Message);
                throw;
            }
        }

        public async Task<IDataResult<CustomerListResponseDto>> GetCustomers()
        {
            try
            {
                var entities = await _customerRepository.ListAsync();

                var listDto = entities.Select(CustomerListItemDto.GetModel).ToList();

                return new SuccessDataResult<CustomerListResponseDto>(new CustomerListResponseDto { Customers = listDto });
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CustomerListResponseDto>(ex.Message);
                throw;
            }
        }
    }
}
