using BillingSystem.InvoiceService.Application.Dto;
using BillingSystem.Shared.Helper.GenericResultModel;

namespace BillingSystem.InvoiceService.Application.Interface
{
    public interface IInvoiceService
    {
        public Task<IDataResult<InvoiceSaveResponseDto>> Add(InvoiceSaveDto dto);
        public Task<IDataResult<InvoiceUpdateResponseDto>> Update(InvoiceUpdateDto dto);
        public Task<IDataResult<InvoiceDeleteResponseDto>> Delete(InvoiceDeleteRequestDto dto);
        public Task<IDataResult<InvoiceListResponseDto>> GetInvoiceList(InvoiceListItemRequestDto dto);
        public Task<IDataResult<InvoiceListItemDto>> GetInvoice(int id);
    }
}
