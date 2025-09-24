using MediatR;

namespace Customer.Application.Customers;

public sealed record GetCustomerById(Guid Id) : IRequest<CustomerDto?>;
public sealed record ListCustomers(int Skip, int Take) : IRequest<IReadOnlyList<CustomerDto>>;
public sealed record CreateCustomer(string Name, string? Email, string? Phone) : IRequest<Guid>;
public sealed record UpdateCustomer(Guid Id, string Name, string? Email, string? Phone) : IRequest;
public sealed record DeleteCustomer(Guid Id) : IRequest;