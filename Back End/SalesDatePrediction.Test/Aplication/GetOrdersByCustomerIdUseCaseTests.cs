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
    public class GetOrdersByCustomerIdUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsMappedOrders_ForGivenCustomerId()
        {
            // Arrange
            long customerId = 1;
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 100,
                    RequiredDate = DateTime.Today.AddDays(7),
                    ShippedDate = null,
                    ShipName = "Cliente X",
                    ShipAddress = "Calle Falsa 123",
                    ShipCity = "Madrid"
                },
                new Order
                {
                    OrderId = 101,
                    RequiredDate = DateTime.Today.AddDays(10),
                    ShippedDate = DateTime.Today,
                    ShipName = "Cliente Y",
                    ShipAddress = "Av. Real 456",
                    ShipCity = "Barcelona"
                }
            };

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(r => r.GetOrdersByCustomerIdAsync(customerId))
                    .ReturnsAsync(orders);

            var useCase = new GetOrdersByCustomerIdUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var orderList = result.ToList();
            Assert.Equal(100, orderList[0].OrderId);
            Assert.Equal("Cliente X", orderList[0].ShipName);
            Assert.Equal("Madrid", orderList[0].ShipCity);

            Assert.Equal(101, orderList[1].OrderId);
            Assert.Equal("Cliente Y", orderList[1].ShipName);
            Assert.Equal("Barcelona", orderList[1].ShipCity);

            // Verifica que el repo fue llamado una sola vez con el parámetro correcto
            mockRepo.Verify(r => r.GetOrdersByCustomerIdAsync(customerId), Times.Once);
        }
    }
}
