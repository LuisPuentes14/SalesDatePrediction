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
    public class ShippersControllerTests
    {
        private readonly Mock<IGetAllShipperUseCase> _mockGetAllShipperUseCase;
        private readonly ShippersController _controller;

        public ShippersControllerTests()
        {
            _mockGetAllShipperUseCase = new Mock<IGetAllShipperUseCase>();
            _controller = new ShippersController(_mockGetAllShipperUseCase.Object);
        }

        [Fact]
        public async Task GetAllShippers_ReturnsListOfShippers()
        {
            // Arrange
            var shippers = new List<ShipperDTO>
        {
            new ShipperDTO { ShipperId = 1, CompanyName = "DHL" },
            new ShipperDTO { ShipperId = 2, CompanyName = "FedEx" }
        };

            _mockGetAllShipperUseCase
                .Setup(x => x.ExecuteAsync())
                .ReturnsAsync(shippers);

            // Act
            var result = await _controller.GetAllShippers();

            // Assert
            Assert.NotNull(result);
            var shipperList = Assert.IsAssignableFrom<IEnumerable<ShipperDTO>>(result);
            Assert.Equal(2, ((List<ShipperDTO>)shipperList).Count);
            Assert.Contains(shipperList, s => s.CompanyName == "DHL");
            Assert.Contains(shipperList, s => s.CompanyName == "FedEx");

            _mockGetAllShipperUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
        }
    }
}
