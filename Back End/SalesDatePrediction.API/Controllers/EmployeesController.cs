using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IGetAllEmployeesUseCase _getAllEmployeesUseCase;
        public EmployeesController(IGetAllEmployeesUseCase getAllEmployeesUseCase)
        {
            _getAllEmployeesUseCase = getAllEmployeesUseCase;
        }

        [HttpGet]
        [Route("AllEmployees")]
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            return await _getAllEmployeesUseCase.ExecuteAsync();
        }       
    }
}
