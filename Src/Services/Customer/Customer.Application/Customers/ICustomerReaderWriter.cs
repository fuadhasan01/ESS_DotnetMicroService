using System.Threading;
using System.Threading.Tasks;

namespace Customer.Application.Customers;

public interface ICustomerReader
{
    Task<CustomerDto?> ById(Guid id, CancellationToken ct);
    Task<IReadOnlyList<CustomerDto>> List(int skip, int take, CancellationToken ct);
}

public interface ICustomersWriter
{
    Task<Guid> Add(string name, string? email, string? phone, CancellationToken ct);
    Task Update(Guid id, string name, string? email, string? phone, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
}