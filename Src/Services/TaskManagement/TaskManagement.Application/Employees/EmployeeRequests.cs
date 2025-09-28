using MediatR;

namespace TaskManagement.Application.Employees;

public sealed record CreateEmployee(string Name, string Email) : IRequest<Guid>;
public sealed record UpdateEmployee(Guid Id, string Name, string Email) : IRequest;
public sealed record DeleteEmployee(Guid Id) : IRequest;
public sealed record GetEmployeeById(Guid Id) : IRequest<EmployeeDto?>;
public sealed record ListEmployees(int Skip, int Take) : IRequest<IReadOnlyList<EmployeeDto>>;