using Domain.Concrete;
using FluentValidation;

namespace Business.CrossCuttingConcerns.ValidationRules
{
    public class CurrencyAccountValidator:AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(ca => ca.Name)
                .NotEmpty()
                .WithMessage("İsim alanı boş geçilemez")
                .MaximumLength(150)
                .WithMessage("İsim alanı en fazla 150 karakter olabilir");
            RuleFor(ca => ca.Code)
                .NotEmpty()
                .WithMessage("Kod alanı boş geçilemez")
                .MaximumLength(150)
                .WithMessage("Kod alanı en fazla 150 karakter olabilir");

            RuleFor(ca => ca.Address)
                .NotEmpty()
                .WithMessage("Adres alanı boş geçilemez")
                .MaximumLength(250)
                .WithMessage("Adres alanı en fazla 250 karakter olabilir");
            RuleFor(ca => ca.TaxDepartment)
                .NotEmpty()
                .WithMessage("Vergi Dairesi alanı boş geçilemez")
                .MaximumLength(150)
                .WithMessage("Vergi dairesi alanı en fazla 150 karakter olabilir");
        }
    }
}
