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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString) {

            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(long id)
        {
            var sql = @"SELECT 
                        o.orderid AS OrderId,
                        o.requireddate AS RequiredDate,
                        o.shippeddate AS ShippedDate,
                        o.shipname AS ShipName,
                        o.shipaddress AS ShipAddress,
                        o.shipcity AS ShipCity
                    FROM Sales.Orders o
                    WHERE o.custid = @CustomerId;";

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Order>(sql, new { CustomerId = id });
            }
        }

        public Task<IEnumerable<SalesDatePredictions>> GetSalesDatePrediction()
        {         

            string query = @"WITH OrderIntervals AS (
                                SELECT 
                                    o.custid,
                                    DATEDIFF(DAY, 
                                             LAG(o.orderdate) OVER (PARTITION BY o.custid ORDER BY o.orderdate),
                                             o.orderdate) AS DaysBetween
                                FROM Sales.Orders o
                                WHERE o.custid IS NOT NULL
                            )
                            , AvgIntervals AS (
                                SELECT custid, AVG(DaysBetween * 1.0) AS AvgDays
                                FROM OrderIntervals
                                WHERE DaysBetween IS NOT NULL
                                GROUP BY custid
                            )
                            , LastOrders AS (
                                SELECT custid, MAX(orderdate) AS LastOrderDate
                                FROM Sales.Orders
                                GROUP BY custid
                            )
                            SELECT 
                                c.custid AS CustId,
                                c.companyname AS CustomerName,
                                lo.LastOrderDate AS LastOrderDate,
                                DATEADD(DAY, ISNULL(ai.AvgDays, 0), lo.LastOrderDate) AS NextPredictedOrder
                            FROM Sales.Customers c
                            JOIN LastOrders lo ON c.custid = lo.custid
                            LEFT JOIN AvgIntervals ai ON c.custid = ai.custid;";

            using (var conn = new SqlConnection(_connectionString))
            {
                var predictions = conn.Query<SalesDatePredictions>(query);

                if (predictions != null && predictions.Any()) {
                    return Task.FromResult(predictions);
                }                  
            }

            return Task.FromResult(Enumerable.Empty<SalesDatePredictions>());
        }
    }
}
