using BillingSystemOperational.InvoiceService.Application.Dto;
using BillingSystemOperational.InvoiceService.Application.Interface;
using BillingSystemOperational.InvoiceService.Domain;
using BillingSystemOperational.InvoiceService.Infrastructure.Repository.Interface;
using Shared.Helper.GenericResultModel;
using Shared.Persistance.Interface;

namespace BillingSystemOperational.InvoiceService.Application.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceLineRepository _invoiceLineRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IInvoiceLineRepository invoiceLineRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _invoiceLineRepository = invoiceLineRepository;
        }

        public async Task<IDataResult<InvoiceSaveResponseDto>> Add(InvoiceSaveDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var entity = new Invoice
                {
                    CustomerId = dto.CustomerId,
                    InvoiceNumber = dto.InvoiceNumber,
                    InvoiceDate = dto.InvoiceDate.ToUniversalTime(),
                    TotalAmount = dto.TotalAmount,
                    UserId = dto.UserId,
                    RecordDate = DateTime.UtcNow,
                    IsDeleted = false,
                    InvoiceLines = dto.InvoiceLines.Select(line => new InvoiceLine
                    {
                        ItemName = line.ItemName,
                        Quantity = line.Quantity,
                        Price = line.Price,
                        UserId = dto.UserId,
                        RecordDate = DateTime.UtcNow,
                        IsDeleted = false
                    }).ToList()
                };
                await _invoiceRepository.AddAsync(entity);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new SuccessDataResult<InvoiceSaveResponseDto>(new InvoiceSaveResponseDto
                {
                    InvoiceId = entity.InvoiceId,
                    Message = "Fatura başarıyla kaydedildi."
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<InvoiceSaveResponseDto>(ex.ToString());
                throw new Exception($"Fatura kaydı sırasında hata: {ex.Message}");
            }

        }

        public async Task<IDataResult<InvoiceDeleteResponseDto>> Delete(InvoiceDeleteRequestDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var invoice = await _invoiceRepository.GetAsync(i => i.InvoiceId == dto.InvoiceId && !i.IsDeleted);
                if (invoice == null)
                    return new ErrorDataResult<InvoiceDeleteResponseDto>(new InvoiceDeleteResponseDto { Success = false, Message = "Fatura bulunamadı." });

                var lines = await _invoiceLineRepository.ListAsync(l => l.InvoiceId == dto.InvoiceId && !l.IsDeleted);
                foreach (var line in lines)
                    line.IsDeleted = true;

                invoice.IsDeleted = true;

                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new SuccessDataResult<InvoiceDeleteResponseDto>(new InvoiceDeleteResponseDto { Success = true, Message = "Fatura başarıyla silindi (soft delete)." });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<InvoiceDeleteResponseDto>(new InvoiceDeleteResponseDto { Success = false, Message = $"Hata: {ex.Message}" });

                throw;
            }

        }

        public async Task<IDataResult<InvoiceUpdateDto>> GetInvoice(int id)
        {
            var result = await _invoiceRepository.GetAsync(i => i.IsDeleted != true && i.InvoiceId == id);
            var lineResult = await _invoiceLineRepository.ListAsync(il => il.IsDeleted != true && il.InvoiceId == id);
            var dto = new InvoiceUpdateDto
            {
                CustomerId = result.CustomerId,
                InvoiceDate = result.InvoiceDate,
                InvoiceId = result.InvoiceId,
                TotalAmount = result.TotalAmount,
                InvoiceNumber = result.InvoiceNumber,
                UserId = result.UserId,
                InvoiceLines = lineResult.Select(il => new InvoiceLineUpdateDto
                {
                    InvoiceLineId = il.InvoiceLineId,
                    ItemName = il.ItemName,
                    Price = il.Price,
                    Quantity = il.Quantity
                }).ToList()
            };
            if (result is not null)
            {
                return new SuccessDataResult<InvoiceUpdateDto>(dto);
            }
            return new ErrorDataResult<InvoiceUpdateDto>();
        }

        public async Task<IDataResult<InvoiceListResponseDto>> GetInvoiceList(InvoiceListItemRequestDto dto)
        {
            var entity = await _invoiceRepository.ListAsync(i =>
                !i.IsDeleted &&
                i.InvoiceDate >= dto.StartDate.ToUniversalTime() &&
                i.InvoiceDate <= dto.EndDate.ToUniversalTime());

            var invoiceList = entity
                        .Select(i => new InvoiceListItemDto
                        {
                            InvoiceId = i.InvoiceId,
                            InvoiceNumber = i.InvoiceNumber,
                            InvoiceDate = i.InvoiceDate,
                            TotalAmount = i.TotalAmount,
                            //CustomerTitle = i.Customer != null ? i.Customer.Title : "" // Customer navigation varsa
                        }).ToList();

            return new SuccessDataResult<InvoiceListResponseDto>(new InvoiceListResponseDto
            {
                Invoices = invoiceList
            });

            throw new NotImplementedException();
        }

        public async Task<IDataResult<InvoiceListResponseDto>> GetInvoices()
        {
            try
            {
                var listInvoice = await _invoiceRepository.ListAsync(i => i.IsDeleted == false);
                var listDto = listInvoice.Select(x => new InvoiceListItemDto
                {
                    InvoiceDate = x.InvoiceDate,
                    CustomerTitle = "",
                    InvoiceId = x.InvoiceId,
                    InvoiceNumber = x.InvoiceNumber,
                    TotalAmount = x.TotalAmount
                }).ToList();

                return new SuccessDataResult<InvoiceListResponseDto>(new InvoiceListResponseDto { Invoices = listDto });

            }
            catch (Exception)
            {
                return new ErrorDataResult<InvoiceListResponseDto>();
                throw;
            }
        }

        public async Task<IDataResult<InvoiceUpdateResponseDto>> Update(InvoiceUpdateDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var existingInvoice = await _invoiceRepository.GetAsync(i => i.InvoiceId == dto.InvoiceId && !i.IsDeleted);
                if (existingInvoice == null)
                    return new ErrorDataResult<InvoiceUpdateResponseDto>(new InvoiceUpdateResponseDto { Success = false, Message = "Fatura bulunamadı." });

                existingInvoice.InvoiceNumber = dto.InvoiceNumber;
                existingInvoice.InvoiceDate = dto.InvoiceDate.ToUniversalTime();
                existingInvoice.TotalAmount = dto.TotalAmount;
                existingInvoice.CustomerId = dto.CustomerId;

                var existingLines = (await _invoiceLineRepository.ListAsync(l => l.InvoiceId == dto.InvoiceId && !l.IsDeleted)).ToList();

                var updatedLineIds = dto.InvoiceLines.Where(l => l.InvoiceLineId > 0).Select(l => l.InvoiceLineId).ToList();
                var linesToDelete = existingLines.Where(e => !updatedLineIds.Contains(e.InvoiceLineId)).ToList();


                foreach (var deleteLine in linesToDelete)
                    _invoiceLineRepository.SoftDelete(deleteLine);


                foreach (var lineDto in dto.InvoiceLines)
                {
                    if (lineDto.InvoiceLineId == 0)
                    {
                        // Yeni satır
                        var newLine = new InvoiceLine
                        {
                            InvoiceId = dto.InvoiceId,
                            ItemName = lineDto.ItemName,
                            Quantity = lineDto.Quantity,
                            Price = lineDto.Price,
                            UserId = dto.UserId,
                            RecordDate = DateTime.UtcNow.ToUniversalTime(),
                            IsDeleted = false,
                        };
                        await _invoiceLineRepository.AddAsync(newLine);
                    }
                    else
                    {
                        // Güncelleme
                        var existingLine = existingLines.FirstOrDefault(l => l.InvoiceLineId == lineDto.InvoiceLineId);
                        if (existingLine != null)
                        {
                            existingLine.ItemName = lineDto.ItemName;
                            existingLine.Quantity = lineDto.Quantity;
                            existingLine.Price = lineDto.Price;
                        }
                    }
                }

                _invoiceRepository.Update(existingInvoice);

                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new SuccessDataResult<InvoiceUpdateResponseDto>(new InvoiceUpdateResponseDto { Success = true, Message = "Fatura başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ErrorDataResult<InvoiceUpdateResponseDto>(new InvoiceUpdateResponseDto { Success = false, Message = $"Hata: {ex.Message}" });
                throw;
            }

        }
    }
}
