using BillingSystem.AccountService.Applicaiton;
using BillingSystem.AccountService.Applicaiton.Service.Implementation;
using BillingSystem.AccountService.Applicaiton.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authManager;

        public AccountController(IAuthService authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto dto)
        {
            var userToLogin = await _authManager.Login(dto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = await _authManager.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(result.Data);
        }
    }
}
