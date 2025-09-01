using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Aplication.UseCases
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {

        private readonly IOrderRepository _repository;

        public CreateOrderUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateOrderDTO createOrderDTO)
        {

            Order order = new Order
            {
                CustId = createOrderDTO.CustId,
                EmpId = createOrderDTO.EmpId,
                OrderDate = createOrderDTO.OrderDate,
                RequiredDate = createOrderDTO.RequiredDate,
                ShippedDate = createOrderDTO.ShippedDate,
                ShipperId = createOrderDTO.ShipperId,
                Freight = createOrderDTO.Freight,
                ShipName = createOrderDTO.ShipName,
                ShipAddress = createOrderDTO.ShipAddress,
                ShipCity = createOrderDTO.ShipCity,
                ShipCountry = createOrderDTO.ShipCountry,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductId = createOrderDTO.ProductId,
                        UnitPrice = createOrderDTO.UnitPrice,
                        Qty = createOrderDTO.Qty,
                        Discount = createOrderDTO.Discount
                    }
                }
            };

            await _repository.AddOrderAsync(order);          
        }
    }
}
