using MediatR;
using TaskManagement.Domain;

namespace TaskManagement.Application.Tasks;

public class GetTaskByIdHandler : IRequestHandler<GetTaskById, TaskDto?>
{
    private readonly ITaskReader _reader;
    public GetTaskByIdHandler(ITaskReader reader) => _reader = reader;
    public Task<TaskDto?> Handle(GetTaskById request, CancellationToken ct) =>
        _reader.ById(request.Id, ct);
}

public class ListTasksHandler : IRequestHandler<ListTasks, IReadOnlyList<TaskDto>>
{
    private readonly ITaskReader _reader;
    public ListTasksHandler(ITaskReader reader) => _reader = reader;
    public Task<IReadOnlyList<TaskDto>> Handle(ListTasks request, CancellationToken ct) =>
        _reader.List(request.Skip, request.Take, ct);
}

public class CreateTaskHandler : IRequestHandler<CreateTask, Guid>
{
    private readonly ITaskWriter _writer;
    public CreateTaskHandler(ITaskWriter writer) => _writer = writer;
    public Task<Guid> Handle(CreateTask request, CancellationToken ct) =>
        _writer.Add(request.Title, request.Description, ct);
}

public class UpdateTaskHandler : IRequestHandler<UpdateTask>
{
    private readonly ITaskWriter _writer;
    public UpdateTaskHandler(ITaskWriter writer) => _writer = writer;
    public async Task Handle(UpdateTask request, CancellationToken ct) =>
        await _writer.Update(request.Id, request.Title, request.Description, ct);
}

public class DeleteTaskHandler : IRequestHandler<DeleteTask>
{
    private readonly ITaskWriter _writer;
    public DeleteTaskHandler(ITaskWriter writer) => _writer = writer;
    public async Task Handle(DeleteTask request, CancellationToken ct) =>
        await _writer.Delete(request.Id, ct);
}