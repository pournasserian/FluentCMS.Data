using FluentCMS.Data.Abstractions;
using System;

namespace FluentCMS.Data.Common
{
    /// <summary>
    /// Base implementation of IEntity that can be used for all entities
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the date and time when the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified
        /// </summary>
        public DateTime? ModifiedAt { get; set; }
    }
}