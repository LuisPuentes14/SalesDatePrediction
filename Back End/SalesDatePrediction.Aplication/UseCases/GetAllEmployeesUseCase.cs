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
    public class GetAllEmployeesUseCase : IGetAllEmployeesUseCase
    {
        private readonly IEmployeeRepository _repository;

        public GetAllEmployeesUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDTO>> ExecuteAsync()
        {
            var employees = await _repository.GetAllEmployeesAsync();

            IEnumerable<EmployeeDTO> dtoList = employees.Select(e => new EmployeeDTO
            {
                EmpId = e.EmpId,
                FullName = e.FullName
            });


            return dtoList;
        }

    }
}
