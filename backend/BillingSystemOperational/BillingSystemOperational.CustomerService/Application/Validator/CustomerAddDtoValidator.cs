using BillingSystemOperational.CustomerService.Application.Dto;
using FluentValidation;

namespace BillingSystemOperational.CustomerService.Application.Validator
{
    

    public class CustomerAddDtoValidator : AbstractValidator<CustomerAddDto>
    {
        public CustomerAddDtoValidator()
        {
            RuleFor(x => x.TaxNumber)
                .NotEmpty().WithMessage("Vergi numarası boş olamaz.")
                .NotNull().WithMessage("Vergi numarası boş olamaz.")
                .Length(10).WithMessage("Vergi numarası 10 haneli olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Unvan boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.EMail)
                .EmailAddress().WithMessage("Geçerli bir e-posta girin.")
                .NotEmpty();

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçersiz kullanıcı.");
        }
    }
}
