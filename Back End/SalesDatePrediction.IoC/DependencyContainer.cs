using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesDatePrediction.Aplication;
using SalesDatePrediction.Infrastructure;

namespace SalesDatePrediction.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddAplicationServices();

            return services;
        }
    }
}
