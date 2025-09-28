using TaskManagement.Application.Employees;
using TaskManagement.Domain;

namespace TaskManagement.Infrastructure.Persistence.Employees;

public class EmployeesWriter : IEmployeeWriter
{
    private readonly TaskManagementDbContext _db;
    public EmployeesWriter(TaskManagementDbContext db) => _db = db;

    public async Task<Guid> Add(string name, string email, CancellationToken ct)
    {
        var employee = new Employee(name, email);
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync(ct);
        return employee.Id;
    }

    public async Task Update(Guid id, string name, string email, CancellationToken ct)
    {
        var employee = await _db.Employees.FindAsync(new object[] { id }, ct);
        if (employee == null) throw new ArgumentException("Employee not found", nameof(id));
        employee.GetType().GetProperty("Name")!.SetValue(employee, name);
        employee.GetType().GetProperty("Email")!.SetValue(employee, email);
        await _db.SaveChangesAsync(ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var employee = await _db.Employees.FindAsync(new object[] { id }, ct);
        if (employee == null) throw new ArgumentException("Employee not found", nameof(id));
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync(ct);
    }
}