using IResult = BillingSystem.Shared.Helper.GenericResultModel.IResult;
using BillingSystem.Shared.Helper.GenericResultModel;
using BillingSystem.AccountService.Domain;
using BillingSystem.AccountService.Infrastructure.Helpers.JWT;

namespace BillingSystem.AccountService.Applicaiton.Service.Interface
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
    }
}
