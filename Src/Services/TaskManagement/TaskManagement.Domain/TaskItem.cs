namespace TaskManagement.Domain;

public class TaskItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public TaskItem(string title, string? description)
    {
        Title = title;
        Description = description;
    }
}