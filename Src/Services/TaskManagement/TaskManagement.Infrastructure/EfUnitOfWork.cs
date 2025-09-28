using TaskManagement.Application.Abstractions;

namespace TaskManagement.Infrastructure;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly TaskManagementDbContext _db;
    public EfUnitOfWork(TaskManagementDbContext db) => _db = db;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}