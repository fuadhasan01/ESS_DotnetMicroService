using Customer.Application.Customers;
using Customer.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Persistence.Customers;

public sealed class CustomerReader : ICustomerReader
{
    private readonly CustomerDbContext _db;
    public CustomerReader(CustomerDbContext db) => _db = db;

    public Task<CustomerDto?> ById(Guid id, CancellationToken ct) =>
        _db.Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerDto(c.Id, c.Name, c.Email, null)) // Phone is not present
            .FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<CustomerDto>> List(int skip, int take, CancellationToken ct) =>
        await _db.Customers
            .OrderBy(c => c.Name)
            .Skip(skip).Take(take)
            .Select(c => new CustomerDto(c.Id, c.Name, c.Email, null)) // Phone is not present
            .ToListAsync(ct);
}