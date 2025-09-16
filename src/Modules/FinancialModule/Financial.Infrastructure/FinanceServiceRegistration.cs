using Financial.Application.Consumers;
using Financial.Infrastructure;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure
{
    public static class FinanceServiceRegistration
    {
        public static IServiceCollection AddFinanceModule(this IServiceCollection services, string connectionString)
        {
            // DbContext
            services.AddDbContext<FinanceDbContext>(options =>
                options.UseSqlServer(connectionString));

            // MassTransit Consumers
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConfirmedConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost"); // or read from configuration
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}