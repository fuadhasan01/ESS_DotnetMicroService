using TaskManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Persistence.Tasks;

public class TaskAssignmentWriter
{
    private readonly TaskManagementDbContext _db;
    public TaskAssignmentWriter(TaskManagementDbContext db) => _db = db;

    public async Task Assign(Guid taskId, Guid employeeId, CancellationToken ct)
    {
        var exists = await _db.TaskAssignments.AnyAsync(a => a.TaskId == taskId && a.EmployeeId == employeeId, ct);
        if (exists) throw new InvalidOperationException("Task already assigned to this employee.");

        var assignment = new TaskAssignment(taskId, employeeId);
        _db.TaskAssignments.Add(assignment);
        await _db.SaveChangesAsync(ct);
    }
}