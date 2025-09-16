using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Kernel;

namespace Shared.Infrastructure;

public class DbRepository<T> :IRepository<T> where T : BaseEntity
{
private readonly DbContext _db;
public DbRepository(DbContext db) => _db = db;
public async Task AddAsync(T entity, CancellationToken ct = default) =>
    await _db.Set<T>().AddAsync(entity, ct);
public IQueryable<T> Query() => _db.Set<T>().AsQueryable();
public async Task<T?> GetAsync(Guid id, CancellationToken ct = default) =>
    await _db.Set<T>().FindAsync(new object[] { id }, ct);
public void Remove(T entity) => _db.Set<T>().Remove(entity);
}