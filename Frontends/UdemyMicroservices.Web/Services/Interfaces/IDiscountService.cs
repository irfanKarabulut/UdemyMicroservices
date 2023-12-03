using UdemyMicroservices.Web.Models.Discount;

namespace UdemyMicroservices.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
