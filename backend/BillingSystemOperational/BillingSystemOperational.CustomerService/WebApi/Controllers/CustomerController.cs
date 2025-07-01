using BillingSystemOperational.CustomerService.Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystemOperational.CustomerService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAuthorize()
        {
            return Ok(HttpContext.Request.Headers.Authorization.ToString());
        }
        [Authorize]
        [HttpGet("getcustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetCustomers();
            return Ok(result);

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetCustomer([FromBody]int id)
        {
            var result = await _customerService.GetCustomer(id);
            if (!result.Success || result.Data is null)
            {
                return BadRequest(result.Data);
            }
            return Ok(result.Data);
        }
    }
}
