using FluentCMS.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FluentCMS.Data.SQLite.Provider;

/// <summary>
/// Entity Framework Core DbContext for SQLite database
/// </summary>
public class SQLiteDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SQLiteDbContext"/> class
    /// </summary>
    /// <param name="options">The DbContext options</param>
    public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) 
        : base(options)
    {
    }

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    /// <returns>The number of affected records</returns>
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>The number of affected records</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates audit fields for tracked entities
    /// </summary>
    private void UpdateAuditFields()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedAt = utcNow;
                    break;
            }
        }
    }

    /// <summary>
    /// Configures the model for entities
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity mappings
        // This can be extended with additional configuration
        // or moved to separate configuration classes
    }
}