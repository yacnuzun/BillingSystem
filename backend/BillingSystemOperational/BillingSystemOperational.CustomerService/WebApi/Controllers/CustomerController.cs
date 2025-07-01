using BillingSystemOperational.CustomerService.Application.Dto;
using BillingSystemOperational.CustomerService.Application.Service.Interface;
using FluentValidation;
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
        private readonly IValidator<CustomerAddDto> validator;

        public CustomerController(ICustomerService customerService, IValidator<CustomerAddDto> validator)
        {
            _customerService = customerService;
            this.validator = validator;
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

        [HttpPost("addcustomer")]
        public async Task<IActionResult> AddCustomer(CustomerAddDto customer)
        {
            var isValid = await validator.ValidateAsync(customer);
            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }
            var result = await _customerService.AddAsync(customer);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
