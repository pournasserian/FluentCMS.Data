namespace FluentCMS.Data.Abstractions;

/// <summary>
/// Base interface for all entities in the system
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity
    /// </summary>
    Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified
    /// </summary>
    DateTime? ModifiedAt { get; set; }
}