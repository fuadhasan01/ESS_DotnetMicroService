namespace TaskManagement.Domain;

public class TaskItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public TaskStatus TaskStatus { get; private set; } = TaskStatus.Pending;
    public Status Status { get; private set; } = Status.Active;

    public TaskItem(string title, string? description)
    {
        Title = title;
        Description = description;
    }
    public void Update(string title, string? description, TaskStatus taskStatus)
    {
        Title = title;
        Description = description;
        TaskStatus = taskStatus;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Deactivate()
    {
        Status = Status.Inactive;
    }
}
public enum TaskStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3
}
public enum Status
{
    Active = 1,
    Inactive = 2
}