using BillingSystem.CustomerService.Application.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.CustomerService.WebApi.Controllers
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

        [HttpGet("getcustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetCustomers();
            return Ok(result);

        }
    }
}
