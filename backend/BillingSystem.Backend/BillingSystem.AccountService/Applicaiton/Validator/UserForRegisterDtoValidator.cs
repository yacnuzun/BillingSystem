using FluentValidation;

namespace BillingSystem.AccountService.Applicaiton.Validator
{
    public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kullanıcı adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Customer)
                .NotNull().WithMessage("Müşteri bilgileri boş olamaz.")
                .SetValidator(new CustomerAddDtoValidator()); // iç validator'ı çağır
        }
    }
}
