using System.Reflection;

using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Domain.Entities;
using CaWorkshop.Infrastructure.Data.Interceptors;
using CaWorkshop.Infrastructure.Identity;

using Duende.IdentityServer.EntityFramework.Options;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CaWorkshop.Infrastructure.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions options, 
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder
            .EnableDetailedErrors();
#endif

        optionsBuilder
            .AddInterceptors(_auditableEntitySaveChangesInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {   
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
