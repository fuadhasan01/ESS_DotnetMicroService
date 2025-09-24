namespace Customer.Application.Customers;

public record CustomerDto(Guid Id, string Name, string? Email, string? Phone);
