using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesDatePrediction.Domain.Interfaces;
using SalesDatePrediction.Infrastructure.Persistence.Repositories;

namespace SalesDatePrediction.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            string sqlServerConnectionString = configuration.GetConnectionString("SqlServerConnection");

            services.AddScoped<ICustomerRepository>(provider => new CustomerRepository(sqlServerConnectionString));
            services.AddScoped<IEmployeeRepository>(provider => new EmployeeRepository(sqlServerConnectionString));
            services.AddScoped<IOrderRepository>(provider => new OrderRepository(sqlServerConnectionString));
            services.AddScoped<IProductRepository>(provider => new ProductRepository(sqlServerConnectionString));
            services.AddScoped<IShipperRepository>(provider => new ShipperRepository(sqlServerConnectionString));


            return services;
        }
    }
}
