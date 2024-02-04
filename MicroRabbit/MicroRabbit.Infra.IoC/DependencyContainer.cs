using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.Microservices.Banking.Application.Services;
using MicroRabbit.Microservices.Banking.Data.Context;
using MicroRabbit.Microservices.Banking.Data.Repository;
using MicroRabbit.Microservices.Banking.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Domain bus
        services.AddTransient<IEventBus, RabbitMQBus>();

        // Application Services
        services.AddTransient<IAccountService, AccountService>();

        // Data
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<BankingDbContext>();
    }
}
