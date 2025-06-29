using BillingSystemOperational.InvoiceService.Application.Dto;
using BillingSystemOperational.InvoiceService.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystemOperational.InvoiceService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Authorize]
        [HttpGet("authorizeTest")]
        public async Task<IActionResult> GetAuthorize()
        {
            return Ok(HttpContext.Request.Headers.Authorization.ToString());
        }
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
        [HttpGet]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var result = await _invoiceService.GetInvoice(id);
            if (!result.Success)
            {
                return BadRequest(result.Data);
            }
            return Ok(result.Data);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> InvoiceList(InvoiceListItemRequestDto requestDto)
        {
            var result = await _invoiceService.GetInvoiceList(requestDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> InvoiceSave(InvoiceSaveDto request)
        {
            var result = await _invoiceService.Add(request);
            if (!result.Success) { return BadRequest(result); }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> InvoiceUpdate(InvoiceUpdateDto request)
        {
            var result = await _invoiceService.Update(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> InvoiceDelete(InvoiceDeleteRequestDto request)
        {
            var result = await _invoiceService.Delete(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
