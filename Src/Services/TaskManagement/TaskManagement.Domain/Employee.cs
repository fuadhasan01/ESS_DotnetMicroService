namespace TaskManagement.Domain;

public class Employee
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public EmployeeStatus Status { get; private set; } = EmployeeStatus.Active;

    public Employee(string name, string email)
    {
        Name = name;
        Email = email;
    }
    public void Deactivate()
    {
        Status = EmployeeStatus.Inactive;
    }

}

public enum EmployeeStatus
{
    Active = 1,
    Inactive = 2
}