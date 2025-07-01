using FluentValidation;

namespace BillingSystem.AccountService.Applicaiton.Validator
{
    public class CustomerAddDtoValidator : AbstractValidator<CustomerAddDto>
    {
        public CustomerAddDtoValidator()
        {
            RuleFor(x => x.TaxNumber)
                .NotEmpty().WithMessage("Vergi numarası boş olamaz.")
                .Length(10).WithMessage("Vergi numarası 10 haneli olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Unvan boş olamaz.")
                .MaximumLength(100);

            RuleFor(x => x.EMail)
                .EmailAddress().WithMessage("Geçerli bir e-posta girin.")
                .NotEmpty();
        }
    }
}
