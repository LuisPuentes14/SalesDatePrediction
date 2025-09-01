using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGetSalesPredictionUseCase _getSalesPredictionUseCase;
        private readonly IGetOrdersByCustomerIdUseCase  _getOrdersByCustomerIdUseCase;
        private readonly ICreateOrderUseCase _createOrderUseCase;
        public CustomersController(IGetSalesPredictionUseCase getSalesPredictionUseCase,
            IGetOrdersByCustomerIdUseCase getOrdersByCustomerIdUseCase,
            ICreateOrderUseCase createOrderUseCase
            )
        {
            _getSalesPredictionUseCase = getSalesPredictionUseCase;
            _getOrdersByCustomerIdUseCase = getOrdersByCustomerIdUseCase;
            _createOrderUseCase = createOrderUseCase;
        }
        
        [HttpGet]
        [Route("SalesPrediction")]
        public async  Task<IEnumerable<SalesDatePredictionDTO>> GetSalesPrediction()
        {
           return await _getSalesPredictionUseCase.ExecuteAsync();
        }

        [HttpGet]
        [Route("OrdersByCustomer/{id}")]
        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomer(long id )
        {
            return await _getOrdersByCustomerIdUseCase.ExecuteAsync(id);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> GetOrdersByCustomer([FromBody] CreateOrderDTO createOrderDTO)
        {
            try
            {
               await _createOrderUseCase.ExecuteAsync(createOrderDTO);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

             return Ok();
        }
    }
}
