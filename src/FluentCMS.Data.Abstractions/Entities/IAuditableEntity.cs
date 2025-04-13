namespace FluentCMS.Data.Abstractions.Entities;

/// <summary>
/// Interface for entities that need auditing information (created/modified timestamps)
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created
    /// </summary>
    DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the entity was last modified
    /// </summary>
    DateTime? ModifiedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the user who created the entity
    /// </summary>
    string? CreatedBy { get; set; }
    
    /// <summary>
    /// Gets or sets the user who last modified the entity
    /// </summary>
    string? ModifiedBy { get; set; }
}
