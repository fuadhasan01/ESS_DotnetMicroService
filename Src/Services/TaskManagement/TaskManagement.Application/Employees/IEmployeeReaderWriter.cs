
namespace TaskManagement.Application.Employees;

public interface IEmployeeReader
{
    Task<EmployeeDto?> ById(Guid id, CancellationToken ct);
    Task<IReadOnlyList<EmployeeDto>> List(int skip, int take, CancellationToken ct);
}

public interface IEmployeeWriter
{
    Task<Guid> Add(string name, string email, CancellationToken ct);
    Task Update(Guid id, string name, string email, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
}