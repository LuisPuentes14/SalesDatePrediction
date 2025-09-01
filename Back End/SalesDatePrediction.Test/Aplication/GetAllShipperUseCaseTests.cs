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
    public class GetAllShipperUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsMappedShippers()
        {
            // Arrange
            var shippers = new List<Shipper>
            {
                new Shipper { ShipperId = 1, CompanyName = "DHL" },
                new Shipper { ShipperId = 2, CompanyName = "FedEx" }
            };

            var mockRepo = new Mock<IShipperRepository>();
            mockRepo.Setup(r => r.GetAllShippersAsync())
                    .ReturnsAsync(shippers);

            var useCase = new GetAllShipperUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.ShipperId == 1 && s.CompanyName == "DHL");
            Assert.Contains(result, s => s.ShipperId == 2 && s.CompanyName == "FedEx");

            // Verificar que el repo se llamó exactamente una vez
            mockRepo.Verify(r => r.GetAllShippersAsync(), Times.Once);
        }
    }
}
