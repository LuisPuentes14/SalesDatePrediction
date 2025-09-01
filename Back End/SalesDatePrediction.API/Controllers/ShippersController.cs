using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IGetAllShipperUseCase _getAllShipperUseCase;
        public ShippersController(IGetAllShipperUseCase getAllShipperUseCase)
        {
            _getAllShipperUseCase = getAllShipperUseCase;
        }

        [HttpGet]
        [Route("AllShippers")]
        public async Task<IEnumerable<ShipperDTO>> GetAllShippers()
        {
            return await _getAllShipperUseCase.ExecuteAsync();
        }       
    }
}
