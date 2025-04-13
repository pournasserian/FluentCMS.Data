namespace FluentCMS.Data.Abstractions.Entities;

/// <summary>
/// Base entity interface that defines the ID property
/// </summary>
/// <typeparam name="TKey">Type of the entity identifier</typeparam>
public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity
    /// </summary>
    TKey Id { get; set; }
}

/// <summary>
/// Base entity interface with string identifier
/// </summary>
public interface IEntity : IEntity<string>
{
}
