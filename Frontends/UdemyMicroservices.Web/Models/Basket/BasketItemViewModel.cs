namespace UdemyMicroservices.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        private decimal? DiscountAppliedPrice { get; set; }
        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice= discountPrice;
        }
        public decimal GetCurrentPrice
        {
            get => DiscountAppliedPrice !=null ? DiscountAppliedPrice.Value : Price;
        }
    }
}
