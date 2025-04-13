namespace FluentCMS.Data.Abstractions.Entities;

/// <summary>
/// Base abstract class for all entities
/// </summary>
/// <typeparam name="TKey">Type of the entity identifier</typeparam>
public abstract class BaseEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity
    /// </summary>
    public TKey Id { get; set; } = default!;
    
    /// <summary>
    /// Determines whether the specified entity is equal to the current entity
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is not BaseEntity<TKey> other) return false;
        if (ReferenceEquals(this, obj)) return true;
        
        return Id.Equals(other.Id);
    }

    /// <summary>
    /// Returns the hash code for this entity
    /// </summary>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

/// <summary>
/// Base entity with string identifier
/// </summary>
public abstract class BaseEntity : BaseEntity<string>, IEntity
{
}
