using Customer.Application.Abstractions;

namespace Customer.Infrastructure;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly CustomerDbContext _db;
    public EfUnitOfWork(CustomerDbContext db) => _db = db;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}