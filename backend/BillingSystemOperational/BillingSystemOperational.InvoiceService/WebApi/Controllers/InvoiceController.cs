using BillingSystemOperational.InvoiceService.Application.Dto;
using BillingSystemOperational.InvoiceService.Application.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BillingSystemOperational.InvoiceService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IValidator<InvoiceUpdateDto> _updateValidator;
        private readonly IValidator<InvoiceSaveDto> _saveValidator;
        private readonly IValidator<InvoiceDeleteRequestDto> _deleteValidator;
        private readonly IValidator<InvoiceLineSaveDto> _invoiceLineValidator;
        private readonly IValidator<InvoiceListItemRequestDto> _invoiceListValidator;
        private readonly IValidator<InvoiceLineUpdateDto> _invoiceLineUpdateValidator;
        public InvoiceController(IInvoiceService invoiceService,
            IValidator<InvoiceUpdateDto> updateValidator,
            IValidator<InvoiceSaveDto> saveValidator,
            IValidator<InvoiceDeleteRequestDto> deleteValidator,
            IValidator<InvoiceLineSaveDto> invoiceLineValidator,
            IValidator<InvoiceListItemRequestDto> invoiceListValidator,
            IValidator<InvoiceLineUpdateDto> invoiceLineUpdateValidator)
        {
            _invoiceService = invoiceService;
            _updateValidator = updateValidator;
            _saveValidator = saveValidator;
            _deleteValidator = deleteValidator;
            _invoiceLineValidator = invoiceLineValidator;
            _invoiceListValidator = invoiceListValidator;
            _invoiceLineUpdateValidator = invoiceLineUpdateValidator;
        }

        [Authorize]
        [HttpGet("authorizeTest")]
        public async Task<IActionResult> GetAuthorize()
        {
            return Ok(HttpContext.Request.Headers.Authorization.ToString());
        }
        [Authorize]
        [HttpGet("getivoices")]
        public async Task<IActionResult> GetListInvoice()
        {
            var result = await _invoiceService.GetInvoices();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetInvoice(int id)
        {
            if (id.ToString().IsNullOrEmpty())
            {
                return BadRequest("id value is null");
            }
            var result = await _invoiceService.GetInvoice(id);
            if (!result.Success)
            {
                return BadRequest(result.Data);
            }
            return Ok(result.Data);
        }
        [Authorize]
        [HttpGet("getlist")]
        public async Task<IActionResult> InvoiceList([FromQuery] InvoiceListItemRequestDto requestDto)
        {
            var validate = await _invoiceListValidator.ValidateAsync(requestDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            var result = await _invoiceService.GetInvoiceList(requestDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InvoiceSave(InvoiceSaveDto request)
        {
            var validate = await _saveValidator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            var result = await _invoiceService.Add(request);
            if (!result.Success) { return BadRequest(result); }
            return Ok(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> InvoiceUpdate(InvoiceUpdateDto request)
        {
            var validate = await _updateValidator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            var result = await _invoiceService.Update(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> InvoiceDelete(InvoiceDeleteRequestDto request)
        {
            var validate = await _deleteValidator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            var result = await _invoiceService.Delete(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
