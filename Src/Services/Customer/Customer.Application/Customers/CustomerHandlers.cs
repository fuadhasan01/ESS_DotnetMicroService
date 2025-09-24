using MediatR;

namespace Customer.Application.Customers;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, CustomerDto?>
{
    private readonly ICustomerReader _reader;
    public GetCustomerByIdHandler(ICustomerReader reader) => _reader = reader;
    public Task<CustomerDto?> Handle(GetCustomerById request, CancellationToken ct)
        => _reader.ById(request.Id, ct);
}

public class ListCustomersHandler : IRequestHandler<ListCustomers, IReadOnlyList<CustomerDto>>
{
    private readonly ICustomerReader _reader;
    public ListCustomersHandler(ICustomerReader reader) => _reader = reader;
    public Task<IReadOnlyList<CustomerDto>> Handle(ListCustomers request, CancellationToken ct)
        => _reader.List(request.Skip, request.Take, ct);
}

public class CreateCustomerHandler : IRequestHandler<CreateCustomer, Guid>
{
    private readonly ICustomersWriter _writer;
    public CreateCustomerHandler(ICustomersWriter writer) => _writer = writer;
    public Task<Guid> Handle(CreateCustomer request, CancellationToken ct)
        => _writer.Add(request.Name, request.Email, request.Phone, ct);
}

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer>
{
    private readonly ICustomersWriter _writer;
    public UpdateCustomerHandler(ICustomersWriter writer) => _writer = writer;
    public async Task Handle(UpdateCustomer request, CancellationToken ct)
        => await _writer.Update(request.Id, request.Name, request.Email, request.Phone, ct);
}

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer>
{
    private readonly ICustomersWriter _writer;
    public DeleteCustomerHandler(ICustomersWriter writer) => _writer = writer;
    public async Task Handle(DeleteCustomer request, CancellationToken ct)
        => await _writer.Delete(request.Id, ct);
}