using Microsoft.Extensions.DependencyInjection;
using WebAppAlexey.DAL.Interfaces;
using WebAppAlexey.DAL.Models;
using WebAppAlexey.DAL.Repositories;

namespace WebAppAlexey.DAL.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, EFUnitOfWork>()
                .AddTransient<IRepository<User>, UserRepository>();

            return services;
        }
    }
}
