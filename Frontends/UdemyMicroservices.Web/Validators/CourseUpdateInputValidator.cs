using FluentValidation;
using UdemyMicroservices.Web.Models.Catalog;

namespace UdemyMicroservices.Web.Validators
{
    public class CourseUpdateInputValidator: AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kurs adı boş olamaz...");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz...");
            RuleFor(x => x.Feature.Duration).NotEmpty().InclusiveBetween(5, 100).WithMessage("Kurs süresi en az 5, en fazla 100 saat olabilir...");
            RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(50).WithMessage("Kurs fiyatı 50 TL'den az olamaz...");
        }
    }
}
