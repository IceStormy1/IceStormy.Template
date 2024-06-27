using IceStormy.Template.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IceStormy.Template.Abstractions.Interfaces;

namespace IceStormy.Template.Data;

public sealed class TemplateDbContext(DbContextOptions options) : DbContext(options)
{
    public override int SaveChanges()
    {
        SetDates();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetDates();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(BaseEntity))!);
    }

    private void SetDates()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IHasCreatedAt or IHasUpdatedAt && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            if (entityEntry is { State: EntityState.Added, Entity: IHasCreatedAt createdEntity })
                createdEntity.CreatedAt = DateTime.UtcNow;

            if (entityEntry is { State: EntityState.Modified, Entity: IHasUpdatedAt modifiedEntity })
                modifiedEntity.UpdatedAt = DateTime.UtcNow;
        }
    }
}