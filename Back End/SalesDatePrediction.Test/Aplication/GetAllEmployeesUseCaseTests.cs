using Moq;
using SalesDatePrediction.Aplication.UseCases;
using SalesDatePrediction.Domain.Entities;
using SalesDatePrediction.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Test.Aplication
{
    public class GetAllEmployeesUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsMappedEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmpId = 1, FirstName = "John", LastName = "Doe" },
                new Employee { EmpId = 2, FirstName = "Jane", LastName = "Smith" }
            };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.GetAllEmployeesAsync())
                    .ReturnsAsync(employees);

            var useCase = new GetAllEmployeesUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.EmpId == 1 && e.FullName == "John Doe");
            Assert.Contains(result, e => e.EmpId == 2 && e.FullName == "Jane Smith");

            // Verificar que se llamó al repositorio
            mockRepo.Verify(r => r.GetAllEmployeesAsync(), Times.Once);
        }
    }
}

