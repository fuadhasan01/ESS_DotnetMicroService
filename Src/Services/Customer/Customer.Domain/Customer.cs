using System;

namespace Customer.Domain;

public sealed class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string? Phone { get; private set; }
    public CustomerStatus Status { get; private set; } = CustomerStatus.Active;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    private Customer() { }

    public Customer(string name, string email, string? phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public void Update(string name, string email, string? phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = CustomerStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum CustomerStatus
{
    Active = 1,
    Inactive = 2
}
