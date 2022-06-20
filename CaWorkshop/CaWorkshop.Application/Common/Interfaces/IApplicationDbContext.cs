﻿
using CaWorkshop.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<TodoList> TodoLists { get; }
    
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
