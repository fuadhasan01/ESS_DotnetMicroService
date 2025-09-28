using TaskManagement.Application.Tasks;
using TaskManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Persistence.Tasks;

public class TaskReader : ITaskReader
{
    private readonly TaskManagementDbContext _db;
    public TaskReader(TaskManagementDbContext db) => _db = db;

    public Task<TaskDto?> ById(Guid id, CancellationToken ct) =>
        _db.Tasks
            .Where(t => t.Id == id)
            .Select(t => new TaskDto(t.Id, t.Title, t.Description))
            .FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<TaskDto>> List(int skip, int take, CancellationToken ct) =>
        await _db.Tasks
            .OrderBy(t => t.Title)
            .Skip(skip).Take(take)
            .Select(t => new TaskDto(t.Id, t.Title, t.Description))
            .ToListAsync(ct);
}