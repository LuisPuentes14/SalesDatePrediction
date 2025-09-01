using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGetAllProductsUseCase _getAllProductsUseCase;
        public ProductsController(IGetAllProductsUseCase getAllProductsUseCase)
        {
            _getAllProductsUseCase = getAllProductsUseCase;
        }

        [HttpGet]
        [Route("AllProducts")]
        public async Task<IEnumerable<ProductDTO>> GetAllProdcuts()
        {
            return await _getAllProductsUseCase.ExecuteAsync();
        }       
    }
}
