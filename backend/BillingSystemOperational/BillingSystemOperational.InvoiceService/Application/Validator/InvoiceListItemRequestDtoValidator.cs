using BillingSystemOperational.InvoiceService.Application.Dto;
using FluentValidation;

namespace BillingSystemOperational.InvoiceService.Application.Validator
{
    public class InvoiceListItemRequestDtoValidator : AbstractValidator<InvoiceListItemRequestDto>
    {
        public InvoiceListItemRequestDtoValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => Convert.ToDateTime(x.EndDate))
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => Convert.ToDateTime(x.StartDate))
                .WithMessage("End date must be after or equal to start date.");
        }
    }

}

