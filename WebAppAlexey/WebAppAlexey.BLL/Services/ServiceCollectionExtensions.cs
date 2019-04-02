using Microsoft.Extensions.DependencyInjection;
using WebAppAlexey.BLL.Interfaces;

namespace WebAppAlexey.BLL.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<IAdmin, AdminService>();

            return services;
        }
    }
}
