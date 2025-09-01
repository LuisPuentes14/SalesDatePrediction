using Dapper;
using Microsoft.Data.SqlClient;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var sql = @"SELECT 
                        p.productid AS ProductId,
                        p.productname AS ProductName
                    FROM Production.Products p;";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Product>(sql);
            }
        }
    }
}
