namespace UdemyMicroservices.Web.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }

        public string UserId { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems { get; set; }
        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(basketItem =>
                    {
                        var discountPrice = basketItem.Price * ((decimal)DiscountRate.Value / 100);
                        basketItem.AppliedDiscount(Math.Round(basketItem.Price - discountPrice, 2));
                    });
                }

                return _basketItems;
            }

            set { _basketItems = value; }
        }
        public decimal TotalPrice
        {
            get => _basketItems.Sum(x => x.GetCurrentPrice);
        }
        public bool HasDiscount
        {
            get=>!string.IsNullOrWhiteSpace(DiscountCode) && DiscountRate.HasValue;
        }
    }
}
