using BillingSystem.AccountService.Applicaiton;
using BillingSystem.AccountService.Applicaiton.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
