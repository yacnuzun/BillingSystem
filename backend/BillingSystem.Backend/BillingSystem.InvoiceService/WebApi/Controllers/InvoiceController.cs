using BillingSystem.InvoiceService.Application.Dto;
using BillingSystem.InvoiceService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.InvoiceService.WebApi.Controllers
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
        [HttpGet("gettest")]
        public async Task<IActionResult> GetTest()
        {
            return Ok();
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
            return Ok("");
        }
        [HttpPut]
        public async Task<IActionResult> InvoiceUpdate(InvoiceUpdateDto request)
        {
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> InvoiceDelete(InvoiceDeleteRequestDto request)
        {
            return Ok();
        }
    }
}
