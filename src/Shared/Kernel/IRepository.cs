﻿using Shared.Entities;

namespace Shared.Kernel;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    void Remove(T entity);
    IQueryable<T> Query();
}
