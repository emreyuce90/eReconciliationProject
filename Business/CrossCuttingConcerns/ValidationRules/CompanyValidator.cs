using Domain.Concrete;
using FluentValidation;

namespace Business.CrossCuttingConcerns.ValidationRules
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Şirket adı alanı boş geçilemez")
                .MaximumLength(250)
                .WithMessage("Şirket adı alanı en fazla 250 karakter olabilir");
            RuleFor(c => c.Address)
                .NotEmpty()
                .WithMessage("Adres alanı boş geçilemez")
                .MaximumLength(500)
                .WithMessage("Adres alanı maximum 500 karakter olabilir");
            RuleFor(c => c.TaxDepartment)
                .NotEmpty()
                .WithMessage("Vergi dairesi adı boş geçilemez")
                .MaximumLength(100)
                .WithMessage("Vergi dairesi adı en fazla 100 karakter içerebilir");

         
        }
    }
}
