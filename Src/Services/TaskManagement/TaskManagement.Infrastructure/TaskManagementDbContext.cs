using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain;

namespace TaskManagement.Infrastructure;

public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
        : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<TaskAssignment> TaskAssignments => Set<TaskAssignment>();
}