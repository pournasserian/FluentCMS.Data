namespace FluentCMS.Data.Abstractions.Entities;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}
