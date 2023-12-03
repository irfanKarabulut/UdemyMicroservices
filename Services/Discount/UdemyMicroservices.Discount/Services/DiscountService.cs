using Dapper;
using Npgsql;
using System.Data;
using UdemyMicroservices.Discount.Models;
using UdemyMicroservices.Shared.Dtos;

namespace UdemyMicroservices.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbconnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbconnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var result = await _dbconnection.ExecuteAsync("delete from discount where id=@Id)", new {Id=id});

            if (result > 0)
            {
                return Response<NoContent>.Success(200);
            }

            return Response<NoContent>.Fail("An error occurred while deleting", 500);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbconnection.QueryAsync<Models.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code=code })).FirstOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found", 404);
            }

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbconnection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new {Id=id})).SingleOrDefault();

            if (discount==null)
            {
                return Response<Models.Discount>.Fail("Discount not found",404);
            }

            return Response<Models.Discount>.Success(discount,200);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbconnection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var result = await _dbconnection.ExecuteAsync("insert into discount (userid,rate,code) values(@UserId,@Rate,@Code)",discount);

            if (result>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("An error occurred while adding",500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var result = await _dbconnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id)", discount);

            if (result > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("An error occurred while updating", 500);
        }
    }
}
