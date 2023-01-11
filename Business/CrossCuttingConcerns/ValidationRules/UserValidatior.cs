using Core.Entities.Concrete;
using FluentValidation;

namespace Business.CrossCuttingConcerns.ValidationRules
{
    public class UserValidatior:AbstractValidator<User>
    {
        public UserValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("İsim alanı boş geçilemez")
                .MaximumLength(150).WithMessage("İsim alanı en fazla 150 karakter olabilir");

            RuleFor(x => x.EMail)
                .NotEmpty()
                .WithMessage("EMail alanı boş geçilemez")
                .MaximumLength(150).WithMessage("EMail alanı en fazla 150 karakter olabilir")
                .EmailAddress()
                .WithMessage("Lütfen geçerli bir email adresi giriniz.");
        }
    }
}
