using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Web.Models.Order;
using UdemyMicroservices.Web.Services.Interfaces;

namespace UdemyMicroservices.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;    

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            // Senkron iletişim
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            // Asenkron iletişim RabbitMQ, MassTransit ile
            var orderStatus = await _orderService.SuspendOrder(checkoutInfoInput);

            if (!orderStatus.IsSuccessful) 
            { 
                ViewBag.error = orderStatus.Error;
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                return View(checkoutInfoInput); 
            }

            // Senkron iletişim
            // return RedirectToAction(nameof(SuccessfulCheckout),new {orderId=orderStatus.OrderId});

            //Asenkron
            return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(500,1000) });
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrders());
        }
    }
}
