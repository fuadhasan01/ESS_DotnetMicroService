using TaskManagement.Application.Tasks;
using TaskManagement.Domain;

namespace TaskManagement.Infrastructure.Persistence.Tasks;

public class TasksWriter : ITaskWriter
{
    private readonly TaskManagementDbContext _db;
    public TasksWriter(TaskManagementDbContext db) => _db = db;

    public async Task<Guid> Add(string title, string? description, CancellationToken ct)
    {
        var task = new TaskItem(title, description);
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync(ct);
        return task.Id;
    }

    public async Task Update(Guid id, string title, string? description, CancellationToken ct)
    {
        var task = await _db.Tasks.FindAsync(new object[] { id }, ct);
        if (task == null) throw new ArgumentException("Task not found", nameof(id));
        task.GetType().GetProperty("Title")!.SetValue(task, title);
        task.GetType().GetProperty("Description")!.SetValue(task, description);
        await _db.SaveChangesAsync(ct);
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var task = await _db.Tasks.FindAsync(new object[] { id }, ct);
        if (task == null) throw new ArgumentException("Task not found", nameof(id));
        // Soft delete by setting status to Inactive
        task.Deactivate();
        await _db.SaveChangesAsync(ct);
    }
}