using Dapper;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("DELETE from discount where id=@Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);

        }

        public async Task<Response<List<DiscountDto>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<DiscountDto>("Select * from discount");

            return Response<List<DiscountDto>>.Success(discounts.ToList(), 200);

        }

        public async Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<DiscountDto>("select * from discount where code=@Code and userid=@UserId", new { Code = code, UserId = userId })).SingleOrDefault();
            if(discount==null)
            {
                return Response<DiscountDto>.Fail("Discount not found", 404);
            }
            return Response<DiscountDto>.Success(discount, 200);
        }

        public async Task<Response<DiscountDto>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<DiscountDto>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
            {
                return Response<DiscountDto>.Fail("Discount not found", 404);
            }
            return Response<DiscountDto>.Success(discount, 200);

        }

        public async Task<Response<NoContent>> Save(DiscountDto discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

            if(saveStatus>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("An error occurred while adding",500);

        }

        public async Task<Response<NoContent>> Update(DiscountDto discount)
        {
            var status = await _dbConnection.ExecuteAsync("UPDATE discount set userid=@UserId,rate=@Rate,code=@Code where id=@Id",
                new
                {
                    Id = discount.Id,
                    UserId = discount.UserId,
                    Rate = discount.Rate,
                    Code = discount.Code
                });

            if(status>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount not found ", 404);
        }
    }
}
