using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesDatePrediction.Aplication.Interfaces;
using SalesDatePrediction.Aplication.UseCases;
using SalesDatePrediction.Domain.Interfaces;

namespace SalesDatePrediction.Aplication
{
    public static class  DependencyContainer
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IGetSalesPredictionUseCase, GetSalesPredictionUseCase>();
            services.AddScoped<IGetOrdersByCustomerIdUseCase, GetOrdersByCustomerIdUseCase>();
            services.AddScoped<IGetAllEmployeesUseCase, GetAllEmployeesUseCase>();
            services.AddScoped<IGetAllShipperUseCase, GetAllShipperUseCase>();
            services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
          
            return services;
        }
    }
}
