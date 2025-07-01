using BillingSystem.AccountService.Applicaiton.Service.Interface;
using BillingSystem.AccountService.Domain;
using BillingSystem.AccountService.Infrastructure.Repository.Interface;
using BillingSystem.Shared.Helper.GenericResultModel;
using BillingSystem.Shared.Persistance.Interface;

namespace BillingSystem.AccountService.Applicaiton.Service.Implementation
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            ILogger<UserManager> logger
            )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IDataResult<User>> AddAsync(User user)
        {
            try
            {
                var entity = await _userRepository.AddAsyncT(user);
                return new SuccessDataResult<User>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");

                throw;
            }
        }

        public async void Add(User user)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _userRepository.AddAsync(user);

                await _unitOfWork.CommitAsync();

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }

        }
        public async Task<IDataResult<List<User>>> GetAll()
        {
            try
            {
                var result = await _userRepository.ListAsync();
                if (result == null)
                {
                    return new ErrorDataResult<List<User>>();
                }
                return new SuccessDataResult<List<User>>(result.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }

        }

        public async Task<IDataResult<User>> GetById(int id)
        {
            try
            {
                var result = await _userRepository.GetAsync(u => u.UserId == id);
                if (result == null)
                {
                    return new ErrorDataResult<User>();
                }
                return new SuccessDataResult<User>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.InnerException}/{ex.Message}/{ex.Source}");
                throw;
            }
        }

        public async Task<IDataResult<User>> GetExistUser(string userName)
        {
            try
            {
                var entity = await _userRepository.GetAsync(u => u.UserName == userName);
                if (entity == null)
                {
                    return new ErrorDataResult<User>();
                }
                return new SuccessDataResult<User>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
