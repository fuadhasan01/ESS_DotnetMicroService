using TaskManagement.Application.Employees;
using TaskManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Persistence.Employees;

public class EmployeeReader : IEmployeeReader
{
    private readonly TaskManagementDbContext _db;
    public EmployeeReader(TaskManagementDbContext db) => _db = db;

    public Task<EmployeeDto?> ById(Guid id, CancellationToken ct) =>
        _db.Employees
            .Where(e => e.Id == id)
            .Select(e => new EmployeeDto(e.Id, e.Name, e.Email))
            .FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<EmployeeDto>> List(int skip, int take, CancellationToken ct) =>
        await _db.Employees
            .OrderBy(e => e.Name)
            .Skip(skip).Take(take)
            .Select(e => new EmployeeDto(e.Id, e.Name, e.Email))
            .ToListAsync(ct);
}