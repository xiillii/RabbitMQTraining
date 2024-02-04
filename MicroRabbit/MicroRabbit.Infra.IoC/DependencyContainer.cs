using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.Microservices.Banking.Application.Services;
using MicroRabbit.Microservices.Banking.Data.Context;
using MicroRabbit.Microservices.Banking.Data.Repository;
using MicroRabbit.Microservices.Banking.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Infra.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Domain bus
        services.AddTransient<IEventBus, RabbitMQBus>(sp =>
        {
            
            return new RabbitMQBus(sp.GetService<IMediator>());
        });

        // Application Services
        services.AddTransient<IAccountService, AccountService>();

        // Data
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<BankingDbContext>();
    }
}
