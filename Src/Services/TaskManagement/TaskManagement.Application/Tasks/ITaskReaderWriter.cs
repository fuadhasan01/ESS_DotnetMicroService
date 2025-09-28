using TaskManagement.Domain;
namespace TaskManagement.Application.Tasks;

public interface ITaskReader
{
    Task<TaskDto?> ById(Guid id, CancellationToken ct);
    Task<IReadOnlyList<TaskDto>> List(int skip, int take, CancellationToken ct);
}

public interface ITaskWriter
{
    Task<Guid> Add(string title, string? description, CancellationToken ct);
    //Add TaskStatus parameter
    Task Update(Guid id, string title, string? description, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
    Task Assign(Guid taskId, Guid employeeId, CancellationToken ct);
}