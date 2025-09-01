using Moq;
using SalesDatePrediction.API.Controllers;
using SalesDatePrediction.Aplication.DTOs;
using SalesDatePrediction.Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Test.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IGetAllEmployeesUseCase> _mockGetAllEmployeesUseCase;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockGetAllEmployeesUseCase = new Mock<IGetAllEmployeesUseCase>();
            _controller = new EmployeesController(_mockGetAllEmployeesUseCase.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var employees = new List<EmployeeDTO>
        {
            new EmployeeDTO { EmpId = 1, FullName = "John Doe" },
            new EmployeeDTO { EmpId = 2, FullName = "Jane Smith" }
        };

            _mockGetAllEmployeesUseCase
                .Setup(x => x.ExecuteAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            Assert.NotNull(result);
            var employeeList = Assert.IsAssignableFrom<IEnumerable<EmployeeDTO>>(result);
            Assert.Equal(2, ((List<EmployeeDTO>)employeeList).Count);
            Assert.Contains(employeeList, e => e.FullName == "John Doe");
            Assert.Contains(employeeList, e => e.FullName == "Jane Smith");

            _mockGetAllEmployeesUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
        }
    }
}
