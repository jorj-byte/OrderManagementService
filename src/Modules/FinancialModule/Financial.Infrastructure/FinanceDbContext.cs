using Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure;

internal class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) :
        base(options) {}
    public DbSet<Payment> Payments => Set<Payment>();
}
