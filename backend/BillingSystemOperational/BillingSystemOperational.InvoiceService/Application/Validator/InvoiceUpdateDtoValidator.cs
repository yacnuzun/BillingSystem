using BillingSystemOperational.InvoiceService.Application.Dto;
using BillingSystemOperational.InvoiceService.Application.Interface;
using FluentValidation;

namespace BillingSystemOperational.InvoiceService.Application.Validator
{
    public class InvoiceUpdateDtoValidator : AbstractValidator<InvoiceUpdateDto>
    {
        public InvoiceUpdateDtoValidator(IInvoiceService invoiceService)
        {
            RuleFor(x => x.InvoiceId)
                .GreaterThan(0).WithMessage("InvoiceId must be greater than 0.");
                

            RuleFor(x=>x)
                .MustAsync(async (c, cancelation) =>
                {
                    var result = await invoiceService.GetCustomer(c.CustomerId);
                    return result is null || !result.Success || result.Data is null ? false : true;
                });

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");

            RuleFor(x => x.InvoiceNumber)
                .NotEmpty().WithMessage("Invoice number is required.")
                .MaximumLength(50).WithMessage("Invoice number must not exceed 50 characters.");

            RuleFor(x => x.InvoiceDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Invoice date cannot be in the future.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be a positive number.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.InvoiceLines)
                .NotNull().WithMessage("Invoice lines are required.")
                .NotEmpty().WithMessage("Invoice must have at least one line.")
                .ForEach(line =>
                {
                    // Burada tanımını yaptığını varsaydığımız bir Validator
                    line.SetValidator(new InvoiceLineUpdateDtoValidator()); // veya InvoiceLineUpdateDtoValidator
                });
        }
    }

}

