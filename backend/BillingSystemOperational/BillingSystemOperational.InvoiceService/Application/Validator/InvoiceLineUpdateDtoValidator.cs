using BillingSystemOperational.InvoiceService.Application.Dto;
using FluentValidation;

namespace BillingSystemOperational.InvoiceService.Application.Validator
{
    public class InvoiceLineUpdateDtoValidator : AbstractValidator<InvoiceLineUpdateDto>
    {
        public InvoiceLineUpdateDtoValidator()
        {
            RuleFor(x => x.ItemName)
                .NotEmpty().WithMessage("Item name is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive number.");
        }
    }


}
