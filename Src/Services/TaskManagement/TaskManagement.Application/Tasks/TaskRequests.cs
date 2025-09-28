using MediatR;

namespace TaskManagement.Application.Tasks;

public sealed record CreateTask(string Title, string? Description) : IRequest<Guid>;
public sealed record UpdateTask(Guid Id, string Title, string? Description) : IRequest;
public sealed record DeleteTask(Guid Id) : IRequest;
public sealed record GetTaskById(Guid Id) : IRequest<TaskDto?>;
public sealed record ListTasks(int Skip, int Take) : IRequest<IReadOnlyList<TaskDto>>;