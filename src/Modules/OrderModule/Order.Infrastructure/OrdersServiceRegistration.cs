

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Persistance;
using Order.Infrastructure.Repository;
using OrderApplication.Contracts;
using Shared.Kernel;

namespace Order.Infrastructure
{
    public static class OrdersServiceRegistration
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services, string connectionString)
        {
            // DbContext
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}