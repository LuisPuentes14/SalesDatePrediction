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
    public class GetSalesPredictionUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsMappedSalesPredictions()
        {
            // Arrange
            var predictions = new List<SalesDatePredictions>
            {
                new SalesDatePredictions
                {
                    CustId = 1,
                    CustomerName = "Cliente X",
                    LastOrderDate = DateTime.Today.AddDays(-15),
                    NextPredictedOrder = DateTime.Today.AddDays(5)
                },
                new SalesDatePredictions
                {
                    CustId = 2,
                    CustomerName = "Cliente Y",
                    LastOrderDate = DateTime.Today.AddDays(-30),
                    NextPredictedOrder = DateTime.Today.AddDays(10)
                }
            };

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(r => r.GetSalesDatePrediction())
                    .ReturnsAsync(predictions);

            var useCase = new GetSalesPredictionUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var predictionList = result.ToList();

            Assert.Equal(1, predictionList[0].CustId);
            Assert.Equal("Cliente X", predictionList[0].CustomerName);
            Assert.Equal(predictions[0].LastOrderDate, predictionList[0].LastOrderDate);
            Assert.Equal(predictions[0].NextPredictedOrder, predictionList[0].NextPredictedOrder);

            Assert.Equal(2, predictionList[1].CustId);
            Assert.Equal("Cliente Y", predictionList[1].CustomerName);
            Assert.Equal(predictions[1].LastOrderDate, predictionList[1].LastOrderDate);
            Assert.Equal(predictions[1].NextPredictedOrder, predictionList[1].NextPredictedOrder);

            // Verifica que el repo se llamó una sola vez
            mockRepo.Verify(r => r.GetSalesDatePrediction(), Times.Once);
        }
    }
}
