using BuildingBlocks.Web.Errors;
using Customer.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
//builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddCustomerPersistence(builder.Configuration);

// Register infrastructure services (single extension method)
builder.Services.AddCustomerInfrastructure();

// Add MediatR for Application layer
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Customer.Application.Customers.CreateCustomer>());

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetSection("Database")["ConnectionString"]!);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseProblemDetails(); // <- Our custom ProblemDetails(Error) middleware
//app.UseSerilogRequestLogging();

app.MapHealthChecks("/healthz");

// Apply migrations in dev (optional)
// if (app.Environment.IsDevelopment())
// {
//     using var scope = app.Services.CreateScope();
//     var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
//     await db.Database.MigrateAsync();
// }

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
