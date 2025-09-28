using MediatR;

namespace TaskManagement.Application.Tasks;

public sealed record AssignTask(Guid TaskId, Guid EmployeeId) : IRequest;