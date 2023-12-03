using FluentValidation;
using UdemyMicroservices.Web.Models.Discount;

namespace UdemyMicroservices.Web.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim kodu boş olamaz...");
        }
    }
}
