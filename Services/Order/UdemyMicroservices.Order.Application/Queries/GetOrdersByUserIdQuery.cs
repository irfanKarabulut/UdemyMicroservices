using MediatR;
using UdemyMicroservices.Order.Application.Dtos;
using UdemyMicroservices.Shared.Dtos;

namespace UdemyMicroservices.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
