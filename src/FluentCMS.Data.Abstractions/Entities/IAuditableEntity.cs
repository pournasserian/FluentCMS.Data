namespace FluentCMS.Data.Abstractions.Entities;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
