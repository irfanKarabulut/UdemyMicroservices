using UdemyMicroservices.Web.Models.Order;

namespace UdemyMicroservices.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron yapı
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);

        /// <summary>
        /// Asenkron yapı için, rabbitMQ kullanılacak
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrders();
    }
}
