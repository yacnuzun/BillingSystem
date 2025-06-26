using BillingSystem.AccountService.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace BillingSystem.AccountService.Infrastructure.Helpers.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
        JwtSecurityToken ValidateTokenGetClaims(string jwtToken);

    }
}
