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
    public class GetAllProductsUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsMappedProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Laptop" },
                new Product { ProductId = 2, ProductName = "Smartphone" }
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetAllProductsAsync())
                    .ReturnsAsync(products);

            var useCase = new GetAllProductsUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.ProductId == 1 && p.ProductName == "Laptop");
            Assert.Contains(result, p => p.ProductId == 2 && p.ProductName == "Smartphone");

            // Verificar que el repo se llamó exactamente una vez
            mockRepo.Verify(r => r.GetAllProductsAsync(), Times.Once);
        }
    }
}
