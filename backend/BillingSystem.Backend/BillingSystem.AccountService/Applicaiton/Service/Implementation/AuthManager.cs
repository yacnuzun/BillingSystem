using BillingSystem.AccountService.Applicaiton.Service.Interface;
using BillingSystem.AccountService.Domain;
using BillingSystem.AccountService.Infrastructure.Helpers.JWT;
using BillingSystem.Shared.Constant;
using BillingSystem.Shared.Helper.GenericResultModel;
using BillingSystem.Shared.Helper.Security.Hashing;
using BillingSystem.Shared.Persistance.Interface;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using IResult = BillingSystem.Shared.Helper.GenericResultModel.IResult;
using Newtonsoft.Json;

namespace BillingSystem.AccountService.Applicaiton.Service.Implementation
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthManager> _log;
        private readonly HttpClient httpClient = new HttpClient();

        public AuthManager(IUserService userService,
            ITokenHelper tokenHelper,
            ILogger<AuthManager> log,
            IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _log = log;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                var userToCheck = await _userService.GetExistUser(userForLoginDto.UserName);
                if (userToCheck == null)
                {
                    return new ErrorDataResult<User>(Messages.UserNotFound);
                }

                if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.Password))
                {
                    return new ErrorDataResult<User>(Messages.PasswordError);
                }

                return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw;
            }

        }
        public async Task<IResult> AddCustomer(CustomerAddDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var request = await httpClient.PostAsync("https://localhost:44391/api/Customer/addcustomer", content);

            if (!request.IsSuccessStatusCode)
            {
                return new ErrorResult(Messages.FailedProccess);
            }

            string response = await request.Content.ReadAsStringAsync();

            var success = JsonConvert.DeserializeObject<CustomerAddResponse>(response);

            if (success is null || !success.Success)
            {
                return new ErrorDataResult<CustomerAddResponse>(Messages.FailedProccess);
            }

            return new SuccessDataResult<CustomerAddResponse>(success, Messages.SuccessProccess);
        }

        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var passwordHash = HashingHelper.CreatePasswordHash(userForRegisterDto.Password);
                var user = new User
                {
                    UserName = userForRegisterDto.UserName,
                    IsDeleted = false,
                    Password = passwordHash,
                    RecordDate = DateTime.UtcNow.ToUniversalTime(),
                };
                var entity = await _userService.AddAsync(user);
                await _unitOfWork.CommitAsync();
                userForRegisterDto.Customer.UserId = entity.Data.UserId;
                if (userForRegisterDto.Customer.UserId == null && userForRegisterDto.Customer == null && userForRegisterDto.Customer.UserId <= 0)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ErrorDataResult<User>(Messages.FailedProccess);
                }
                var result = await AddCustomer(userForRegisterDto.Customer);
                if (!result.Success)
                {

                    await _unitOfWork.RollbackTransactionAsync();
                    return new ErrorDataResult<User>(Messages.FailedProccess);
                }
                await _unitOfWork.CommitTransactionAsync();

                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<User>(Messages.FailedProccess);
                throw;
            }

        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

    }
}
