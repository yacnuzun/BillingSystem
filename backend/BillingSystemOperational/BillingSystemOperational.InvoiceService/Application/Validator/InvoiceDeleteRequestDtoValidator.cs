using BillingSystemOperational.InvoiceService.Application.Dto;
using BillingSystemOperational.InvoiceService.Application.Interface;
using FluentValidation;

namespace BillingSystemOperational.InvoiceService.Application.Validator
{
    public class InvoiceDeleteRequestDtoValidator : AbstractValidator<InvoiceDeleteRequestDto>
    {
        public InvoiceDeleteRequestDtoValidator(IInvoiceService invoiceService)
        {
            RuleFor(x => x.InvoiceId)
                .NotEmpty()
                .NotNull()
                .MustAsync(async (i, cancelation) => {
                    var result = await invoiceService.GetInvoice(i);
                    return result is null||!result.Success || result.Data is null?false:true;
                })
                .GreaterThanOrEqualTo(1).WithMessage("InvoiceId must be greater than 0.");

        }

    }

}
