namespace TaskManagement.Domain;

public class TaskAssignment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid TaskId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;

    public TaskAssignment(Guid taskId, Guid employeeId)
    {
        TaskId = taskId;
        EmployeeId = employeeId;
    }
}