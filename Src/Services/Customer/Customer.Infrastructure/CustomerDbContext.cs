using Customer.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

    public DbSet<Customer.Domain.Customer> Customers => Set<Customer.Domain.Customer>();
}