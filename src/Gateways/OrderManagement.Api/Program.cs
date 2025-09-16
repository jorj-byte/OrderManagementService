using Finance.Infrastructure;
using Financial.Application.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure;
using Shared.Identity;
using Serilog;
using Shared.Infrastructure;
using Shared.Kernel;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblies(
        typeof(Order.Application.AssemblyMarker).Assembly,
        typeof(Financial.Application.AssemblyMarker).Assembly,
        typeof(Shared.Kernel.AssemblyMarker).Assembly
    ));
builder.Services.AddOrdersModule(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddFinanceModule(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<ICacheService, RedisCacheService>();

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConfirmedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseUserIdMiddleware();
app.UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();