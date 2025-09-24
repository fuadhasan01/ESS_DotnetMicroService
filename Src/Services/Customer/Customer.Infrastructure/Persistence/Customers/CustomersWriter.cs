using Customer.Application.Customers;
using Customer.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Persistence.Customers;

public sealed class CustomersWriter : ICustomersWriter
{
    private readonly CustomerDbContext _db;
    public CustomersWriter(CustomerDbContext db) => _db = db;

    public async Task<Guid> Add(string name, string? email, string? phone, CancellationToken ct)
    {
        var customer = new Customer.Domain.Customer(name, email ?? string.Empty);
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(ct);
        return customer.Id;
    }

    public async Task Update(Guid id, string name, string? email, string? phone, CancellationToken ct)
    {
        var customer = await _db.Customers.FindAsync(new object[] { id }, ct);
        if (customer == null) throw new ArgumentException("Customer not found", nameof(id));
        customer.Update(name, email ?? string.Empty);
        await _db.SaveChangesAsync(ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var customer = await _db.Customers.FindAsync(new object[] { id }, ct);
        if (customer == null) throw new ArgumentException("Customer not found", nameof(id));
        customer.Deactivate();
        await _db.SaveChangesAsync(ct);
    }
}