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
    public class OrderRepository: IOrderRepository
    {

        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddOrderAsync(Order order)
        {
            var sql = "Sales.AddNewOrder"; // Nombre del SP

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                foreach (var detail in order.OrderDetails)
                {
                    var parameters = new
                    {
                        CustId = order.CustId,
                        EmpId = order.EmpId,
                        OrderDate = order.OrderDate,
                        RequiredDate = order.RequiredDate,
                        ShippedDate = order.ShippedDate,
                        ShipperId = order.ShipperId,
                        Freight = order.Freight,
                        ShipName = order.ShipName,
                        ShipAddress = order.ShipAddress,
                        ShipCity = order.ShipCity,
                        
                        ShipCountry = order.ShipCountry,
                        ProductId = detail.ProductId,
                        UnitPrice = detail.UnitPrice,
                        Qty = detail.Qty,
                        Discount = detail.Discount
                    };

                    await conn.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
        }
    }
}
