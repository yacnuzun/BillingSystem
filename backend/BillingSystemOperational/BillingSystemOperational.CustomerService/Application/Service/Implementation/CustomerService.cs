using BillingSystemOperational.CustomerService.Application.Dto;
using BillingSystemOperational.CustomerService.Application.Service.Interface;
using BillingSystemOperational.CustomerService.Domain;
using BillingSystemOperational.CustomerService.Infrastructure.Repository.Interface;
using Shared.Constant;
using Shared.Helper.GenericResultModel;
using Shared.Persistance.Interface;

namespace BillingSystemOperational.CustomerService.Application.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitofWork;
        public CustomerService(ICustomerRepository customerRepository, IHttpContextAccessor contextAccessor, IUnitOfWork unitofWork)
        {
            _customerRepository = customerRepository;
            _contextAccessor = contextAccessor;
            _unitofWork = unitofWork;
        }

        public async Task<CustomerAddResponse> AddAsync(CustomerAddDto customerAddDto)
        {
            try
            {
                await _unitofWork.BeginTransactionAsync();

                Customer entity = new Customer()
                {
                    CustomerId = 0,
                    Address = customerAddDto.Address,
                    EMail = customerAddDto.EMail,
                    IsDeleted = false,
                    RecordDate = DateTime.UtcNow.ToUniversalTime(),
                    TaxNumber = customerAddDto.TaxNumber,
                    Title = customerAddDto.Title,
                    UserId = customerAddDto.UserId
                };
                await _customerRepository.AddAsync(entity);
                await _unitofWork.CommitAsync();
                await _unitofWork.CommitTransactionAsync();
                return new CustomerAddResponse() { Message =Messages.SuccessProccess, Success = true};
            }
            catch (Exception)
            {
                await _unitofWork.RollbackTransactionAsync();
                return new CustomerAddResponse { Success= false , Message= Messages.FailedProccess};
                throw;
            }
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
                var currentUser = _contextAccessor.HttpContext.Items["userId"].ToString();
                var entities = await _customerRepository.ListAsync(c=> c.IsDeleted != true && c.UserId != Convert.ToUInt32(currentUser) );

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
