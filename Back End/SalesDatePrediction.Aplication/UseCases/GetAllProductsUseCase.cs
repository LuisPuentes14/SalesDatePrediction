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
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductRepository _repository;

        public GetAllProductsUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO>> ExecuteAsync()
        {
            var orders = await _repository.GetAllProductsAsync();

            IEnumerable<ProductDTO> dtoList = orders.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName
            });


            return dtoList;
        }
    }
}
