using Customer.Application.Abstractions;
using Customer.Application.Customers;
using Customer.Infrastructure.Persistence.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomerInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICustomerReader, CustomerReader>();
        services.AddScoped<ICustomersWriter, CustomersWriter>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        // Register handlers if needed
        return services;
    }
}