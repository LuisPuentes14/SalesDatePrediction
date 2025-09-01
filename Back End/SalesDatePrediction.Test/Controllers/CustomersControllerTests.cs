using Microsoft.AspNetCore.Mvc;
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
    public class CustomersControllerTests
    {
        private readonly Mock<IGetSalesPredictionUseCase> _mockSalesPredictionUseCase;
        private readonly Mock<IGetOrdersByCustomerIdUseCase> _mockOrdersByCustomerIdUseCase;
        private readonly Mock<ICreateOrderUseCase> _mockCreateOrderUseCase;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockSalesPredictionUseCase = new Mock<IGetSalesPredictionUseCase>();
            _mockOrdersByCustomerIdUseCase = new Mock<IGetOrdersByCustomerIdUseCase>();
            _mockCreateOrderUseCase = new Mock<ICreateOrderUseCase>();

            _controller = new CustomersController(
                _mockSalesPredictionUseCase.Object,
                _mockOrdersByCustomerIdUseCase.Object,
                _mockCreateOrderUseCase.Object
            );
        }

        [Fact]
        public async Task GetSalesPrediction_ReturnsSalesPredictions()
        {
            // Arrange
            var mockPredictions = new List<SalesDatePredictionDTO>
            {
                new SalesDatePredictionDTO { CustId = 1, CustomerName = "Cliente X" },
                new SalesDatePredictionDTO { CustId = 2, CustomerName = "Cliente Y" }
            };

            _mockSalesPredictionUseCase.Setup(x => x.ExecuteAsync())
                .ReturnsAsync(mockPredictions);

            // Act
            var result = await _controller.GetSalesPrediction();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockSalesPredictionUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOrdersByCustomer_ReturnsOrders()
        {
            // Arrange
            long customerId = 5;
            var mockOrders = new List<OrderDTO>
            {
                new OrderDTO { OrderId = 1, ShipName = "Cliente A" },
                new OrderDTO { OrderId = 2, ShipName = "Cliente B" }
            };

            _mockOrdersByCustomerIdUseCase.Setup(x => x.ExecuteAsync(customerId))
                .ReturnsAsync(mockOrders);

            // Act
            var result = await _controller.GetOrdersByCustomer(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockOrdersByCustomerIdUseCase.Verify(x => x.ExecuteAsync(customerId), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var newOrder = new CreateOrderDTO { CustId = 1, ProductId = 2 };

            _mockCreateOrderUseCase.Setup(x => x.ExecuteAsync(newOrder))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.GetOrdersByCustomer(newOrder);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            _mockCreateOrderUseCase.Verify(x => x.ExecuteAsync(newOrder), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var newOrder = new CreateOrderDTO { CustId = 1, ProductId = 2 };

            _mockCreateOrderUseCase.Setup(x => x.ExecuteAsync(newOrder))
                .ThrowsAsync(new Exception("Error al crear"));

            // Act
            var result = await _controller.GetOrdersByCustomer(newOrder);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            _mockCreateOrderUseCase.Verify(x => x.ExecuteAsync(newOrder), Times.Once);
        }
    }
}
