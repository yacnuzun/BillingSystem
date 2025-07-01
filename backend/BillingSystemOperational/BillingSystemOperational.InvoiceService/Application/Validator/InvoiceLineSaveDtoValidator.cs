using BillingSystemOperational.InvoiceService.Application.Dto;
using FluentValidation;

namespace BillingSystemOperational.InvoiceService.Application.Validator
{
    public class InvoiceLineSaveDtoValidator : AbstractValidator<InvoiceLineSaveDto>
    {
        public InvoiceLineSaveDtoValidator()
        {
            RuleFor(x => x.ItemName)
                .NotEmpty().WithMessage("Item name is required.")
                .MaximumLength(100).WithMessage("Item name must not exceed 100 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive number.");
        }
    }


}
