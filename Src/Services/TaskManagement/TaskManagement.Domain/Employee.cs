namespace TaskManagement.Domain;

public class Employee
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Employee(string name, string email)
    {
        Name = name;
        Email = email;
    }

}