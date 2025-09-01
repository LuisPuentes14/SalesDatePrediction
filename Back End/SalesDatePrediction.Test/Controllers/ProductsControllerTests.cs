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
    public class ProductsControllerTests
    {
        private readonly Mock<IGetAllProductsUseCase> _mockGetAllProductsUseCase;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockGetAllProductsUseCase = new Mock<IGetAllProductsUseCase>();
            _controller = new ProductsController(_mockGetAllProductsUseCase.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<ProductDTO>
        {
            new ProductDTO { ProductId = 1, ProductName = "Laptop" },
            new ProductDTO { ProductId = 2, ProductName = "Mouse" }
        };

            _mockGetAllProductsUseCase
                .Setup(x => x.ExecuteAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllProdcuts();

            // Assert
            Assert.NotNull(result);
            var productList = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(result);
            Assert.Equal(2, ((List<ProductDTO>)productList).Count);
            Assert.Contains(productList, p => p.ProductName == "Laptop");
            Assert.Contains(productList, p => p.ProductName == "Mouse");

            _mockGetAllProductsUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
        }
    }
}
