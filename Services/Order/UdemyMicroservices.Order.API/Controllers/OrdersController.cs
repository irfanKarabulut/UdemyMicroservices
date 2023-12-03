using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Order.Application.Commands;
using UdemyMicroservices.Order.Application.Queries;
using UdemyMicroservices.Shared.Services;

namespace UdemyMicroservices.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _sharedIdentityService = sharedIdentityService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders() {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {          
            var response = await _mediator.Send(createOrderCommand);

            return Ok(response);
        }
    }
}
