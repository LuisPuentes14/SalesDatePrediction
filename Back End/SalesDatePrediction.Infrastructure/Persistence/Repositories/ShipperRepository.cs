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
    public class ShipperRepository : IShipperRepository
    {

        private readonly string _connectionString;

        public ShipperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            var sql = @"SELECT 
                        s.shipperid AS ShipperId,
                        s.companyname AS CompanyName
                    FROM Sales.Shippers s;";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Shipper>(sql);
            }
        }
    }
}
