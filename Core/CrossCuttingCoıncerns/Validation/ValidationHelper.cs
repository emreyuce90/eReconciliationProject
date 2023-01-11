using FluentValidation;

namespace Core.CrossCuttingCoıncerns.Validation
{
    public static class ValidationHelper
    {
        public static void ValidateObject(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
