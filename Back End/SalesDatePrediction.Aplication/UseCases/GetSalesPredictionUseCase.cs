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
    public class GetSalesPredictionUseCase : IGetSalesPredictionUseCase
    {
        private readonly ICustomerRepository _repository;

        public GetSalesPredictionUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalesDatePredictionDTO>> ExecuteAsync()
        {
            var predictions = await _repository.GetSalesDatePrediction();

            IEnumerable<SalesDatePredictionDTO> dtoList = predictions.Select(p => new SalesDatePredictionDTO
            {
                CustId = p.CustId,
                CustomerName = p.CustomerName,
                LastOrderDate = p.LastOrderDate,
                NextPredictedOrder = p.NextPredictedOrder
            });


            return dtoList;
        }


    }
}
