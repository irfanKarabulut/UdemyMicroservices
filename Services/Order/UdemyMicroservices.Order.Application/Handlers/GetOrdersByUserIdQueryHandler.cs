
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyMicroservices.Order.Application.Dtos;
using UdemyMicroservices.Order.Application.Mapping;
using UdemyMicroservices.Order.Application.Queries;
using UdemyMicroservices.Order.Infrastructure;
using UdemyMicroservices.Shared.Dtos;

namespace UdemyMicroservices.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
           var orders = await _context.Orders.Include(x=>x.OrderItems).Where(x=>x.BuyerId==request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(),200);
            }

            return Response<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<OrderDto>>(orders), 200);
        }
    }
}
