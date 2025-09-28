using TaskManagement.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTaskManagementPersistence(builder.Configuration);
builder.Services.AddTaskManagementInfrastructure();
// Add MediatR for Application layer
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(TaskManagement.Application.Tasks.CreateTask).Assembly);
});

// Add health checks (optional, similar to Customer.Api)
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetSection("Database")["ConnectionString"]!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Optional: ProblemDetails middleware if you have it
// app.UseProblemDetails();

app.MapHealthChecks("/healthz");

// Apply migrations in dev (optional)
// if (app.Environment.IsDevelopment())
// {
//     using var scope = app.Services.CreateScope();
//     var db = scope.ServiceProvider.GetRequiredService<TaskManagementDbContext>();
//     await db.Database.MigrateAsync();
// }

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
