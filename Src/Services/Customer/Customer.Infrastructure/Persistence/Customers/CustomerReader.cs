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
            .Select(c => new CustomerDto(c.Id, c.Name, c.Email, c.Phone)) // Phone is now included
            .FirstOrDefaultAsync(ct);

    //Get list only status active customers

    public async Task<IReadOnlyList<CustomerDto>> List(int skip, int take, CancellationToken ct) =>

        await _db.Customers
            .OrderBy(c => c.Name)
            .Skip(skip).Take(take)
            .Select(c => new CustomerDto(c.Id, c.Name, c.Email, c.Phone)) // Phone is now included
            .ToListAsync(ct);

    //Active Customers
    public async Task<IReadOnlyList<CustomerDto>> ActiveCustomers(int skip, int take, CancellationToken ct) =>
        await _db.Customers
            .Where(c => c.Status == CustomerStatus.Active)
            .OrderBy(c => c.Name)
            .Skip(skip).Take(take)
            .Select(c => new CustomerDto(c.Id, c.Name, c.Email, c.Phone)) // Phone is now included
            .ToListAsync(ct);
}