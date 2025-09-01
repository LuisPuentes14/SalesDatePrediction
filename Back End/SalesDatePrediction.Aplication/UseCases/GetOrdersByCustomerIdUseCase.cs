using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Aplication.UseCases
{
    public class GetOrdersByCustomerIdUseCase: IGetOrdersByCustomerIdUseCase
    {
        private readonly ICustomerRepository _repository;

        public GetOrdersByCustomerIdUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDTO>> ExecuteAsync(long id)
        {
            var orders = await _repository.GetOrdersByCustomerIdAsync(id);

            IEnumerable<OrderDTO> dtoList = orders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                ShipName = o.ShipName,
                ShipAddress = o.ShipAddress,
                ShipCity = o.ShipCity
            });


            return dtoList;
        }

    }
}
