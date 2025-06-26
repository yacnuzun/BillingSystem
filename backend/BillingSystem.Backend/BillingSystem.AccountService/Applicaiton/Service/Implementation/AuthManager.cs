using BillingSystem.AccountService.Applicaiton.Service.Interface;
using BillingSystem.AccountService.Domain;
using BillingSystem.AccountService.Infrastructure.Helpers.JWT;
using BillingSystem.Shared.Constant;
using BillingSystem.Shared.Helper.GenericResultModel;
using BillingSystem.Shared.Helper.Security.Hashing;
using IResult = BillingSystem.Shared.Helper.GenericResultModel.IResult;

namespace BillingSystem.AccountService.Applicaiton.Service.Implementation
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<AuthManager> _log;

        public AuthManager(IUserService userService,
            ITokenHelper tokenHelper,
            ILogger<AuthManager> log)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _log = log;
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

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

    }
}
