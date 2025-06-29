using BillingSystemOperational.InvoiceService.Application.Dto;
using Shared.Helper.GenericResultModel;

namespace BillingSystemOperational.InvoiceService.Application.Interface
{
    public interface IInvoiceService
    {
        public Task<IDataResult<InvoiceSaveResponseDto>> Add(InvoiceSaveDto dto);
        public Task<IDataResult<InvoiceUpdateResponseDto>> Update(InvoiceUpdateDto dto);
        public Task<IDataResult<InvoiceDeleteResponseDto>> Delete(InvoiceDeleteRequestDto dto);
        public Task<IDataResult<InvoiceListResponseDto>> GetInvoiceList(InvoiceListItemRequestDto dto);
        public Task<IDataResult<InvoiceUpdateDto>> GetInvoice(int id);
        public Task<IDataResult<InvoiceListResponseDto>> GetInvoices();
    }
}
