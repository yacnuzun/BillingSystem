using BillingSystem.Shared.Helper.GenericResultModel;
using BillingSystem.AccountService.Domain;

namespace BillingSystem.AccountService.Applicaiton.Service.Interface
{
    public interface IUserService
    {
        //void Add(User user, UserRoles userRoles);
        Task<IDataResult<List<User>>> GetAll();
        Task<IDataResult<User>> GetById(int id);
        Task<IDataResult<User>> GetExistUser(string userName);
        Task<IDataResult<User>> AddAsync(User user);
    }
}
