using MediatR;

namespace TaskManagement.Application.Employees;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeById, EmployeeDto?>
{
    private readonly IEmployeeReader _reader;
    public GetEmployeeByIdHandler(IEmployeeReader reader) => _reader = reader;
    public Task<EmployeeDto?> Handle(GetEmployeeById request, CancellationToken ct) =>
        _reader.ById(request.Id, ct);
}

public class ListEmployeesHandler : IRequestHandler<ListEmployees, IReadOnlyList<EmployeeDto>>
{
    private readonly IEmployeeReader _reader;
    public ListEmployeesHandler(IEmployeeReader reader) => _reader = reader;
    public Task<IReadOnlyList<EmployeeDto>> Handle(ListEmployees request, CancellationToken ct) =>
        _reader.List(request.Skip, request.Take, ct);
}

public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, Guid>
{
    private readonly IEmployeeWriter _writer;
    public CreateEmployeeHandler(IEmployeeWriter writer) => _writer = writer;
    public Task<Guid> Handle(CreateEmployee request, CancellationToken ct) =>
        _writer.Add(request.Name, request.Email, ct);
}

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee>
{
    private readonly IEmployeeWriter _writer;
    public UpdateEmployeeHandler(IEmployeeWriter writer) => _writer = writer;
    public async Task Handle(UpdateEmployee request, CancellationToken ct) =>
        await _writer.Update(request.Id, request.Name, request.Email, ct);
}

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee>
{
    private readonly IEmployeeWriter _writer;
    public DeleteEmployeeHandler(IEmployeeWriter writer) => _writer = writer;
    public async Task Handle(DeleteEmployee request, CancellationToken ct) =>
        await _writer.Delete(request.Id, ct);
}