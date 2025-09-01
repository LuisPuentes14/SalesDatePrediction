using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;
using SalesDatePrediction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Aplication.UseCases
{
    public class GetAllShipperUseCase: IGetAllShipperUseCase
    {
        private readonly IShipperRepository _repository;

        public GetAllShipperUseCase(IShipperRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ShipperDTO>> ExecuteAsync()
        {
            var orders = await _repository.GetAllShippersAsync();

            IEnumerable<ShipperDTO> dtoList = orders.Select(s => new ShipperDTO
            {
                ShipperId = s.ShipperId,
                CompanyName = s.CompanyName
            });


            return dtoList;
        }
    }
}
