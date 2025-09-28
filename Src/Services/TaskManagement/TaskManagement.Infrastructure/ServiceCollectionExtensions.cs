using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTaskManagementPersistence(
        this IServiceCollection services, IConfiguration cfg)
    {
        var db = new DbOptions();
        cfg.GetSection("Database").Bind(db);

        services.AddDbContext<TaskManagementDbContext>(opt =>
            opt.UseNpgsql(db.ConnectionString, npg =>
                npg.MigrationsHistoryTable("__EFMigrationsHistory", db.Schema)));

        return services;
    }
}