using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Tasks;
using TaskManagement.Application.Employees;
using TaskManagement.Application.Abstractions;
using TaskManagement.Infrastructure.Persistence.Tasks;
using TaskManagement.Infrastructure.Persistence.Employees;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskManagementInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITaskReader, TaskReader>();
        services.AddScoped<ITaskWriter, TasksWriter>();
        services.AddScoped<IEmployeeReader, EmployeeReader>();
        services.AddScoped<IEmployeeWriter, EmployeesWriter>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<TaskAssignmentWriter>();
        return services;
    }
}